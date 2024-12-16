using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Dtos;

public class CardDTO : BaseDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public int LaneId { get; set; }
    public virtual LaneDTO? Lane { get; set; }
    public string? UserId { get; set; }
    public virtual UserDTO? User { get; set; }
    public virtual ICollection<TagDTO> Tags { get; } = [];
    public virtual ICollection<CheckboxDTO> Checkboxes { get; } = [];
}