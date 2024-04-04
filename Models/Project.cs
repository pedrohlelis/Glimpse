using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models
{
    public class Project 
    {
        [Key]
        public int ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public DateOnly CreationDate { get; set; }
        public required string ProjectDescription { get; set; }
        public virtual List<Board>? Boards { get; set; }
        //public virtual User FkUserUserId { get; set; }
    }
}