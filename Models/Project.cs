using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Project 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    public int ResponsibleUserId { get; set;}
    public string? ProjectName { get; set; }
    public string? CreationDate { get; set; }
    public string? ProjectDescription { get; set; }
    public bool IsActive { get; set; }
    public User User { get; set; }
}
