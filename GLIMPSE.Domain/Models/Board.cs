using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models
{
    public class Board : Base
    {
        [Required(ErrorMessage="O campo não pode ser vazio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres.")]
        public string? Name { get; set; }
        public string? Background { get; set; }
        public string? CreatorId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<Tag> Tags { get; } = [];
        public virtual ICollection<Lane> Lanes { get; } = [];
    }
}