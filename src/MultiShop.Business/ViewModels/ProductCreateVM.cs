using Microsoft.AspNetCore.Http;
using MultiShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class ProductCreateVM
    {
        //public IFormFile Images { get; set; }
        public int Id { get; set; }
        [Required,MaxLength(200)]
        public string? Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required,MinLength(4)]
        public string Description { get; set; }
        public bool IsDelete { get; set; } = false;
        [Required]
        public int CategoryId { get; set; }
    }
}
