using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Domain.Models
{
    public class Sprint : Base
    {
        public string? Name { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public virtual ICollection<Card> Cards { get; } = [];
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}
