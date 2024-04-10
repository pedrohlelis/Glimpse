using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Lane
{
    [Key]
    public int LaneId { get; set; }
    public string LaneName { get; set; }
    public virtual required Board Board { get; set; }
    public int LaneIndex { get; set; }
}