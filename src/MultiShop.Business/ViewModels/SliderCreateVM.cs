using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class SliderCreateVM
    {
        [Required,MaxLength(150)]
        public string Title { get; set; }
        [Required, MinLength(10), MaxLength(250)]
        public string? Description { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
        public TimeSpan? Created_at { get; set; }
        public TimeSpan? Updated_at { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
