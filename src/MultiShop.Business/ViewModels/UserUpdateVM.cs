using MultiShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class UserUpdateVM
    {
        public List<string>? roles { get; set; }
        public AppUser? user { get; set; }
    }
}
