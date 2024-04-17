using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace Glimpse.ViewModels;

public class ProfileVM
{
    public IFormFile? ProfilePicFile { get; set; }
    public string? ProfilePicPath {get; set;}
    [Required(ErrorMessage = "Name is required.")]
    [Display(Name = "First Name")]
    [MaxLength(25)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [Display(Name = "Last Name")]
    [MaxLength(25)]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string? Email { get; set; }

}