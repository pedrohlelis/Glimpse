// using Glimpse.Migrations;
using Glimpse.Models;
using Glimpse.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Authentication setup
builder.Services.AddAuthentication()
    .AddCertificate()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });

builder.Services.AddScoped<IEmailSender, EmailSender>();

var connectionString = builder.Configuration.GetConnectionString("default");

// Swagger setup
builder.Services.AddSwaggerGen();

// Database context setup
builder.Services.AddDbContext<GlimpseContext>(
    options => options.UseSqlServer(connectionString)
);

// Identity setup
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GlimpseContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromHours(3));

// Configure JSON options to handle reference loops
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Aumentando a profundidade máxima se necessário
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
