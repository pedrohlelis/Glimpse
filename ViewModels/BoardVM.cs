using Glimpse.Models;

namespace Glimpse.ViewModels;

public class BoardVM
{
    public User? User { get; set; }
    public List<User>? Members { get; set; }
    public Board? Board { get; set; }
    public Role? UserRole { get; set; }
    public List<Role>? ProjectRoles { get; set; } = new List<Role>();
    public Dictionary<User, Role>? UserRolesDictionary { get; set; }
    public User? ProjectResponsibleUser { get; set; }

    public class RoleEditInputModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public bool CanManageMembers { get; set; }
        public bool CanManageRoles { get; set; }
        public bool CanManageCards { get; set; }
        public bool CanManageTags { get; set; }
        public bool CanManageChecklist { get; set; }
    }
}