using MultiShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product>? Products { get; set; }
        public Product? Product { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public ReviewVM? ReviewVM{ get; set; }
    }
}
