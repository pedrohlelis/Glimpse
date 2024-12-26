using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GLIMPSE.Domain.Models;

public class User : Base
{
    [MaxLength(50)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int? PictureId { get; set; }
    public BlobFile? Picture { get; set; }
    public virtual List<Card> Cards { get; } = [];
    public virtual List<Role> Roles { get; } = [];
    public virtual List<Project> Projects { get; } = [];
}