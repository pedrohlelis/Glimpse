using Glimpse.Migrations;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("default");
// Add services to the container.
builder.Services.AddDbContext<GlimpseContext>(
    options => options.UseSqlServer(connectionString)
);
builder.Services.AddIdentity<User, IdentityRole>(
    Options =>
    {
        Options.Password.RequiredUniqueChars = 0;
        Options.Password.RequireUppercase = true;
        Options.Password.RequiredLength = 8;
        Options.Password.RequireDigit = true;
        Options.Password.RequireNonAlphanumeric = true;
        Options.Password.RequireLowercase = true;
    }
    )
    .AddEntityFrameworkStores<GlimpseContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
