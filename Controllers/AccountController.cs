using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;

namespace Glimpse.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return View();
    }
}
