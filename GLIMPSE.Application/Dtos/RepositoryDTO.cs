using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Application.Dtos;

public class RepositoryDTO : BaseDTO
{
    public string? Owner { get; set; }
    public string? RepoName { get; set; }
    public string? Token { get; set; }
    public int ProjectId { get; set; }
    public virtual required ProjectDTO Project { get; set; }
}