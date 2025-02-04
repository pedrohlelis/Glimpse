using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GLIMPSE.Domain.Models;

public class User : IdentityUser
{
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? PictureId { get; set; }
    public BlobFile? Picture { get; set; }
    public virtual IList<Card> Cards { get; } = [];
    public virtual IList<Role> Roles { get; } = [];
    public virtual IList<Project> Projects { get; } = [];
    public virtual IList<Invite> Invites { get; } = [];
}