using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Application.Dtos;

public class RoleDTO : BaseDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public virtual ICollection<UserDTO> Users { get; } = [];
    public int ProjectId { get; set; }
    public virtual ProjectDTO? Project { get; set; }
    public bool CanManageMembers { get; set; }
    public bool CanManageRoles { get; set; }
    public bool CanManageCards { get; set; }
    public bool CanManageTags { get; set; }
    public bool CanManageChecklist { get; set; }

}
