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

    public async Task<IActionResult> MainProjects()
    {
        string userId = _userManager.GetUserId(User);

        var user = _db.Users
            .Include(u => u.Projects)
                .ThenInclude(p => p.Boards)
            .Single(u => u.Id == userId);

        var userProjects = user.Projects;
        ICollection<Project> activeUserProjects = [];

        foreach(Project project in userProjects)
        {
            if(project.IsActive)
            {
                activeUserProjects.Add(project);
            }
        }
        ViewData["UserName"] = user.FirstName + " " + user.LastName;

        return View(activeUserProjects);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project, IFormFile projectImg)
    {
        project.CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);
        project.LastEdited = DateTime.UtcNow;
        project.IsActive = true;
        project.ResponsibleUserId = _userManager.GetUserId(User);
        
        var currentUser = await _userManager.GetUserAsync(User);

        project.Users.Add(currentUser);

        if (projectImg != null && projectImg.Length > 0)
        {
            string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "project-pictures");
            string nomeArquivo = Guid.NewGuid() + "_" + Path.GetFileName(projectImg.FileName);
            string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await projectImg.CopyToAsync(stream);
            }
            project.Picture = "../project-pictures/" + nomeArquivo;
        } else 
        {
            project.Picture = "../default-images/ProjectDefault.svg";
        }

        if (ModelState.IsValid)
        {
            _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            var DefaultPORole = new Role
            {
                Name = "Product Owner",
                Description = "This is the default Developer role created during project creation",
                Color = "#74B72E",
                CanManageMembers = true,
                CanManageCards = true,
                CanManageTags = true,
                CanManageChecklist = true,
                Project = project
            };
            var DefaultDevTeamRole = new Role
            {
                Name = "Developer",
                Description = "This is the default Developer role created during project creation",
                Color = "#FF1D8E",
                CanManageMembers = true,
                CanManageCards = true,
                CanManageTags = true,
                CanManageChecklist = true,
                Project = project
            };
            project.Roles.Add(DefaultPORole);
            project.Roles.Add(DefaultDevTeamRole);
            var user = await _db.Users
                .Include(u => u.Projects)
                .SingleAsync(u => u.Id == project.ResponsibleUserId);
            user.Roles.Add(DefaultPORole);
            user.Projects.Add(project);
            await _db.SaveChangesAsync();

            return RedirectToAction("MainProjects");
        }

        return View("Create", project);
    }

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
    public async Task<IActionResult> EditProject(Project Project, IFormFile? projectImg)
    {
        if (projectImg != null && projectImg.Length > 0)
        {
            string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "project-pictures");
            string nomeArquivo = Guid.NewGuid() + "_" + Path.GetFileName(projectImg.FileName);
            string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await projectImg.CopyToAsync(stream);
            }
            Project.Picture = "../project-pictures/" + nomeArquivo;
        } else 
        {
            Project.Picture = "../default-images/ProjectDefault.png";
        }

        if (ModelState.IsValid)
        {
            Project oldProject = await _db.Projects.FindAsync(Project.Id);

            _db.Entry(oldProject).CurrentValues.SetValues(Project);
            await _db.SaveChangesAsync();

            return RedirectToAction("MainProjects");
        }

        return View("Edit", Project);
    }

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
    public async Task<IActionResult> DeleteProject(Project project)
    {
        if (ModelState.IsValid)
        {
            project.IsActive = false;
            Project currentProject = await _db.Projects.FindAsync(project.Id);
            _db.Entry(currentProject).CurrentValues.SetValues(project);
            await _db.SaveChangesAsync();

            return RedirectToAction("MainProjects");
        }

        return View("Delete", project);
    }

    public async Task<IActionResult> Users(int projectId)
    {
        Project project = _db.Projects
            .Include(p => p.Users)
                .ThenInclude(u => u.Roles)
            .Include(p => p.Roles.Where(r => r.Project.Id == projectId))
            .Single(p => p.Id == projectId);

        if (project == null)
        {
            return NotFound();
        }

        ICollection<User> users = [];
        User toAddUser = new();
        foreach (User user in project.Users)
        {
            toAddUser = user;
            users.Add(toAddUser);
        }
        Project viewProject = project;
        viewProject.Users = users;

        return View(viewProject);
    }

    public async Task<IActionResult> AddUser(int projectId, string userEmail)
    {
        Project project = await _db.Projects.FindAsync(projectId);
        if (project == null)
        {
            return NotFound("Projeto não existe");
        }
        var user = await _userManager.FindByNameAsync(userEmail);
        if (project == null)
        {
            return NotFound("Email digitado não pertence a nenhum usuário");
        }
        project.Users.Add(user);
        await _db.SaveChangesAsync();

        return RedirectToAction("Users", new { projectId });
    }

}
