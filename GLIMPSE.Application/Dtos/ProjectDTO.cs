using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Dtos;

public class ProjectDTO : BaseDTO
{
    public string? ResponsibleUserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int PictureId { get; set; }
    public BlobFileDTO? Picture { get; set; }
    public virtual ICollection<RepositoryDTO> Repositories { get; set; } = [];
    public virtual ICollection<BoardDTO> Boards { get; set; } = [];
    public virtual ICollection<RoleDTO> Roles { get; set; } = [];
    public virtual ICollection<UserDTO> Users { get; set; } = [];
    public virtual ICollection<SprintDTO> Sprints { get; set; } = [];
}