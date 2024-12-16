using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Dtos;

public class CheckboxDTO : BaseDTO
{
    public string? Name { get; set; }
    public bool Finished { get; set; }
    public int CardId { get; set; }
    public virtual CardDTO? Card { get; set; }
}