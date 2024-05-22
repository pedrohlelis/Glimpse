using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class CardTag
{
    public int CardId { get; set; }
    public int TagId { get; set; }
}