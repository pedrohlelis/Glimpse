using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GLIMPSE.Domain.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int PictureId { get; set; }
    public BlobFile? Picture { get; set; }
    [MaxLength(255)]
    public virtual string? CreatedById { get; set; }
    [MaxLength(255)]
    public virtual string? ModifiedById { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime? ModifiedAt { get; set; }
    public virtual bool IsDeleted { get; set; }
    public virtual DateTime? DeletedAt { get; set; }
    public virtual ICollection<Card> Cards { get; set; } = [];
    public virtual ICollection<Role> Roles { get; } = [];
    public virtual ICollection<Project> Projects { get; } = [];
}