using System.ComponentModel.DataAnnotations;

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

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid Phone number.")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    public bool DeleteAccount {get; set;}  
    // [Required(ErrorMessage = "Password is required.")]
    // [DataType(DataType.Password)]
    // public string? Password { get; set; }
    // [Compare("Password", ErrorMessage = "Passwords don't match.")]
    // [Display(Name = "Confirm Password")]
    // [DataType(DataType.Password)]
    // public string? ConfirmPassword { get; set; }

}