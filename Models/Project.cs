using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Project 
{
    [Key]
    public int ProjectId { get; set; }
    [Required(ErrorMessage = "O nome do projeto é obrigatório")]
    public string ProjectName { get; set; }
    public DateOnly CreationDate { get; set; }
    [Required(ErrorMessage = "A descrição do projeto é obrigatória")]
    public string ProjectDescription { get; set; }
    public string? ProjectPicture { get; set; }
    //public virtual User FkUserUserId { get; set; }
}