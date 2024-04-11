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
    public ICollection<User> Users { get; } = [];
    public ICollection<Tag> Tags { get; } = [];
    public ICollection<Checkbox> Checkboxes { get; } = [];
}