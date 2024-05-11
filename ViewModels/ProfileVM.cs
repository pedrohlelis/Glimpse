using System.ComponentModel.DataAnnotations;


namespace Glimpse.ViewModels;

public class ProfileVM
{
    public IFormFile? PictureFile { get; set; }
    public string? PicturePath {get; set;}

    [Required(ErrorMessage = "First Name is required.")]
    [Display(Name = "First Name")]
    [MaxLength(25)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required.")]
    [Display(Name = "Last Name")]
    [MaxLength(25)]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email.")]
    public string? Email { get; set; }

}