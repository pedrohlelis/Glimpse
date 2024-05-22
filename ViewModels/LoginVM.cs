using System.ComponentModel.DataAnnotations;

namespace Glimpse.ViewModels;

public class LoginVM 
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string? Email {get; set;}

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password {get; set;}
    [Display(Name = "Remember Me")]
    public bool RememberMe {get; set;}
}