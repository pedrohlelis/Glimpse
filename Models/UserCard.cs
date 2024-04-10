using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class UserCard
{
    [Key]
    public int UserCardId { get; set; }
    public virtual User User { get; set; }
    public virtual Card Card { get; set; }
}