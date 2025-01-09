using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Autofac;
using GLIMPSE.Infrastructure.IOC;
using GLIMPSE.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddAuthentication()
    .AddCertificate()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(o => o.AddPolicy(name: "MyPolicy", builder =>
{
    builder
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin()
           .WithExposedHeaders("X-Pagination")
           .WithHeaders("authorization", "accept", "content-type", "origin", "X-Pagination", "OPTIONS");
}));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GlimpseContext>(options =>
                                  options.UseSqlServer(builder.Configuration.GetConnectionString("default"),
                                                        options => options.EnableRetryOnFailure()));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<GlimpseContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromHours(3));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ModuleIOC());
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

var app = builder.Build();
app.UseCors("MyPolicy");

// Swagger and HTTPS setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();