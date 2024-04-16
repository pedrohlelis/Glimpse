using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Controllers;

[Authorize]
public class BoardController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public BoardController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }
    // Abre o quadro
    public async Task<IActionResult> GetBoardInfo(int id)
    {
        Board board = _db.Boards.Find(id);
        Console.WriteLine(id);

        if (board == null)
        {
            return NotFound();
        }
        // Lista dos usuarios do quadro
        ViewData["users"] = GetUsersFromBoard(board);

        // HashMap das raias e dos cards do quadro
        ViewData["lanes"] = GetLanesFromBoard(board);
        
        // Hash -> Card e Tags
        

        return View(board);
    }
    public async Task<IActionResult> GetProjectBoards(int id)
    {
        Project project = await _db.Projects.FindAsync(id);

        if (project == null || project.IsActive == false)
        {
            return NotFound();
        }

        ViewData["Boards"] = project.Boards;

        return View(project);
    }

    // CREATE
    public IActionResult Create(int id)
    {
        Project project = _db.Projects.Find(id);

        ViewData["project"] = project;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(Board Board, IFormFile BoardImg, int projectId)
    {
        Board.CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);
        Board.IsActive = true;
        Board.CreatorId = _userManager.GetUserAsync(User).Result.Id;
        Board.Project = _db.Projects.Find(projectId);

        if (ModelState.IsValid)
        {
            if (BoardImg != null && BoardImg.Length > 0)
            {
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "board-pictures");
                string nomeArquivo = new Guid() + "-board-pic.png";
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await BoardImg.CopyToAsync(stream);
                }
                Board.Background = "../board-pictures/" + nomeArquivo;
            } 
            var entityEntry = _db.Boards.Add(Board);
            await _db.SaveChangesAsync();
            int id = entityEntry.Entity.Id;

            return await GetBoardInfo(id);
        }

        return View("Create", Board);
    }

    // UPDATE
    public async Task<IActionResult> Edit(int id)
    {
        var Board = await _db.Boards.FindAsync(id);

        if (Board == null)
        {
            return NotFound();
        }

        return View(Board);
    }

    [HttpPost]
    public async Task<IActionResult> EditarBoard(Board Board)
    {
        if (ModelState.IsValid)
        {
            //var BoardAntigo = await _db.Boards.FindAsync(Board.CodBoard);
            //_db.Entry(BoardAntigo).CurrentValues.SetValues(Board);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ViewData["uniqueAlert"] = "Chassi do Board ja cadastrado";

                return View("Edit", Board);
            }
            return RedirectToAction("Get");
        }

        return View("Edit", Board);
    }

    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var Board = await _db.Boards.FindAsync(id);

        if (Board == null)
        {
            return NotFound();
        }

        return View(Board);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarBoard(Board Board)
    {
        if (ModelState.IsValid)
        {
            Board item = await _db.Boards.FindAsync(Board.Id);
            _db.Entry(item).CurrentValues.SetValues(Board);
            await _db.SaveChangesAsync();

            return RedirectToAction("Get");
        }

        return View("Delete", Board);
    }

    public async Task<ICollection<User>> GetUsersFromBoard(Board board)
    {
        ICollection<User> users = [];

        foreach (User user in board.Project.Users)
        {
            if (user.IsActive == true)
            {
                users.Add(user);
            }
        }
        
        return users;
    }

    public ICollection<Lane> GetLanesFromBoard(Board board)
    {
        ICollection<Lane> lanes = [];

        foreach (Lane lane in board.Lanes)
        {
            lanes.Add(lane);
        }

        return lanes;
    }
}
