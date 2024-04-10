using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Card 
{
    [Key]
    public int CardId { get; set; }
    public string CardName { get; set; }
    public string CardDescription { get; set; }
    public virtual Lane Lane { get; set; }
}