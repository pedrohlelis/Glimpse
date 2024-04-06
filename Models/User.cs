using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Glimpse.Models;

public class User : IdentityUser
{
    public String? ProfilePic {get; set;}
    public bool IsActive {get; set;}
}
