using MultiShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class Review: BaseEntity
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Created_at { get; set; }
    }
}
