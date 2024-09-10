using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GLIMPSE.Domain.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Picture { get; set; }
    public bool IsActive { get; set; }
    public virtual List<Card> Cards { get; set; } = new List<Card>();
    public virtual List<Role> Roles { get; } = [];
    public virtual List<Project> Projects { get; } = [];
}