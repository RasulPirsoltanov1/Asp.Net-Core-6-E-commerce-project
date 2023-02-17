﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class UserVM
    {
        public int? Id { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? IsActive { get; set; }
        public string Role { get; set; }
    }
}
