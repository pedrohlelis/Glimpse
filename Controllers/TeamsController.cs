using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.Migrations;

namespace Glimpse.Controllers;
public class TeamController : Controller
{

    [HttpPost("CreateTeam")]
    public async Task<IActionResult> PostNewTeam([FromForm] Team newTeamMember)
    {
        try
        {
            await using (var _context = new GlimpseContext())
            {
                _context.Teams.Add(newTeamMember);
                _context.SaveChanges();
                return Ok("Usuario adicionado ao projeto com sucesso.");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Ocorreu um erro ao cadastrar o usuario no Projeto. " + e.Message);
        }
    }

    [HttpGet("TeamsList")]
    public async Task<IActionResult> GetAllTeams()
    {
        try
        {
            await using (var _context = new GlimpseContext())
            {
                List<Team> item = _context.Teams.ToList();
                return Ok(item);
            }
        }
        catch (Exception e)
        {
            return NotFound("Ocorreu um erro durante sua solicitacao. " + e.Message);
        }
    }

    // [HttpPut("UpdateTeam/{id}")]
    // public async Task<IActionResult> UpdateRole(int id, [FromForm] Role roleNewInfo)
    // {
    //     try
    //     {
    //         await using (var _context = new GlimpseContext())
    //         {
    //             var item = _context.Roles.FirstOrDefault(t => t.RoleId == id);

    //             int aux = item.RoleId;
    //             item = roleNewInfo;
    //             item.RoleId = aux;

    //             _context.SaveChanges();
    //             return Ok("Os dados do usuario foram atualizados.");
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest("Ocorreu um erro durante sua requisição. " + e.Message);
    //     }
    // }

    // [HttpDelete("RemoveMemberFromTeam")]
    // public async Task<IActionResult> RemoveRole(int userId, int projectId)
    // {
    //     try
    //     {
    //         await using (var _context = new GlimpseContext())
    //         {
    //             var item = _context.Users.FirstOrDefault(t => t.UserId == id);

    //             _context.Users.Remove(item);

    //             _context.SaveChanges();
    //             return Ok("Cargo removido com sucesso.");
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest("Erro ao remover cargo. " + e.Message);
    //     }
    // }
}