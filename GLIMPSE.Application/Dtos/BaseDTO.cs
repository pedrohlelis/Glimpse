
namespace GLIMPSE.Application.Dtos
{
    public class BaseDTO
    {
        public int? Id { get; set; }
        public string? CreatedById { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}