using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Controllers;

[Route("Glimpse/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly UserManager<User> _userManager;
    private readonly string _profilePicFolderName;

    public UserController(IWebHostEnvironment hostingEnvironment, UserManager<User> userManager)
    {
        _hostingEnvironment = hostingEnvironment;
        _userManager = userManager;
        _profilePicFolderName = "ProfilePics";
    }

    public IActionResult ProfilePage()
    {
        ViewData["UserId"] = _userManager.GetUserId(this.User);
        return View();
    }

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