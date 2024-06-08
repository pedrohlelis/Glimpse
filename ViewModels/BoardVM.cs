using System.Drawing;
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
    public List<Role> CanManageRoles {get; set;} = new List<Role>();
    public List<User> UnemployedUsers {get; set;} = new List<User>();
    public bool? IsMemberSideBarActive { get; set; }

    public class RoleEditInputModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Color? Color { get; set; }
        public bool CanManageMembers { get; set; }
        public bool CanManageRoles { get; set; }
        public bool CanManageCards { get; set; }
        public bool CanManageTags { get; set; }
        public bool CanManageChecklist { get; set; }
    }
}