using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Team
{
    private int _TeamId;
    private int _FkProjectsProjectId;
    private List<User> _FkUsersUserId = [];

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TeamId {get{return _TeamId;} set{_TeamId = value;}}
    [Required]
    [ForeignKey("Projects")]
    public int FkProjectsProjectId {get{return _FkProjectsProjectId;} set{_FkProjectsProjectId = value;}}
    [Required]
    [ForeignKey("Users")]
    public List<User> FkUsersUserId {get{return _FkUsersUserId;} set{_FkUsersUserId = value;}}
}
