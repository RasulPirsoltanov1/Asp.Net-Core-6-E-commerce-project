
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string? UsernameOrEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool rememberMe { get; set; }
    }
}
