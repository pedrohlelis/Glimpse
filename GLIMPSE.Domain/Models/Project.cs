using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models;

public class Project : Base
{
    public string? ResponsibleUserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int PictureId { get; set; }
    public BlobFile? Picture { get; set; }
    public virtual List<Repository> Repositories { get; set; } = [];
    public virtual List<Board> Boards { get; set; } = [];
    public virtual List<Role> Roles { get; set; } = [];
    public virtual List<User> Users { get; set; } = [];
    public virtual List<Sprint> Sprints { get; set; } = [];
}