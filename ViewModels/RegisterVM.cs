using System.ComponentModel.DataAnnotations;

namespace Glimpse.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Email is required.")]

    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Compare("Password", ErrorMessage = "Passwords don't match.")]
    public string? ConfirmPassword { get; set; }
    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}