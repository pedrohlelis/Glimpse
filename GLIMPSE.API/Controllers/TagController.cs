using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.Infrastructure.Data.Context;

namespace GLIMPSE.API.Controllers;

[Authorize]
public class TagController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public TagController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }
    [HttpPost]
    public async Task<IActionResult> CreateTag(string tagName, string color, int id, bool IsMemberSideBarActive)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = IsMemberSideBarActive });
        }  

        var Tag = new Tag
        {
            Name = tagName,
            Color = color,
            Board = await _db.Boards.FirstOrDefaultAsync(x => x.Id == id)
        };

        _db.Tags.Add(Tag);
        await _db.SaveChangesAsync();
        var Board = await _db.Boards
            .Include(u => u.Tags)
            .SingleAsync(p => p.Id == id);

        Board.Tags.Add(Tag);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> EditTag(string Name,string Color, int tagId, int boardId, bool IsMemberSideBarActive)
    {
        // Retrieve the existing role from the database
        Tag toEditTag = await _db.Tags.FindAsync(tagId);

        // Update the role properties with the submitted form data
        toEditTag.Name = Name;
        toEditTag.Color = Color;

        // Save changes to the database
        await _db.SaveChangesAsync();
        // Redirect to the appropriate action with the boardId parameter
        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTag(int tagToDeleteId, int id, bool IsMemberSideBarActive)
    {
        Tag tag = await _db.Tags.FindAsync(tagToDeleteId);
        var Board = await _db.Boards
            .Include(u => u.Tags)
            .SingleAsync(p => p.Id == id);

        Board.Tags.Remove(tag);
        _db.Tags.Remove(tag);
        await _db.SaveChangesAsync();
        return RedirectToAction("GetBoardInfo", "Board", new { id = id, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public async Task<IActionResult> AddTagToCard(int tagCardId, int tagId, int boardId)
    {
        var tag = _db.Tags.FirstOrDefault(x => x.Id == tagId);
        var card = _db.Cards
            .Include(b => b.Tags)
            .SingleOrDefault(u => u.Id == tagCardId);
        
        if (tag != null && card != null)
        {
            card.Tags.Add(tag);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId});
    }
}