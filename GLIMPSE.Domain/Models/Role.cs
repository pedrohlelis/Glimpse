using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models;

public class Role : Base
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public virtual ICollection<User> Users { get; } = [];
    public virtual Project? Project { get; set; }
    public bool CanManageMembers { get; set; }
    public bool CanManageRoles { get; set; }
    public bool CanManageCards { get; set; }
    public bool CanManageTags { get; set; }
    public bool CanManageChecklist { get; set; }

}
