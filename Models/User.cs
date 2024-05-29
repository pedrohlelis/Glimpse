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
    // card x user
    public virtual ICollection<Card> Cards { get; } = [];
    // cargo x user
    public virtual ICollection<Role> Roles { get; set; } = [];
    // projeto x user
    public virtual ICollection<Project> Projects { get; } = [];
}