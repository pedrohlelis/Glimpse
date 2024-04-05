using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glimpse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Controllers;

public class ProjectController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProjectController(GlimpseContext db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
    }

    // READ
    public async Task<IActionResult> MainProjects()
    {
        return View(await _db.Projects.ToListAsync());
    }

    public async Task<IActionResult> GetInfo(int id)
    {
        var Project = await _db.Projects.FindAsync(id);

        if (Project == null)
        {
            return NotFound();
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

        return View(Project);
    }

    // CREATE
    public IActionResult Create()
    {
        //ViewData["Filiais"] = _db.Filiais.ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project, IFormFile projectImg)
    {
        project.CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);

        if (ModelState.IsValid)
        {
            if (projectImg != null && projectImg.Length > 0)
            {
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                string nomeArquivo = Guid.NewGuid().ToString() + "_" + Path.GetFileName(projectImg.FileName);
                string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await projectImg.CopyToAsync(stream);
                }
                project.ProjectPicture = "~/uploads/" + nomeArquivo;
            } 
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            return RedirectToAction("ProjectBoards");
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

        return View("Create", project);
    }

    // UPDATE
    public async Task<IActionResult> Edit(int id)
    {
        var Project = await _db.Projects.FindAsync(id);

        if (Project == null)
        {
            return NotFound();
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

        return View(Project);
    }

    [HttpPost]
    public async Task<IActionResult> EditarProject(Project Project)
    {
        if (ModelState.IsValid)
        {
            //var ProjectAntigo = await _db.Projects.FindAsync(Project.CodProject);
            //_db.Entry(ProjectAntigo).CurrentValues.SetValues(Project);
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
            return RedirectToAction("Get");
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

        return View("Edit", Project);
    }

    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var Project = await _db.Projects.FindAsync(id);

        if (Project == null)
        {
            return NotFound();
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

        return View(Project);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarProject(Project project)
    {
        if (ModelState.IsValid)
        {
            //var item = await _db.Projects.FindAsync(project.CodProject);
            //_db.Projects.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction("Get");
        }

        return View("Delete", project);
    }
}
