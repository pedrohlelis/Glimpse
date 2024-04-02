using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class User
{
    private int _UserId;
    private String? _UserName;
    private String? _UserEmail;
    private String? _UserPassword;
    private String? _ProfilePic;
    private bool _IsActive;
    private List<Project> _projects = [];

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId {get{return _UserId;} set{_UserId = value;}}
    public String? UserName {get{return _UserName;} set{_UserName = value;}}
    public String? UserEmail {get{return _UserEmail;} set{_UserEmail = value;}}
    public String? UserPassword {get{return _UserPassword;} set{_UserPassword = value;}}
    public String? ProfilePic {get{return _ProfilePic;} set{_ProfilePic = value;}}
    public bool IsActive {get{return _IsActive;} set{_IsActive = value;}}
    public List<Project> Projects { get{return _projects; } set{ _projects = value;}}
    public List<Role> Roles {get; set;}
}
