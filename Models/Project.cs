using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Project 
{
    [Key]
    public int ProjectId { get; set; }
    public int ResponsibleUserId { get; set;}
    public string? ProjectName { get; set; }
    public DateOnly CreationDate { get; set; }
    public string? ProjectDescription { get; set; }
    public string? ProjectPicture { get; set; }
    public bool IsActive { get; set; }
}