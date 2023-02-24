using MultiShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class Setting:BaseEntity
    {
        public int Id{ get; set; }
        public string? Text { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FacebookLink{ get; set; }
        public string? InstagramLink{ get; set; }
        public string? LinkedinLink{ get; set; }
        public string? TwitterLink{ get; set; }
        public string? SiteIcon { get; set; }
        public string? SiteName{ get; set; }
    }
}
