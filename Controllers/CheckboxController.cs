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
        var checkbox = new Checkbox { Name = model.Name ,Finished = model.Finished };
        _db.Checkboxes.Add(checkbox);
        await _db.SaveChangesAsync();

        var card = await _db.Cards
            .Include(u => u.Checkboxes)
            .SingleAsync(p => p.Id == model.CardId);

        card.Checkboxes.Add(checkbox);
        await _db.SaveChangesAsync();
        
        return Json(new { success = true, checkbox });
    }
    [HttpPost]
    public async Task<IActionResult> EditCheckbox(int boardId, string name, bool finished, int checkboxId)
    {
        var checkbox = await _db.Checkboxes.FindAsync(checkboxId);

        checkbox.Name = name;
        checkbox.Finished = finished;

        _db.Entry(checkbox);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }
    [HttpPost]
    public async Task<IActionResult> DeleteCheckbox(int boardId, int checkboxId, int cardId)
    {
        var card = await _db.Cards
            .Include(u => u.Checkboxes)
            .FirstOrDefaultAsync(u => u.Id == cardId);

        var checkbox = await _db.Checkboxes.FindAsync(checkboxId);

        card.Checkboxes.Remove(checkbox);

        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }
}
