using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.ViewModels;
using Microsoft.AspNetCore.Identity;
using Glimpse.Services;

namespace Glimpse.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailService emailService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
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
        ModelState.AddModelError("", "Invalid Login Attempt");
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
                var resultReactivate = await _userManager.UpdateAsync(existingUser);

                if (resultReactivate.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Handle errors during reactivation
                    foreach (var error in resultReactivate.Errors)
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
                ProfilePic = null,
                IsActive = true,
            };

            var resultCreateUser = await _userManager.CreateAsync(user, model.Password!);
            if (resultCreateUser.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                // var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
                // EmailDto emailDto = new EmailDto(user.Email, "email confirmation.", "teste");
                // _emailService.SendEmail(emailDto);
                
                // await _emailService.SendEmail(user.Email, "teste", "Hello World");
                // return RedirectToAction(nameof(SuccessRegistration));
                return RedirectToAction("Index", "Home");
            }
            foreach(var error in resultCreateUser.Errors)
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
        await _userManager.UpdateAsync(user);

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return View("Error");
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
    }

    [HttpGet]
    public IActionResult SuccessRegistration()
    {
        return View();
    }
}
