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

    public ProjectController(GlimpseContext db)
    {
        _db = db;
    }

    // READ
    public async Task<IActionResult> MainProjectsPage()
    {
        List<Project> projects = await _db.Projects.ToListAsync();
        return View(projects);
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
    public async Task<IActionResult> CriarProject(Project Project)
    {
        if (ModelState.IsValid)
        {
            _db.Projects.Add(Project);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ViewData["uniqueAlert"] = "Chassi do Project ja cadastrado";
                //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

                return View("Create", Project);
            }
            return RedirectToAction("Get");
        }
        //ViewData["Filiais"] = await _db.Filiais.ToListAsync();

        return View("Create", Project);
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
