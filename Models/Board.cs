using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        public required string BoardName { get; set; }
        public string? BackgroundImage { get; set; }
        public DateOnly CreationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual required Project Project { get; set; }
        //public virtual User FkUserUserId { get; set; }
    }
}