using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.DOMAIN.ViewModels;
using GLIMPSE.Domain.Helpers;

namespace GLIMPSE.API.Controllers;

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
            User = user,
            PicturePath = user.Picture,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };

        ViewData["UserId"] = _userManager.GetUserId(this.User);
        return View(userProfile);
    }
    [HttpGet()]
    public IActionResult GetUserInfo()
    {
        return Ok();
    }

    [HttpGet("profile/edit", Name = "ProfileEdit")]
    public IActionResult ProfileEdit()
    {
        var user = _userManager.GetUserAsync(this.User).Result;
        var userProfile = new ProfileVM
        {
            User = user,
            PicturePath = user.Picture,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };

        ViewData["UserId"] = _userManager.GetUserId(this.User);
        return View(userProfile);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile([FromForm] ProfileVM profileVM)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.GetUserAsync(User).Result;

            var profilePicture = profileVM.PictureFile;
            if (profilePicture != null && profilePicture.Length > 0)
            {
                if (user.Picture != null) { FileHandlingHelper.DeleteFile(@"..\UserProfilePics", user.Picture); }
                //user!.Picture = FileHandlingHelper.UploadFile(profilePicture, _profilePicFolderName, _hostingEnvironment);
            }
            else { user!.Picture = null; }
            // user.Email = profileVM.Email;
            // user.UserName = profileVM.Email;
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
        return RedirectToAction("Profile", "User");
    }
}