using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using System.ComponentModel.DataAnnotations;

namespace Glimpse.Controllers;

public class BoardController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    public BoardController(GlimpseContext db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
    }
    // Abre o quadro
    public async Task<IActionResult> GetBoardInfo(int id)
    {
        var board = await _db.Boards.FindAsync(id);

        if (board == null)
        {
            return NotFound();
        }
        // Lista dos usuarios do quadro
        ViewData["users"] = GetUsersFromBoard(id);

        // HashMap das raias e dos cards do quadro
        ViewData["lanesAndCards"] = GetLanesWithCardsAsync(id);

        // List de checkboxes
        
        
        // Hash -> Card e Tags
        

        return View(board);
    }

    // CREATE
    public IActionResult Create(string id)
    {
        string name = "";
        foreach (User item in _db.Users)
        {
            //if(item.UserId == id)
            {
                //name = item.UserName;
                break;
            }
        }
        ViewData["projectName"] = name;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(Board Board, IFormFile BoardImg)
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

    public async Task<List<User>> GetUsersFromBoard(int boardId)
    {
        List<User> users = [];
        //foreach (Team team in await _db.Teams.ToListAsync())
        {
            //if (await _db.Boards.FindAsync(Board.Id))
            {

            }
        }
        
        return users;
    }

    public async Task<Dictionary<Lane, List<Card>>> GetLanesWithCardsAsync(int boardId)
    {
        Dictionary<Lane, List<Card>> laneCardHashMap = new Dictionary<Lane, List<Card>>();

        foreach (Lane lane in await _db.Lanes.ToListAsync())
        {
            if (lane.Board.Id == boardId)
            {
                List<Card> cards = await _db.Cards.Where(card => card.Lane.Id == lane.Id).ToListAsync();
                laneCardHashMap.Add(lane, cards);
            }
        }

        return laneCardHashMap;
    }
}
