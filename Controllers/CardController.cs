using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Glimpse.Controllers;

[Authorize]
public class CardController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public CardController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }

    public async Task<IActionResult> GetCardInfo(int id)
    {
        var Card = _db.Cards
            .Include(c => c.Tags)
            .Include(c => c.Checkboxes)
            .Single(c => c.Id == id);

        if (Card == null)
        {
            return NotFound();
        }

        return View(Card);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard(string name, int laneId, int id)
    {
        if (string.IsNullOrEmpty(name))
        {
            return RedirectToAction("GetBoardInfo", "Board", new { id });
        }

        var card = new Card
        {
            Name = name,
            Lane = await _db.Lanes.FirstOrDefaultAsync(x => x.Id == laneId)
        };

        _db.Cards.Add(card);
        await _db.SaveChangesAsync();
        var lane = await _db.Lanes
            .Include(u => u.Cards)
            .SingleAsync(p => p.Id == laneId);

        lane.Cards.Add(card);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> EditCard(int cardId, string? name, string? description, DateOnly? date, int id)
    {
        var card = await _db.Cards.FindAsync(cardId);

        card.Name = name;
        card.Description = description;
        card.Date = date;

        _db.Entry(card);

        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();

            return RedirectToAction("GetBoardInfo", "Board", new { id });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCard(int deleteCardId, int boardId)
    {
        try
        {
            var card = await _db.Cards.FindAsync(deleteCardId);
            _db.Cards.Remove(card);
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
        }
        
        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }
    [HttpPost]
    public IActionResult MoveCard(int id)
    {
        return RedirectToAction("GetBoardInfo", "Board", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> AddUserToCard(int userCardId, int boardId, string userId) {
        Card card = await _db.Cards.FindAsync(userCardId);
        var user = _db.Users
            .Include(u => u.Cards)
            .SingleOrDefault(u => u.Id == userId);

        card.User = user;

        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();

            return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }
    [HttpPost]
    public async Task<IActionResult> RemoveUserFromCard(int userCardId, int boardId, string userId) {
        Card card = await _db.Cards.FindAsync(userCardId);
        var user = _db.Users
            .Include(u => u.Cards)
            .SingleOrDefault(u => u.Id == userId);

        user.Cards.Remove(card);

        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();

            return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }
}
