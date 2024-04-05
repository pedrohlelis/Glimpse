using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Team
{
    public int _FkUsersUserId;
    private int _FkProjectsProjectId;
    private int? _FkRolesRoleId;

    public int FkProjects {get{return _FkProjectsProjectId;} set{_FkProjectsProjectId = value;}}
    public virtual Project Project { get; set; }
    public int FkUsers {get{return _FkUsersUserId;} set{_FkUsersUserId = value;}}
    public virtual User User { get; set; }
    
    public int? FkRoles { get{return _FkRolesRoleId; } set{ _FkRolesRoleId = value;}}
    public virtual Role? Role { get; set; }

}
