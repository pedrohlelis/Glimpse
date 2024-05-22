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

    public async Task<IActionResult> GetBoardInfo(int id)
    {
        var board = _db.Boards
            .Include(u => u.Lanes)
            .Single(u => u.Id == id);

        if (board == null)
        {
            return NotFound();
        }

        //ViewData["users"] = GetUsersFromBoard(board);
        ViewData["lanes"] = board.Lanes;
        //ViewData["cards"] = 


        return View(board);
    }
    public async Task<IActionResult> GetProjectBoards(int id)
    {
        System.Console.WriteLine("");
        Project project = _db.Projects
            .Include(u => u.Boards)
            .Single(u => u.Id == id);
        bool creator = false;
        if (project == null || !project.IsActive )

        if (project == null || project.IsActive == false)
        {
            return NotFound();
        }
        string userId = _userManager.GetUserId(User);

        if (userId == project.ResponsibleUserId)
        {
            creator = true;
        }

        ViewData["Boards"] = project.Boards;
        ViewData["Creator"] = creator;

        return View(project);
    }

    public IActionResult Create(int id)
    {
        var project = _db.Projects
            .Include(u => u.Boards)
            .Single(u => u.Id == id);

        ViewData["project"] = project;
        ViewData["projectName"] = project.Name;
        ViewData["projectId"] = project.Id;
        
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
                string nomeArquivo = Guid.NewGuid() + "-board-pic.png";
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await BoardImg.CopyToAsync(stream);
                }
                Board.Background = "../board-pictures/" + nomeArquivo;
            }
            _db.Boards.Add(Board);
            await _db.SaveChangesAsync();
            var project = await _db.Projects
                .Include(u => u.Boards)
                .SingleAsync(p => p.Id == projectId);

            project.Boards.Add(Board);
            await _db.SaveChangesAsync();
            return RedirectToAction("GetBoardInfo", new { id = Board.Id });
        }

        return View("Create", Board);
    }

    public async Task<IActionResult> Edit(int projectId, int boardId)
    {
        var Board = await _db.Boards.FindAsync(boardId);

        if (Board == null)
        {
            return NotFound();
        }

        ViewData["projectId"] = projectId;

        return View(Board);
    }

    [HttpPost]
    public async Task<IActionResult> EditBoard(Board Board, IFormFile BoardImg, int projectId)
    {

        if (ModelState.IsValid)
        {
            if (BoardImg != null && BoardImg.Length > 0)
            {
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "board-pictures");
                string nomeArquivo = Guid.NewGuid() + "-board-pic.png";
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await BoardImg.CopyToAsync(stream);
                }
                Board.Background = "../board-pictures/" + nomeArquivo;
            }
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ViewData["uniqueAlert"] = "Chassi do Board ja cadastrado";

                return View("Edit", Board);
            }
            return RedirectToAction("GetProjectBoards", new { id = projectId });
        }

        return View("Edit", Board);
    }

    public async Task<IActionResult> Delete(int projectId, int boardId)
    {
        var Board = await _db.Boards.FindAsync(boardId);

        if (Board == null)
        {
            return NotFound();
        }

        ViewData["projectId"] = projectId;

        return View(Board);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarBoard(Board Board, IFormFile BoardImg, int projectId)
    {
        if (ModelState.IsValid)
        {
            if (BoardImg != null && BoardImg.Length > 0)
            {
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "board-pictures");
                string nomeArquivo = Guid.NewGuid() + "-board-pic.png";
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await BoardImg.CopyToAsync(stream);
                }
                Board.Background = "../board-pictures/" + nomeArquivo;
            }
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ViewData["uniqueAlert"] = "Chassi do Board ja cadastrado";

                return View("Edit", Board);
            }
            return RedirectToAction("GetProjectBoards", new { id = projectId });
        }

        return View("Edit", Board);
    }

    public async Task<ICollection<User>> GetUsersFromBoard(Board board)
    {
        /*ICollection<User> users = [];

        foreach (User user in board.Project.Users)
        {
            if (user.IsActive == true)
            {
                users.Add(user);
            }
        }*/
        
        return null;
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
