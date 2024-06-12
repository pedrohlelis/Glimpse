using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Checkbox
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Finished { get; set; }
    public virtual Card? Card { get; set; }
}