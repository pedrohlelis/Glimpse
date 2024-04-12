using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;

namespace Glimpse.Controllers;

[Authorize]
public class RoleController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    public RoleController(GlimpseContext db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
    }
    // Padrão para Roles, exibe todos os registros (teste)
    public async Task<IActionResult> ShowRoles()
    {
        // Lista de todos os projetos
        ViewData["projects"] = _db.Projects.ToList();
        // Lista de todos os usuários
        ViewData["users"] = _db.Users.ToList();
        // Relacionamento user - role
        ViewData["userRole"] = _db.UserRoles.ToList();
        // Lista de todos os roles
        List<Role> roles = _db.Roles.ToList();
        
        return View(roles);
    }
    public async Task<IActionResult> GetProjectBoards(int id)
    {
        Project project = await _db.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }
        ViewData["ProjectBoards"] = project.Boards;

        return View();
    }

    // CREATE
    public IActionResult Create(string id)
    {
        foreach (User item in _db.Users)
        {
            if(item.Id == id)
            {
                //name = item.UserName;
                break;
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(Board Board, IFormFile BoardImg)
    {
        Board.CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);

        if (ModelState.IsValid)
        {
            if (BoardImg != null && BoardImg.Length > 0)
            {
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "board-pictures");
                string nomeArquivo = new Guid() + "_" + Path.GetFileName(BoardImg.FileName);
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await BoardImg.CopyToAsync(stream);
                }
                Board.Background = "../board-pictures/" + nomeArquivo;
            } 
            _db.Boards.Add(Board);
            await _db.SaveChangesAsync();

            return RedirectToAction("BoardBoards");
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

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
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

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
                //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

                return View("Edit", Board);
            }
            return RedirectToAction("Get");
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

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

    /*public async Task<Dictionary<Lane, List<Card>>> GetLanesWithCardsAsync(int boardId)
    {
        Dictionary<Lane, List<Card>> laneCardHashMap = [];

        foreach (Lane lane in await _db.Lanes.ToListAsync())
        {
            if (lane.Board.Id == boardId)
            {
                List<Card> cards = lane.Cards.ToList();
                laneCardHashMap.Add(lane, cards);
            }
        }

        return laneCardHashMap;
    }*/
}
