using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WorkspaceGlimpse;
public class UserController
{
    [HttpPost("AddUser")]
    public bool PostNewUser([FromForm] User newUser)
    {
        using (var _context = new GlimpseContext())
        {
            newUser.UserId = 0;
            newUser.IsActive = true;
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
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