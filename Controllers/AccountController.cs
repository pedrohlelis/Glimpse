using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!user.IsActive)
                {
                    ModelState.AddModelError("", "Invalid Login Attempt.");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View(model);
            }
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if(ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null && !existingUser.IsActive)
            {
                // Reactivate the existing user
                existingUser.IsActive = true;
                var result1 = await _userManager.UpdateAsync(existingUser);

                if (result1.Succeeded)
                {
                    // Handle successful reactivation
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Handle errors during reactivation
                    foreach (var error in result1.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }


            User user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                Picture = null,
                IsActive = true,
            };

            var result = await _userManager.CreateAsync(user, model.Password!);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("DeleteAccount", Name = "DeleteAccount")]
    public async Task<IActionResult> DeleteAccount()
    {
        var user = _userManager.GetUserAsync(User).Result;

        user.IsActive = false;
        _userManager.UpdateAsync(user);

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}
