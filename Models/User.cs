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
    public virtual List<Card> Cards { get; set; } = new List<Card>();
    // cargo x user
    public virtual List<Role> Roles { get; } = [];
    // projeto x user
    public virtual List<Project> Projects { get; } = [];
}