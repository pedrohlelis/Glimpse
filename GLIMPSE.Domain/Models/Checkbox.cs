using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models;

public class Checkbox : Base
{
    public string? Name { get; set; }
    public bool Finished { get; set; }
    public int CardId { get; set; }
    public virtual Card? Card { get; set; }
}