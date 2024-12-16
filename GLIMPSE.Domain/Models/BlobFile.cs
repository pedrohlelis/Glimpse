using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLIMPSE.Domain.Models
{
    public class BlobFile : Base
    {
        public string? Path { get; set; }
    }
}