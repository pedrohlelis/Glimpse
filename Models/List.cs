using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;
    public class List
    {
        [Key]
        public int ListId { get; set; }

        [Required]
        [StringLength(50)]
        public string ListName { get; set; }

        [Required]
        [StringLength(50)]
        public string ListColor { get; set; }

        //[ForeignKey("Project")]
        //public int FkProjectProjectId { get; set; }
    }
