using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
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
    public async Task<IActionResult> CreateCard(string name, int laneId, int id, bool IsMemberSideBarActive)
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

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
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

            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCard(int deleteCardId, int boardId)
    {
        try
        {
            var card = await _db.Cards
                .Include(u => u.Checkboxes)
                .SingleAsync(p => p.Id == deleteCardId);

            card.Checkboxes.Clear();
            await _db.SaveChangesAsync();

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
    public async Task<IActionResult> SaveCardOrder([FromForm] string taskIndexDictionary, int id, bool IsMemberSideBarActive)
    {
        // Deserialize the JSON strings back into structured data
        Console.WriteLine(taskIndexDictionary);
        try
        {
            // Deserialize the JSON strings back into structured data
            var taskIndexDictionaryDeserialized = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(taskIndexDictionary);

            foreach (var kvp in taskIndexDictionaryDeserialized)
            {
                Card card = await _db.Cards.FirstOrDefaultAsync(x => x.Id == int.Parse(kvp.Key));
                if (card != null)
                {
                    if (int.TryParse(kvp.Value[1], out int newIndex))
                    {
                        Lane lane = await _db.Lanes.FirstOrDefaultAsync(l => l.Id == int.Parse(kvp.Value[0]));
                        card.Index = newIndex;
                        card.Lane = lane;
                        lane.Cards.Add(card);
                    }
                    else
                    {
                        // Handle invalid index format
                    }

                    await _db.SaveChangesAsync();
                }
                else
                {
                    // Handle card not found
                }
            }
            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive });
        }
        catch (JsonException ex)
        {
            // Handle JSON deserialization error
            return BadRequest("Invalid JSON format: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
        }
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

