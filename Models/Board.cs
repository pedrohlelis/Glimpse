using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Background { get; set; }
        public DateOnly CreationDate { get; set; }
        public bool IsActive { get; set; }
        public required string CreatorId { get; set; }
        public virtual required Project Project { get; set; }
    }
}