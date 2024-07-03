using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Repository
{
    [Key]
    public int Id { get; set; }
    public string Owner { get; set; }
    public string RepoName { get; set; }
    public string Token { get; set; }
    public virtual Project Project { get; set; }
}