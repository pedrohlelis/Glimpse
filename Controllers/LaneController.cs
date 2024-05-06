using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;

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
    public async Task<bool> CreateLane(string name, int id)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }
        var lane = new Lane
        {
            Name = name,
            Index = 0,
            Board = await _db.Boards.FirstOrDefaultAsync(x => x.Id == id)
        };
        if (lane.Board == null)
        {
            return false;
        }

        _db.Add(lane);
        await _db.SaveChangesAsync();

        return true;
    }

}