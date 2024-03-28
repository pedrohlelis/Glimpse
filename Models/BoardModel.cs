using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        public string? BackgroundImage { get; set; }
        public DateOnly CreationDate { get; set; }
        [ForeignKey("Project")]
        public string FkProjectProjectId { get; set; }
    }
}