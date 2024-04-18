using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public bool IsActive { get; set; }
    // card x user
    public ICollection<Card>? Cards { get; set; }
    // cargo x user 
    public ICollection<Role>? Roles { get; set; } 
    public virtual ICollection<Project> Projects { get; set; } = [];
}