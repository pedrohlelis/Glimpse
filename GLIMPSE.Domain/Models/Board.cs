using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models
{
    public class Board : Base
    {
        public string? Name { get; set; }
        public string? Background { get; set; }
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<Tag> Tags { get; } = [];
        public virtual ICollection<Lane> Lanes { get; } = [];
        public 
    }
}