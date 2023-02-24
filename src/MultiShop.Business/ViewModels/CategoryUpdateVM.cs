using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class CategoryUpdateVM
    {
        public int Id{ get; set; }
        [Required, MinLength(3), MaxLength(256)]
        public string? Name { get; set; }
        public IFormFile Image { get; set; }
        public DateTime Created_at{ get; set; }
    }
}
