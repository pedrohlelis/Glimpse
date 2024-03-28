using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;

namespace Glimpse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly GlimpseContext _db;
        
        public ProjectController(GlimpseContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Project> projects = await _db.Projects.ToListAsync();
            return Ok(projects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo(int id)
        {
            Project project = await _db.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
        [HttpPost]
        public IActionResult AddProject([FromBody] Project project)
        {
            using (_db)
            {
                _db.Projects.Add(project);
                _db.SaveChanges();
                return Ok(project);
            }
        }
    }
}