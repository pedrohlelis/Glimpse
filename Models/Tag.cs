using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Glimpse.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public virtual Board? Board { get; set; }
    [JsonIgnore]
    public virtual ICollection<Card> Cards { get; } = [];
}