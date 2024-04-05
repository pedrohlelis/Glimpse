using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Lane
{
    private int _LaneId;
    private String? _LaneName;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LaneId {get{return _LaneId;} set{_LaneId = value;}}
    public String? LaneName {get{return _LaneName;} set{_LaneName = value;}}

    public virtual Board FkBoards {get; set;}
    public virtual List<Card> FkCards {get; set;}
}
