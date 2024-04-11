using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
