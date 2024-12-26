using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models;

public class Lane : Base
{
    public string? Name { get; set; }
    public int? BoardId { get; set; }
    public virtual Board? Board { get; set; }
    public int Index { get; set; }
    public virtual List<Card> Cards { get; } = [];
}