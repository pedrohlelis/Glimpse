using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GLIMPSE.Domain.Models;

public class Card : Base
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public virtual Lane? Lane { get; set; }
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual ICollection<Tag> Tags { get; } = [];
    public virtual ICollection<Checkbox> Checkboxes { get; } = [];
}