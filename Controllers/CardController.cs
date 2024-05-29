using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;

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
            .Include(c => c.Users)
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
    public async Task<IActionResult> EditCard(int cardId, string name, string description, DateOnly date, int id)
    {
        var card = await _db.Cards.FindAsync(cardId);
        card.Name = name;
        card.Description = description;
        card.Date = date;

        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();

            return RedirectToAction("GetBoardInfo", "Board", new { id });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id });
    }

    public async Task<IActionResult> Delete(int laneId, int CardId)
    {
        var Card = await _db.Cards.FindAsync(CardId);

        if (Card == null)
        {
            return NotFound();
        }

        ViewData["laneId"] = laneId;

        return View(Card);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarCard(Card Card, IFormFile CardImg, int laneId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ViewData["uniqueAlert"] = "Chassi do Card ja cadastrado";

                return View("Edit", Card);
            }
            return RedirectToAction("GetlaneCards", new { id = laneId });
        }

        return View("Edit", Card);
    }

    public async Task<ICollection<User>> GetUsersFromCard(Card Card)
    {
        /*ICollection<User> users = [];

        foreach (User user in Card.lane.Users)
        {
            if (user.IsActive == true)
            {
                users.Add(user);
            }
        }*/
        
        return null;
    }
}
