using GLIMPSE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Application.Dtos
{
    public class InviteDTO : BaseDTO
    {
        public int? ReceiverId { get; set; }
        public virtual UserDTO? Receiver { get; set; }
        public int? ProjectId { get; set; }
        public virtual ProjectDTO? Project { get; set; }
    }
}
