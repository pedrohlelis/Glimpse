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
        var checkbox = new Checkbox { Name = model.Name , Finished = model.Finished }; // Adiciona o campo Finished
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
    public async Task<IActionResult> Edit(CheckboxRequestModel model)
    {
        if (model == null)
        {
            return BadRequest("Model is null");
        }

        if (ModelState.IsValid)
        {
            var item = await _db.Checkboxes.FindAsync(model.CheckboxId);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = model.Name;
            item.Finished = model.Finished;

            try
            {
                await _db.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCheckbox([FromBody] int checkboxId, int boardId)
    {
        try
        {
            var checkbox = await _db.Checkboxes.FindAsync(checkboxId);
            if (checkbox == null)
            {
                // Checkbox não encontrado, redireciona para a visualização do board com uma mensagem de erro
                TempData["ErrorMessage"] = "Checkbox não encontrado.";
                return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
            }

            _db.Checkboxes.Remove(checkbox);
            await _db.SaveChangesAsync();
            
            // Sucesso, redireciona para a visualização do board com uma mensagem de sucesso
            TempData["SuccessMessage"] = "Checkbox removido com sucesso.";
        }
        catch (DbUpdateException ex)
        {
            TempData["ErrorMessage"] = "Erro ao remover o checkbox.";
            return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }

}
