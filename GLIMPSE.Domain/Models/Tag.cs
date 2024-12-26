using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GLIMPSE.Domain.Models;

public class Tag : Base
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public int BoardId { get; set; }
    public virtual Board? Board { get; set; }
    public virtual ICollection<Card> Cards { get; } = [];
}