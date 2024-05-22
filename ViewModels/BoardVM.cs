using Glimpse.Models;

namespace Glimpse.ViewModels;

public class BoardVM
{
    public User? User { get; set; }
    public Board? Board { get; set; }
    public Role? UserRole { get; set; }
    public List<Role>? ProjectRoles { get; set; } = new List<Role>();
    public User? ProjectResponsibleUser { get; set; }
}