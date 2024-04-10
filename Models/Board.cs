using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Board
{
    private int _BoardId;
    private String? _BoardName;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BoardId {get{return _BoardId;} set{_BoardId = value;}}
    public String? BoardName {get{return _BoardName;} set{_BoardName = value;}}
    public virtual Project project {get; set;}
    public virtual List<Lane> lanes {get; set;}
}
