using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Glimpse.ViewModels;

namespace Glimpse.Controllers;

[Authorize]
public class LaneController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public LaneController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }
    [HttpPost]
    public async Task<IActionResult> CreateLane(string name, int id, bool IsMemberSideBarActive)
    {
        if (string.IsNullOrEmpty(name))
        {
            return RedirectToAction("GetBoardInfo", "Board", new { id });
        }  

        var lane = new Lane
        {
            Name = name,
            Index = 0,
            Board = await _db.Boards.FirstOrDefaultAsync(x => x.Id == id)
        };
        _db.Lanes.Add(lane);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> SaveLaneOrder([FromForm] string laneIndexDictionary, int id, bool isMemberSideBarActive)
    {
        Console.WriteLine(laneIndexDictionary);
        // Deserialize the JSON strings back into structured data
        var laneIndexDictionaryDeserialized = JsonSerializer.Deserialize<Dictionary<string, int>>(laneIndexDictionary);

        foreach (var kvp in laneIndexDictionaryDeserialized)
        {
            Lane lane = await _db.Lanes.FirstOrDefaultAsync(x => x.Id == int.Parse(kvp.Key));
            lane.Index = kvp.Value;
            await _db.SaveChangesAsync();
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id, isMemberSideBarActive });
    }
}