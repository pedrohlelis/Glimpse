﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Application.Dtos
{
    public class RegisterDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? PictureId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
