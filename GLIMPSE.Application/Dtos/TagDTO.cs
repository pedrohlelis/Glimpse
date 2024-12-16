using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GLIMPSE.Application.Dtos;

public class TagDTO : BaseDTO
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public int BoardId { get; set; }
    public virtual BoardDTO? Board { get; set; }
    public virtual ICollection<CardDTO> Cards { get; } = [];
}