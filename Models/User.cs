using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Picture { get; set; }
    public bool IsActive { get; set; }
    // card x user
    public ICollection<Card> Cards { get; } = [];
    // cargo x user
    public ICollection<Role> Roles { get; } = [];
    // projeto x user
    public ICollection<Project> Projects { get; } = [];
}