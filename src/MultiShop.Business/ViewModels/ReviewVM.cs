using MultiShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.ViewModels
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string? Comment { get; set; }
        public int? Rating { get; set; }
    }
}
