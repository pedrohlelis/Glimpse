using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Microsoft.AspNetCore.Identity;
using Glimpse.ViewModels;
using System.Text.RegularExpressions;

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

    [Authorize]
    public async Task<IActionResult> GetBoardInfo(int id, bool IsMemberSideBarActive)
    {
        var board = await _db.Boards
            .Include(b => b.Project)
            .Include(b => b.Tags)
            .Include(b => b.Lanes)
                .ThenInclude(l => l.Cards)
                    .ThenInclude(c => c.Tags)
            .Include(b => b.Lanes)
                .ThenInclude(l => l.Cards)
                    .ThenInclude(c => c.Checkboxes)
            .SingleOrDefaultAsync(u => u.Id == id);


        if (board == null)
        {
            return NotFound();
        }

        int projectId = board.Project.Id;

        var project = await _db.Projects
            .Include(b => b.Boards)
            .SingleOrDefaultAsync(p => p.Id == projectId);

        if(!project.IsActive) {
            return NotFound();
        }

        // Retrieve all members associated with the project
        List<User> members = await _db.Users
            .Where(u => u.Projects.Any(p => p.Id == projectId))
            .ToListAsync();

        var currentUser = await _userManager.GetUserAsync(User);

        var user = _db.Users
        .Include(u => u.Projects)
        .FirstOrDefault(u => u.Id == currentUser.Id);

        if (!user.Projects.Contains(board.Project)){
            return Forbid();
        }
        // Retrieve the user's role within the project associated with the board
        var userRole = await _db.Roles
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Project.Id == board.Project.Id && r.Users.Any(u => u.Id == user.Id));

        string responsibleUserId = board.Project.ResponsibleUserId;

        User responsibleUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == responsibleUserId);

        // Create a dictionary to store users and their roles
        List<Role> canManageRoles = new List<Role>();
        // Retrieve all roles associated with the project
        List<Role> roles = await _db.Roles
            .Include(r => r.Users)
            .Where(r => r.Project.Id == projectId)
            .ToListAsync();
        foreach (Role role in roles) {
            if(role.CanManageRoles)
            {
                canManageRoles.Add(role);
            }
        }
        Dictionary<User, Role> userRoles = [];

        foreach (var member in members)
        {
            var memberRole = roles.FirstOrDefault(r => r.Users.Any(u => u.Id == member.Id));

            userRoles.Add(member, memberRole);
        }

        List<Board> userBoards = [];
        foreach (var userBoard in project.Boards) {
            userBoards.Add(userBoard);
        }

        var model = new BoardVM
        {
            User = user,
            UserBoards = userBoards,
            Board = board,
            ProjectRoles = roles,
            UserRole = userRole,
            ProjectResponsibleUser = responsibleUser,
            Members = members,
            UserRolesDictionary = userRoles, // Add the dictionary to the model
            CanManageRoles = canManageRoles,
            IsMemberSideBarActive = IsMemberSideBarActive,
            Tags = (List<Tag>)board.Tags
        };

        return View(model);
    }
    public async Task<IActionResult> GetProjectBoards(int id)
    {
        try {
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
                return Forbid();
            }

            ViewData["Boards"] = project.Boards;
            ViewData["Creator"] = isCreator;

            return View(project);
        }
        catch(Exception ex)
        {
            return NotFound();
        }
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
        else
        {
            Board.Background = "../board-pictures/defaultBackground.jpg";
        }

        var BacklogLane = new Lane
        {
            Name = "BackLog",
            Board = Board,
            Index = 0,
        };
        var ToDoLane = new Lane
        {
            Name = "To Do",
            Board = Board,
            Index = 1,
        };
        var DoneLane = new Lane
        {
            Name = "Done",
            Board = Board,
            Index = 2,
        };
        Board.Lanes.Add(BacklogLane);
        Board.Lanes.Add(ToDoLane);
        Board.Lanes.Add(DoneLane);
        _db.Boards.Add(Board);
        await _db.SaveChangesAsync();
        // Board.Background = "../board-pictures/defaultBackground.jpg";
        if (ModelState.IsValid)
        {
            var project = await _db.Projects
                .Include(u => u.Boards)
                .SingleAsync(p => p.Id == projectId);

            project.Boards.Add(Board);
            await _db.SaveChangesAsync();
            return RedirectToAction("GetBoardInfo", new { id = Board.Id, IsMemberSideBarActive = true });
        }

        return View("Create", Board);
    }

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
    public async Task<IActionResult> EditBoard(Board Board, IFormFile? BoardImg)
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
                Board oldBoard = await _db.Boards.FindAsync(Board.Id);
                _db.Entry(oldBoard).CurrentValues.SetValues(Board);

                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return View("Edit", Board);
            }
            return RedirectToAction("GetBoardInfo", new { id = Board.Id });
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
