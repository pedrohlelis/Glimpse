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
    public async Task<IActionResult> ShowRoles(int projectId)
    {
        ICollection<Role> roles;
        
        System.Console.WriteLine(projectId);

        var project = _db.Projects
            .Include(p => p.Roles)
            .Single(p => p.Id == projectId);
        roles = project.Roles;

        ViewData["projectId"] = projectId;
        
        return View(roles);
    }

    // CREATE
    public IActionResult Create(int projectId)
    {
        // Lista de todos os projetos
        ViewData["projectId"] = projectId;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(Role role, int boardId, int projectId)
    {

        Project project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
        role.Project = project;
        // Lista de todos os roles
        _db.Roles.Add(role);
        project.Roles.Add(role);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }

    // UPDATE
    public async Task<IActionResult> Edit(Role role)
    {
        
        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(Role role, int boardId)
    {
        // Retrieve the existing role from the database
        Role toEditRole = await _db.Roles.FindAsync(role.Id);

        // Update the role properties with the submitted form data
        toEditRole.Name = role.Name;
        toEditRole.Description = role.Description;
        toEditRole.Color = role.Color;
        toEditRole.CanManageMembers = role.CanManageMembers;
        toEditRole.CanManageRoles = role.CanManageRoles;
        toEditRole.CanManageCards = role.CanManageCards;
        toEditRole.CanManageTags = role.CanManageTags;
        toEditRole.CanManageChecklist = role.CanManageChecklist;

        Console.WriteLine(role.CanManageMembers);

        // Save changes to the database
        await _db.SaveChangesAsync();

        // Redirect to the appropriate action with the boardId parameter
        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId });
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> DeleteRole(int roleToDeleteId, int id)
    {
        Role role = await _db.Roles.FindAsync(roleToDeleteId);
        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();
        return RedirectToAction("GetBoardInfo", "Board", new { id = id });
    }

    // [HttpPost]
    // public async Task<IActionResult> DeleteRole(Role role)
    // {
    //     Role toDeleteRole = await _db.Roles.FindAsync(role.Id);
    //     _db.Roles.Remove(toDeleteRole);
    //     await _db.SaveChangesAsync();

    //     return RedirectToAction("Get");
    // }

    public async Task<IActionResult> Assign(int roleId, int projectId)
    {
        Role role = _db.Roles.Find(roleId);
        Project project = _db.Projects
            .Include(p => p.Users)
            .Single(p => p.Id == projectId);

        ICollection<User> projectUsers = [];
        foreach (User user in project.Users)
        {
            if (user.IsActive)
            {
                projectUsers.Add(user);
            }
        }

        ViewData["projectUsers"] = projectUsers;

        return View(role);
    }

    public async Task<IActionResult> AtribuirRole(int roleId, string userId)
    {
        Role role = _db.Roles
            .Include(r => r.Project)
            .Single(r => r.Id == roleId);
        User user = _db.Users
            .Include(u => u.Roles)
            .Single(u => u.Id == userId);
        int projectId = role.Project.Id;
        try{
            var existingUserRoleInProject = user.Roles.FirstOrDefault(ur => ur.Project.Id == role.Project.Id);
            user.Roles.Remove(existingUserRoleInProject);
            user.Roles.Add(role);
            await _db.SaveChangesAsync();
        }catch (Exception e)
        {
            Console.WriteLine(e.Message);
            user.Roles.Add(role);
            await _db.SaveChangesAsync();
        }
        
        // if (existingUserRoleInProject != null)
        // {
        //     user.Roles.Remove(existingUserRoleInProject);
        //     user.Roles.Add(role);
        // }
        // else
        // {
        //     // Adiciona o novo papel ao usuário
        //     user.Roles.Add(role);
        // }
        return RedirectToAction("Users", "Project", new { projectId });
    }

    public async Task<IActionResult> UserRoles(int projectId)
    {
        ICollection<Role> roles;
        
        System.Console.WriteLine(projectId);

        var project = _db.Projects
            .Include(p => p.Roles)
            .Include(r => r.Roles)
            .Single(p => p.Id == projectId);
        roles = project.Roles;

        ViewData["projectId"] = projectId;
        
        return View(roles);
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
