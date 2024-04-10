using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class CardTag
{
    [Key]
    public int CardTagId { get; set; }
    public virtual Card Card { get; set; }
    public virtual Tag Tag { get; set; }
}