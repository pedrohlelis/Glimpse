using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;

namespace Glimpse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly GlimpseContext _db;
        
        public BoardController(GlimpseContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Board> Boards = await _db.Boards.ToListAsync();
            return Ok(Boards);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo(int id)
        {
            Board Board = await _db.Boards.FindAsync(id);

            if (Board == null)
            {
                return NotFound();
            }

            return Ok(Board);
        }
        [HttpPost]
        public IActionResult AddBoard([FromBody] Board Board)
        {
            using (_db)
            {
                _db.Boards.Add(Board);
                _db.SaveChanges();
                return Ok(Board);
            }
        }
    }
}