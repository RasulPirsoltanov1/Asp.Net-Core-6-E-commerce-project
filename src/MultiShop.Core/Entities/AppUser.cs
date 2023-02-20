using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public string ProfilePhoto { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
