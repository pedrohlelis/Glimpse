using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Glimpse.Models;

public class Card 
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateOnly? Date { get; set; }
    public virtual Lane? Lane { get; set; }
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual ICollection<Tag> Tags { get; } = [];
    public virtual ICollection<Checkbox> Checkboxes { get; } = [];
}