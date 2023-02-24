﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MultiShop.Business.ViewModels
{
    public class CategoryCreateVM
    {
        [Required,MinLength(3),MaxLength(256)]
        public string? Name { get; set; }
        [Required]
        public IFormFile? Image { get; set; }

    }
}
