using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.Migrations;
using Glimpse.Helpers;

namespace Glimpse.Controllers;

[Route("Glimpse/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly string _profilePicFolderName;

    public UserController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
        _profilePicFolderName = "ProfilePics";
    }


    // [HttpGet("Register")]
    // public IActionResult RegisterPage()
    // {
    //     return View("Register");
    // }

    // [HttpPost("Register")]
    // public async Task<IActionResult> PostNewUser([FromForm] User newUser, IFormFile profilePic)
    // {
    //     try
    //     {
    //         await using (var _context = new GlimpseContext())
    //         {
    //             newUser.IsActive = true;

    //             string profilePicPath = FileHandlingHelper.UploadFile(profilePic, _profilePicFolderName, _hostingEnvironment);
    //             newUser.ProfilePic = profilePicPath;

    //             _context.Users.Add(newUser);
    //             _context.SaveChanges();
    //             return Ok("Usuario cadastrado com sucesso.");
    //         }
    //     }
    //     catch(Exception e)
    //     {
    //         return BadRequest("Ocorreu um erro ao cadastrar o usuario. " + e.Message);
    //     }
    // }

    // [HttpGet("UsersList")]
    // public async Task<IActionResult> GetAllUsers()
    // {
    //     try
    //     {
    //         await using (var _context = new GlimpseContext())
    //         {
    //             List<User> item = _context.Users.Where(u => u.IsActive).ToList();
    //             return View(@"UserList", item); ;
    //         }
    //     }
    //     catch(Exception e)
    //     {
    //         return NotFound("Ocorreu um erro durante sua solicitacao. " + e.Message);
    //     }
    // }

    // [HttpPut("UpdateUser/{id}")]
    // public async Task<IActionResult> UpdateUser(int id, [FromForm] User userNewInfo)
    // {
    //     try
    //     {
    //         await using (var _context = new GlimpseContext())
    //         {
    //             var item = _context.Users.FirstOrDefault(t => t.UserId == id);
                
    //             item.UserName = userNewInfo.UserName;
    //             item.UserEmail = userNewInfo.UserEmail;
    //             item.UserPassword = userNewInfo.UserPassword;

    //             _context.SaveChanges();
    //             return Ok("Os dados do usuario foram atualizados.");
    //         }
    //     }
    //     catch(Exception e)
    //     {
    //         return BadRequest("Ocorreu um erro durante sua requisição. " + e.Message);
    //     }
    // }

    // [HttpDelete("RemoveUser/{id}")]
    // public async Task<IActionResult> RemoveUser(int id)
    // {
    //     try
    //     {
    //         await using (var _context = new GlimpseContext())
    //         {
    //             var item = _context.Users.FirstOrDefault(t => t.UserId == id);

    //             item.IsActive = false;

    //             _context.SaveChanges();
    //             return Ok("Usuario removido com sucesso.");
    //         }
    //     }
    //     catch(Exception e)
    //     {
    //         return BadRequest("Erro ao remover usuario. " + e.Message);
    //     }
    // }
}