using MultiShop.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class Wishlist : BaseEntity
    {
        public int Id { get; set; }

        // Foreign key to the Product table
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Foreign key to the AppUser table
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
