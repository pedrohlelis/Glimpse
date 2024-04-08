using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Glimpse.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? FirstName { get; set;}
    [PersonalData]
    [MaxLength(50)]
    public string? LastName { get; set;}
    public string? ProfilePic {get; set;}
    public bool IsActive {get; set;}
}
