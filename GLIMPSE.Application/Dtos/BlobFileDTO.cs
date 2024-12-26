using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Dtos;

public class BlobFileDTO : BaseDTO
{
    public string? Path { get; set; }
}