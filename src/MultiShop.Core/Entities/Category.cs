using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsDelete{ get; set; }=false;
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
