using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Application.Interfaces;

namespace GLIMPSE.API.Controllers;

[Authorize]
public class TagController : Controller
{
    private readonly ITagApplicationService tagApplicationService;
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public TagController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager, ITagApplicationService tagApplicationService)
    {
        this.tagApplicationService = tagApplicationService;
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }
    [HttpPost]
    public async Task<IActionResult> CreateTag(string tagName, string color, int id, bool IsMemberSideBarActive)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return RedirectToAction("GetBoardInfo", "Board", new { id });
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

        return RedirectToAction("GetBoardInfo", "Board", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> EditTag(string Name,string Color, int tagId, int boardId, bool IsMemberSideBarActive)
    {
        Tag toEditTag = await _db.Tags.FindAsync(tagId);

        toEditTag.Name = Name;
        toEditTag.Color = Color;  

        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    [HttpPost]
    public IActionResult DeleteTag(int tagToDeleteId)
    {
        try
        {
            return RedirectToAction("GetBoardInfo", "Board", new { id = this.tagApplicationService.Remove(tagToDeleteId).Result.Board?.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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