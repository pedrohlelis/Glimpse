using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using Glimpse.ViewModels;
using Org.BouncyCastle.Crypto.Engines;

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

    public IActionResult KanbanTest() {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> GetBoardInfo(int id)
    {
        var board = _db.Boards
            .Include(u => u.Lanes)
            .Include(b => b.Project)
            .Single(u => u.Id == id);

        if (board == null)
        {
            return NotFound();
        }
        int projectId = board.Project.Id;

        List<User> members = await _db.Users
            .Where(u => u.Projects.Any(p => p.Id == projectId))
            .ToListAsync();

        List<Role> roles = await _db.Roles
            .Include(r => r.Users)
            .Where(r => r.Project.Id == projectId)
            .ToListAsync();

        var user = await _userManager.GetUserAsync(User);

        // Retrieve the user's role within the project associated with the board
        var userRole = await _db.Roles
            .Include(r => r.Users) // Assuming Role.Users is a collection of users with this role
            .FirstOrDefaultAsync(r => r.Project.Id == board.Project.Id && r.Users.Any(u => u.Id == user.Id));

        string responsibleUserId = board.Project.ResponsibleUserId;

        User responsibleUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == responsibleUserId);
        var model = new BoardVM
        {
            User = user,
            Board = board,
            ProjectRoles = roles,
            UserRole = userRole,
            ProjectResponsibleUser = responsibleUser,
            Members = members
        };

        //ViewData["users"] = GetUsersFromBoard(board);
        ViewData["lanes"] = board.Lanes;
        //ViewData["cards"] = 

        return View(model);
    }
    public async Task<IActionResult> GetProjectBoards(int id)
    {
        Project project = _db.Projects
            .Include(p => p.Boards)
            .Include(p => p.Users)
            .Single(p => p.Id == id);

        string userId = _userManager.GetUserId(User);
        bool isMember = project.Users.Any(pm => pm.Id == userId);
        bool isCreator = project.ResponsibleUserId == userId;

        if (project == null || project.IsActive == false)
        {
            return NotFound();
        }

        if (!isMember && !isCreator)
        {
            return Forbid(); // Return forbidden if the user is not a member or the creator
        }

        ViewData["Boards"] = project.Boards;
        ViewData["Creator"] = isCreator;

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
