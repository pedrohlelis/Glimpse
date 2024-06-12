using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="O campo não pode ser vazio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres.")]
        public string? Name { get; set; }
        public string? Background { get; set; }
        public DateOnly CreationDate { get; set; }
        public bool IsActive { get; set; }
        public string? CreatorId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<Tag> Tags { get; } = [];
        public virtual ICollection<Lane> Lanes { get; } = [];
    }
}