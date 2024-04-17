using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using Glimpse.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Controllers;

[Authorize]
public class ProjectController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;

    public ProjectController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
    }

    // READ
    public async Task<IActionResult> MainProjects()
    {
        User user = _userManager.GetUserAsync(User).Result;

        if (user != null && user.IsActive == true)
        {
            ICollection<Project> ActiveProjects = [];
            foreach (Project project in user.Projects)
            {
                if (project.IsActive == true)
                {
                    ActiveProjects.Add(project);
                }
            }
            return View(await _db.Projects.ToListAsync());
        }
        return NotFound();
    }

    // CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project, IFormFile projectImg)
    {
        project.CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);
        project.IsActive = true;
        project.ResponsibleUserId = _userManager.GetUserAsync(User).Result.Id;
        project.Users.Add(_userManager.GetUserAsync(User).Result);

        if (ModelState.IsValid)
        {
            if (projectImg != null && projectImg.Length > 0)
            {
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "project-pictures");
                string nomeArquivo = new Guid() + "_" + Path.GetFileName(projectImg.FileName);
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await projectImg.CopyToAsync(stream);
                }
                project.Picture = "../project-pictures/" + nomeArquivo;
            } 
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return RedirectToAction("MainProjects");
        }

        return View("Create", project);
    }

    // UPDATE
    [HttpGet("/projects/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        Project Project = await _db.Projects.FindAsync(id);

        if (Project == null)
        {
            return NotFound();
        }

        return View(Project);
    }

    [HttpPost]
    public async Task<IActionResult> EditProject(Project Project)
    {
        if (ModelState.IsValid)
        {
            Project oldProject = await _db.Projects.FindAsync(Project.Id);
            _db.Entry(oldProject).CurrentValues.SetValues(Project);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ViewData["uniqueAlert"] = "Chassi do Project ja cadastrado";
                //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

                return View("Edit", Project);
            }
            return RedirectToAction("MainProjects");
        }

        return View("Edit", Project);
    }

    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        Project Project = await _db.Projects.FindAsync(id);

        if (Project == null)
        {
            return NotFound();
        }

        return View(Project);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarProject(Project project)
    {
        if (ModelState.IsValid)
        {
            
            Project currentProject = await _db.Projects.FindAsync(project.Id);
            _db.Entry(currentProject).CurrentValues.SetValues(project);
            await _db.SaveChangesAsync();

            return RedirectToAction("MainProjects");
        }

        return View("Delete", project);
    }
}
