using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Dtos;

public class LaneDTO : BaseDTO
{
    public string? Name { get; set; }
    public int BoardId { get; set; }
    public virtual BoardDTO? Board { get; set; }
    public int Index { get; set; }
    public virtual ICollection<CardDTO> Cards { get; } = [];
}