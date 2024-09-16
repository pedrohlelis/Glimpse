using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.Infrastructure.Data.Context;

namespace GLIMPSE.API.Controllers;

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
    public async Task<IActionResult> CreateRole(Role role, int boardId, int projectId, bool IsMemberSideBarActive)
    {

        Project project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
        role.Project = project;
        // Lista de todos os roles
        _db.Roles.Add(role);
        project.Roles.Add(role);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive = IsMemberSideBarActive});
    }

    // // UPDATE
    // public async Task<IActionResult> Edit(Role role)
    // {
        
    //     return View(role);
    // }

    [HttpPost]
    public async Task<IActionResult> EditRole(Role role, int roleId, int boardId, bool IsMemberSideBarActive)
    {
        Role toEditRole = await _db.Roles.FindAsync(roleId);

        toEditRole.Name = role.Name;
        toEditRole.Description = role.Description;
        toEditRole.Color = role.Color;
        toEditRole.CanManageMembers = role.CanManageMembers;
        toEditRole.CanManageRoles = role.CanManageRoles;
        toEditRole.CanManageCards = role.CanManageCards;
        toEditRole.CanManageTags = role.CanManageTags;
        toEditRole.CanManageChecklist = role.CanManageChecklist;

        await _db.SaveChangesAsync();

        return RedirectToAction("GetBoardInfo", "Board", new { id = boardId, IsMemberSideBarActive = IsMemberSideBarActive });
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> DeleteRole(int roleToDeleteId, int id, bool IsMemberSideBarActive)
    {
        Role role = await _db.Roles.FindAsync(roleToDeleteId);
        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();
        return RedirectToAction("GetBoardInfo", "Board", new { id = id, IsMemberSideBarActive = IsMemberSideBarActive });
    }

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

    [HttpPost]
    public async Task<IActionResult> AtribuirRole(int id ,int roleId, string userId)
    {
        Board board = _db.Boards
            .Include(b => b.Project)
            .Single(b => b.Id == id);
        User user = _db.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Project)
            .Single(u => u.Id == userId);
        Role currentUserRoleInProject = user.Roles.FirstOrDefault(r => r.Project.Id == board.Project.Id);
        if (roleId == 0)
        {
            if (currentUserRoleInProject == null){
                return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
            }
            user.Roles.Remove(currentUserRoleInProject);
            currentUserRoleInProject.Users.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
        }

        Role role = _db.Roles
            .Include(r => r.Project)
            .Single(r => r.Id == roleId);

        if(currentUserRoleInProject == null)
        {
            user.Roles.Add(role);
            role.Users.Add(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
        }
        else
        {
            user.Roles.Remove(currentUserRoleInProject);
            currentUserRoleInProject.Users.Remove(user);
            user.Roles.Add(role);
            role.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction("GetBoardInfo", "Board", new { id, IsMemberSideBarActive = true });
    }

    public async Task<IActionResult> UserRoles(int projectId)
    {
        ICollection<Role> roles;

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

}
