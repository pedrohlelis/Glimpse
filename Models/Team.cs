using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Team
{
    private int _FkUsersUserId;
    private int _FkProjectsProjectId;
    private int? _FkRolesRoleId;

    public int FkProjectsProjectId {get{return _FkProjectsProjectId;} set{_FkProjectsProjectId = value;}}
    public virtual Project Project { get; set; }
    public int FkUsersUserId {get{return _FkUsersUserId;} set{_FkUsersUserId = value;}}
    public virtual User User { get; set; }
    
    public int? FkRolesRoleId { get{return _FkRolesRoleId; } set{ _FkRolesRoleId = value;}}
    public virtual Role Role { get; set; }

}
