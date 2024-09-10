using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models;

public class Repository : Base
{
    public string? Owner { get; set; }
    public string? RepoName { get; set; }
    public string? Token { get; set; }
    public virtual required Project Project { get; set; }
}