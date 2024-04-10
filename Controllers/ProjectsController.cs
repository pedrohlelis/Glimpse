using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Migrations;
using Glimpse.Models;

namespace Glimpse.Controllers;

[Route("Glimpse/[controller]")]
[ApiController]
public class ProjectController : Controller
{
    private readonly GlimpseContext _db;

    public ProjectController()
    {
        _db = new GlimpseContext();
    }

    [HttpPost("CreateProject")]
    public async Task<IActionResult> AddProject([FromForm] Project project)
    {
        await using (_db)
        {
            _db.Projects.Add(project);
            _db.SaveChangesAsync();
            return Ok("Novo Projeto criado com sucesso.");
        }
    }

    [HttpGet("ProjectsList")]
    public async Task<IActionResult> GetAllProjects()
    {
        try
        {
            await using (_db)
            {
                List<Project> item = _db.Projects.Where(u => u.IsActive).ToList();
                return Ok(item);
            }
        }
        catch (Exception e)
        {
            return NotFound("Ocorreu um erro durante sua solicitacao. " + e.Message);
        }
    }

    [HttpGet("GetProject/{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        Project project = await _db.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

}
