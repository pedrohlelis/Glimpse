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
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly UserManager<User> _userManager;
    private readonly string _profilePicFolderName;

    public UserController(IWebHostEnvironment hostingEnvironment, UserManager<User> userManager)
    {
        _hostingEnvironment = hostingEnvironment;
        _userManager = userManager;
        _profilePicFolderName = "UserProfilePics";
    }

    [HttpGet("profile", Name = "Profile")]
    public IActionResult Profile()
    {
        var user = _userManager.GetUserAsync(User).Result;

        var userProfile = new ProfileVM
        {
            PicturePath = user.Picture,
            Name = user.Name,
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
            PicturePath = user.Picture,
            Name = user.Name,
            Email = user.Email,
        };

        ViewData["UserId"] = _userManager.GetUserId(this.User);
        return View(userProfile);
    }

    [HttpPost("profile/edit", Name = "UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromForm] ProfileVM profileVM)
    {
        var user = _userManager.GetUserAsync(User).Result;

        if (ModelState.IsValid)
        {
            var profilePicture = profileVM.PictureFile;
            if (profilePicture != null && profilePicture.Length > 0)
            {
                if (user.Picture != null) { FileHandlingHelper.DeleteFile(@"C:\Glimpse\wwwroot\UserProfilePics", user.Picture); }
                user!.Picture = FileHandlingHelper.UploadFile(profilePicture, _profilePicFolderName, _hostingEnvironment);
            }
            else { user!.Picture = null; }
            user.Email = profileVM.Email;
            user.UserName = profileVM.Email;
            user.Name = profileVM.Name;
            user.Name = profileVM.Name;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile", "User");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View("ProfileEdit", profileVM);
    }

}