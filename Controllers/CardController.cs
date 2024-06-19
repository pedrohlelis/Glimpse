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
            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive });
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

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> EditCard(int cardId, string? name, string? description, DateOnly? date, int id, bool IsMemberSideBarActive)
    {
        var card = await _db.Cards.FindAsync(cardId);

        card.Name = name;
        card.Description = description;
        card.Date = date;

        _db.Entry(card);

        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();

            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = IsMemberSideBarActive });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCard(int deleteCardId, int boardId, bool IsMemberSideBarActive)
    {
        try
        {
            var card = await _db.Cards
                .Include(c => c.User)
                .Include(c => c.Lane)
                .Include(c => c.Tags)
                .Include(c => c.Checkboxes)
                .FirstOrDefaultAsync(c => c.Id == deleteCardId);

            if (card != null)
            {
                foreach (var tag in card.Tags)
                {
                    tag.Cards.Remove(card);
                }

                // Safely remove the card from the user's cards collection if the user exists
                if (card.User != null && card.User.Cards != null)
                {
                    card.User.Cards.Remove(card);
                }

                // Safely remove the card from the lane's cards collection if the lane exists
                if (card.Lane != null && card.Lane.Cards != null)
                {
                    card.Lane.Cards.Remove(card);
                }

                // Create a list of checkboxes to remove
                var checkboxesToRemove = card.Checkboxes.ToList();

                foreach (var checkbox in checkboxesToRemove)
                {
                    checkbox.Card = null;
                    card.Checkboxes.Remove(checkbox);
                    _db.Checkboxes.Remove(checkbox);
                }

                // Remove the card from the database
                _db.Cards.Remove(card);
                await _db.SaveChangesAsync();
            }
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> SaveCardOrder([FromForm] string taskIndexDictionary, int id, bool IsMemberSideBarActive)
    {
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
    public async Task<IActionResult> RemoveUserFromCard(int userCardId, int boardId) {
        var card = await _db.Cards
            .Include(c => c.User)
            .SingleOrDefaultAsync(u => u.Id == userCardId);

        var user = _db.Users.Find(card.User.Id);

        user.Cards.Remove(card);

        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();

            return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }
}

