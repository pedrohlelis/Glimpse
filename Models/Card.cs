using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Card 
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Index { get; set; }
    public virtual Lane? Lane { get; set; }
    public virtual ICollection<User> Users { get; } = [];
    public virtual ICollection<Tag> Tags { get; } = [];
    public virtual ICollection<Checkbox> Checkboxes { get; } = [];
}