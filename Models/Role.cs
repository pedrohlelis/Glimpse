using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Glimpse.Models;

public class Role
{
    [Key]
    public int id {get; set;}
    public string? Name {get; set;}
}
