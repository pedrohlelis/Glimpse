using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Glimpse.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePic { get; set; }
    public bool IsActive { get; set; }
    // card x user
    public ICollection<Card> Cards { get; } = [];
    // cargo x user
    public ICollection<Role> Roles { get; } = [];
    // projeto x user
    public ICollection<Project> Projects { get; } = [];
}