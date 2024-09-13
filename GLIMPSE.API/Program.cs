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

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddAuthentication()
    .AddCertificate()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });

builder.Services.AddScoped<IEmailSender, EmailSender>();

var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GlimpseContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GlimpseContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromHours(3));

builder.Services.AddScoped<GitHubService>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ModuleIOC());
});

// Configure JSON options to handle reference loops
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Swagger and HTTPS setup
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Default route setup
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapRazorPages();
app.Run();