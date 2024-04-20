using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Project 
{
    [Key]
    public int Id { get; set; }
    public string? ResponsibleUserId { get; set; }
    public string? Name { get; set; }
    public DateOnly CreationDate { get; set; }
    public DateOnly LastEdited { get; set; }
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Board> Boards { get; set; } = [];
    public virtual ICollection<Role> Roles { get; set; } = [];
    public virtual ICollection<User> Users { get; set; } = [];
}