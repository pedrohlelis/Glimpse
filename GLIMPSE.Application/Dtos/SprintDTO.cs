using GLIMPSE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Application.Dtos
{
    public class SprintDTO : BaseDTO
    {
        public string? Name { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public virtual ICollection<CardDTO> Cards { get; } = [];
        public int ProjectId { get; set; }
        public virtual ProjectDTO? Project { get; set; }
    }
}
