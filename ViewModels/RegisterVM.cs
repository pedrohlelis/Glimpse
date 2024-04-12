using System.ComponentModel.DataAnnotations;

namespace Glimpse.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "Name is required.")]
    [Display(Name = "Name")]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords don't match.")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}