using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.Migrations;

namespace Glimpse.Controllers;
public class RoleController : Controller
{

    [HttpPost("CreateRole")]
    public async Task<IActionResult> PostNewRole([FromForm] Role newRole)
    {
        try
        {
            await using (var _context = new GlimpseContext())
            {
                newRole.RoleId = 0;
                _context.Roles.Add(newRole);
                _context.SaveChanges();
                return Ok("Cargo criado com sucesso.");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Ocorreu um erro ao cadastrar o usuario. " + e.Message);
        }
    }

    [HttpGet("RolesList")]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            await using (var _context = new GlimpseContext())
            {
                List<Role> item = _context.Roles.ToList();
                return Ok(item);
            }
        }
        catch (Exception e)
        {
            return NotFound("Ocorreu um erro durante sua solicitacao. " + e.Message);
        }
    }

    [HttpPut("UpdateRole/{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromForm] Role roleNewInfo)
    {
        try
        {
            await using (var _context = new GlimpseContext())
            {
                var item = _context.Roles.FirstOrDefault(t => t.RoleId == id);

                int aux = item.RoleId;
                item = roleNewInfo;
                item.RoleId = aux;

                _context.SaveChanges();
                return Ok("Os dados do usuario foram atualizados.");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Ocorreu um erro durante sua requisição. " + e.Message);
        }
    }

    [HttpDelete("RemoveRole/{id}")]
    public async Task<IActionResult> RemoveRole(int id)
    {
        try
        {
            await using (var _context = new GlimpseContext())
            {
                var item = _context.Users.FirstOrDefault(t => t.UserId == id);

                _context.Users.Remove(item);

                _context.SaveChanges();
                return Ok("Cargo removido com sucesso.");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Erro ao remover cargo. " + e.Message);
        }
    }
}