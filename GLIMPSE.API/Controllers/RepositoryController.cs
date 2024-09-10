using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.DOMAIN.ViewModels;

namespace GLIMPSE.API.Controllers;

[Authorize]
public class RepositoryController : Controller
{
    private readonly GlimpseContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly UserManager<User> _userManager;
    private readonly GitHubService _gitHubService;

    public RepositoryController(GlimpseContext db, IWebHostEnvironment hostEnvironment, UserManager<User> userManager, GitHubService gitHubService)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
        _userManager = userManager;
        _gitHubService = gitHubService;
    }

    public IActionResult GetRepositories(int projectId)
    {
        var projectRepos = _db.Projects
            .Include(p => p.Repositories)
            .SingleOrDefault(p => p.Id == projectId);

        return View(projectRepos);
    }
    public IActionResult ConnectRepository(int projectId)
    {
        return View(new ConnectRepositoryViewModel
        {
            ProjectId = projectId
        });
    }

    [HttpPost]
    public async Task<IActionResult> ConnectRepository(ConnectRepositoryViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var project = _db.Projects
                .Include(p => p.Repositories)
                .Single(p => p.Id == viewModel.ProjectId);

            var repository = new Repository
            {
                Owner = viewModel.Owner,
                RepoName = viewModel.RepoName,
                Token = viewModel.Token,
                Project = project
            };

            _db.Repositories.Add(repository);
            await _db.SaveChangesAsync();
            
            project.Repositories.Add(repository);
            await _db.SaveChangesAsync();

            return RedirectToAction("MainProjects", "Project");
        }

        return View(viewModel);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Repository repository)
    {
        if (ModelState.IsValid)
        {
            _db.Add(repository);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        return View(repository);
    }

    public async Task<IActionResult> Commits(int id)
    {
        var repo = await _db.Repositories.FindAsync(id);
        if (repo == null) return NotFound();

        var commits = await _gitHubService.GetCommits(repo.Owner, repo.RepoName, repo.Token);
        return View(commits);
    }

    public async Task<IActionResult> Branches(int id)
    {
        var repo = await _db.Repositories.FindAsync(id);
        if (repo == null) return NotFound();

        var branches = await _gitHubService.GetBranches(repo.Owner, repo.RepoName, repo.Token);
        return View(branches);
    }

    public async Task<IActionResult> Code(int id)
    {
        var repo = await _db.Repositories.FindAsync(id);
        if (repo == null) return NotFound();
        ViewData["RepoName"] = repo.RepoName;

        return View(id);
    }
    [HttpGet]
    public async Task<IActionResult> GetRepositoryStructure(int id, string path = "")
    {
        var repo = await _db.Repositories.FindAsync(id);
        if (repo == null) return NotFound();

        var content = await _gitHubService.GetAllRepositoryContent(repo.Owner, repo.RepoName, repo.Token, path);
        return Json(content);
    }
    [HttpGet]
    public async Task<IActionResult> GetFileContent(int id, string path)
    {
        var repo = await _db.Repositories.FindAsync(id);
        if (repo == null) return NotFound();

        var content = await _gitHubService.GetFileContent(repo.Owner, repo.RepoName, path, repo.Token);
        return Content(content);
    }
}