using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Project 
{
    [Key]
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string CreationDate { get; set; }
    public string ProjectDescription { get; set; }
    public bool ActiveProject { get; set; }
    public bool IsProjectActive()
    {
        return ActiveProject;
    }
}
