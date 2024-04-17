using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Glimpse.ViewModels;
using Microsoft.VisualBasic;

namespace Glimpse.Controllers;

[Route("Glimpse/[controller]")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly UserManager<User> _userManager;
    private readonly string _profilePicFolderName;

    public UserController(IWebHostEnvironment hostingEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _hostingEnvironment = hostingEnvironment;
        _userManager = userManager;
        _signInManager = signInManager;
        _profilePicFolderName = "UserProfilePics";
    }

    [HttpGet("profile", Name = "Profile")]
    public IActionResult Profile()
    {
        var user = _userManager.GetUserAsync(User).Result;

        var userProfile = new ProfileVM
        {
            ProfilePicPath = user.ProfilePic,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };

        ViewData["UserId"] = _userManager.GetUserId(this.User);
        return View(userProfile);
    }

    [HttpGet("profile/edit", Name = "ProfileEdit")]
    public IActionResult ProfileEdit()
    {
        var user = _userManager.GetUserAsync(User).Result;
        var userProfile = new ProfileVM
        {
            ProfilePicPath = user.ProfilePic,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.UserName,
        };

        ViewData["UserId"] = _userManager.GetUserId(this.User);
        return View(userProfile);
    }

    [HttpPost("profile/edit", Name = "UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromForm] ProfileVM profileVM)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.GetUserAsync(User).Result;

            var existingUser = await _userManager.FindByEmailAsync(profileVM.Email);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                ModelState.AddModelError("Email", "This email is already taken by another user.");
                return View("ProfileEdit", profileVM);
            }
            else
            {
                profileVM.Email = user.Email;
            }

            var profilePicture = profileVM.ProfilePicFile;
            if (profilePicture != null && profilePicture.Length > 0)
            {
                if (user.ProfilePic != null) { FileHandlingHelper.DeleteFile(@"..\UserProfilePics", user.ProfilePic); }
                user!.ProfilePic = FileHandlingHelper.UploadFile(profilePicture, _profilePicFolderName, _hostingEnvironment);
            }
            else { user!.ProfilePic = null; }
            user.Email = profileVM.Email;
            user.UserName = profileVM.Email;
            user.FirstName = profileVM.FirstName;
            user.LastName = profileVM.LastName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile", "User");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("ProfileEdit",profileVM);
        }
        return View("ProfileEdit",profileVM);
    }
    public async Task<IActionResult> DeleteProfile()
    {
        var user = _userManager.GetUserAsync(User).Result;
            user!.IsActive = false;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
    }
}