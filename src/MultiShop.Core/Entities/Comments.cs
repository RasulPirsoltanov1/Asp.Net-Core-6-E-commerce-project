using MultiShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Core.Entities
{
    public class Comments: BaseEntity
    {
        public int Id{ get; set; }
        public string Comment{ get; set; }
        public string UserName{ get; set; }
        public string Rating{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
