using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class RegisterVM
    {
        [Required,MinLength(2),MaxLength(256)]
        public string UserName{ get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        [Required, MinLength(2), MaxLength(256),DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required,MinLength(8), MaxLength(500), DataType(DataType.Password)]
        public string Password{ get; set; }
        [Required,Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword{ get; set; }
    }
}
