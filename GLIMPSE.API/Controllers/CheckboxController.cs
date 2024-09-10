using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GLIMPSE.API.Controllers;

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
    public async Task<IActionResult> EditCheckbox(int boardId, [FromForm] string checkboxesStatus)
    {

        var checkboxesStatusDeserialized = JsonSerializer.Deserialize<Dictionary<string, bool>>(checkboxesStatus);

        // Iterate through the dictionary and update checkboxes in the database
        foreach (var kvp in checkboxesStatusDeserialized)
        {

            var checkbox = _db.Checkboxes.FirstOrDefault(c => c.Id == int.Parse(kvp.Key));

            checkbox.Finished = kvp.Value;
        }
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive = false });
        // return BadRequest("Checkbox states updated successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCheckbox(int boardId, int checkboxId, int cardId)
    {
        var card = await _db.Cards
            .Include(u => u.Checkboxes)
            .FirstOrDefaultAsync(u => u.Id == cardId);

        var checkbox = await _db.Checkboxes.FindAsync(checkboxId);

        card.Checkboxes.Remove(checkbox);
        _db.Checkboxes.Remove(checkbox);

        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive = false });
    }
}
