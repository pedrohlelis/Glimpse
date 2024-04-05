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
    public virtual Project FkProjects {get; set;}
    public virtual List<Lane> FkLanes {get; set;}
}
