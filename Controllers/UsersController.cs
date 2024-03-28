using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.Migrations;

namespace Glimpse.Controllers;
public class UserController : Controller
{

    [HttpPost("AddUser")]
    public async Task<IActionResult> PostNewUser([FromForm] User newUser)
    {
        try
        {
            using (var _context = new GlimpseContext())
            {
                newUser.UserId = 0;
                newUser.IsActive = true;
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Ok("Usuario cadastrado com sucesso.");
            }
        }
        catch(Exception e)
        {
            return BadRequest("Ocorreu um erro ao cadastrar o usuario. " + e.Message);
        }
    }

    [HttpGet("UsersList")]
    public List<User> GetAllUsers()
    {
        using (var _context = new GlimpseContext())
        {
            List<User> item = _context.Users.Where(u => u.IsActive).ToList(); 
            return item;
        }
    }

    [HttpPut("UpdateUser/{id}")]
    public bool UpdateUser(int id, [FromForm] User userNewInfo)
    {
        using (var _context = new GlimpseContext())
        {
            var item = _context.Users.FirstOrDefault(t => t.UserId == id);
            
            item.UserName = userNewInfo.UserName;
            item.UserEmail = userNewInfo.UserEmail;
            item.UserPassword = userNewInfo.UserPassword;

            _context.SaveChanges();
            return true;
        }
    }

    [HttpDelete("RemoveUser/{id}")]
    public bool RemoveUser(int id)
    {
        using (var _context = new GlimpseContext())
        {
            var item = _context.Users.FirstOrDefault(t => t.UserId == id);

            item.IsActive = false;

            _context.SaveChanges();
            return true;
        }
    }
}