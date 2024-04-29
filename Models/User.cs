using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Picture { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Card>? Cards { get; set; } = [];
    public virtual ICollection<Role>? Roles { get; set; } = [];
    public virtual ICollection<Project> Projects { get; set; } = [];
}