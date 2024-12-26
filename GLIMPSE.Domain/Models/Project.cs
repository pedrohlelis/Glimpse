using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models;

public class Project : Base
{
    public string? ResponsibleUserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? PictureId { get; set; }
    public BlobFile? Picture { get; set; }
    public virtual List<Repository> Repositories { get; } = [];
    public virtual List<Board> Boards { get; } = [];
    public virtual List<Role> Roles { get; } = [];
    public virtual List<User> Users { get; } = [];
    public virtual List<Sprint> Sprints { get; } = [];
}