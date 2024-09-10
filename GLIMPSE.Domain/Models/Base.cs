using System.ComponentModel.DataAnnotations;

namespace GLIMPSE.Domain.Models
{
    public class Base
    {
        [Key]
        public virtual int Id { get; set; }
        [MaxLength(255)]
        public virtual string? CreatedById { get; set; }
        [MaxLength(255)]
        public virtual string? ModifiedById { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? ModifiedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletedAt { get; set; }

    }
}
