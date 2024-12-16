using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Dtos
{
    public class BoardDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Background { get; set; }
        public int ProjectId { get; set; }
        public virtual ProjectDTO? Project { get; set; }
        public virtual ICollection<TagDTO> Tags { get; } = [];
        public virtual ICollection<LaneDTO> Lanes { get; } = [];
    }
}