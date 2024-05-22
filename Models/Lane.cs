using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Lane
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual Board? Board { get; set; }
    public int Index { get; set; }
    public virtual ICollection<Card> Cards { get; } = [];
}