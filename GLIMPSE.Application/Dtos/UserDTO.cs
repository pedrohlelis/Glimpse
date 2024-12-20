using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GLIMPSE.Application.Dtos;

public class UserDTO : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int PictureId { get; set; }
    public BlobFileDTO? Picture { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<CardDTO> Cards { get; set; } = [];
    public virtual ICollection<RoleDTO> Roles { get; } = [];
    public virtual ICollection<ProjectDTO> Projects { get; } = [];
}