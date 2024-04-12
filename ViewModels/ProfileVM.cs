using System.ComponentModel.DataAnnotations;

namespace Glimpse.ViewModels;

public class ProfileVM
{
    public IFormFile? PictureFile { get; set; }
    public string? PicturePath {get; set;}

    [Required(ErrorMessage = "Name is required.")]
    [Display(Name = "Name")]
    [MaxLength(50)]
    public string? Name { get; set; }
    
    public string? Email { get; set; }

    public bool DeleteAccount {get; set;}  
    // [Required(ErrorMessage = "Password is required.")]
    // [DataType(DataType.Password)]
    // public string? Password { get; set; }
    // [Compare("Password", ErrorMessage = "Passwords don't match.")]
    // [Display(Name = "Confirm Password")]
    // [DataType(DataType.Password)]
    // public string? ConfirmPassword { get; set; }

}