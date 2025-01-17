using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Domain.Models
{
    public class Invite : Base
    {
        public int? ReceiverId { get; set; }
        public virtual User? Receiver { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}
