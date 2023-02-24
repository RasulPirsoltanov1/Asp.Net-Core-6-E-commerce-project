using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class SettingsVM
    {
        public int? Id{ get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkedinLink { get; set; }
        public string? TwitterLink { get; set; }
        [Required,DataType(DataType.ImageUrl)]
        public IFormFile? SiteIcon { get; set; }
        public string? SiteName { get; set; }
    }
}
