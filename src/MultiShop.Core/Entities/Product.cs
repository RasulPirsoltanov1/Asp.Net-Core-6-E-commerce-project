using MultiShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class Product: BaseEntity
    {
        public int Id { get; set; }
        //public string? Image{ get; set; }
        public string? Name { get; set; }
        public int Price{ get; set; }
        public int Raiting { get; set; }
        public int Quantity{ get; set; }
        public bool? IsDelete { get; set; } = false;
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public ICollection<Review> Reviews { get; set; }



    }
}
