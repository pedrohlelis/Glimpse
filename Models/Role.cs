using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Role
{
    private int _RoleId;
    private int _FkTeamsTeamId;
    private int _FkUsersUserId;
    private String? _RoleName;
    private String? _RoleDescription;
    private String? _RoleColor;
    private bool _CanRemoveMembers;
    private bool _CanCreateRole;


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId {get{return _RoleId;} set{_RoleId = value;}}
    [ForeignKey("Teams")]
    public int FkTeamsTeamId {get{return _FkTeamsTeamId;} set{_FkTeamsTeamId = value;}}
    [ForeignKey("Users")]
    public int FkUsersUserId {get{return _FkUsersUserId;} set{_FkUsersUserId = value;}}
    public String? RoleName {get{return _RoleName;} set{_RoleName = value;}}
    public String? RoleDescription {get{return _RoleDescription;} set{_RoleDescription = value;}}
    public String? RoleColor {get{return _RoleColor;} set{_RoleColor = value;}}
    public bool CanRemoveMembers {get{return _CanRemoveMembers;} set{_CanRemoveMembers = value;}}
    public bool CanCreateRole {get{return _CanCreateRole;} set{_CanCreateRole = value;}}
}
