using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Checkbox
{
    [Key]
    public int CheckboxId { get; set; }
    public string CheckboxName { get; set; }
    public bool Finished { get; set; }
    public virtual Card Card { get; set; }
}