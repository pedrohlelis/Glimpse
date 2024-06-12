using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Glimpse.Controllers;

[Authorize]
public class CheckboxController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public CheckboxController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCheckbox([FromBody] CheckboxRequestModel model)
    {
        if (model == null)
        {
            return Json(new { success = false, message = "Dados inválidos" });
        }
        System.Console.WriteLine(model.Name);
        System.Console.WriteLine(model.CardId);
        var checkbox = new Checkbox { Name = model.Name ,Finished = false };
        _db.Checkboxes.Add(checkbox);
        await _db.SaveChangesAsync();

        var card = await _db.Cards
        .Include(u => u.Checkboxes)
        .SingleAsync(p => p.Id == model.CardId);

        card.Checkboxes.Add(checkbox);
        await _db.SaveChangesAsync();
        
        return Json(new { success = true, checkbox });
    }
}
