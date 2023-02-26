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
    public class SettingsVM
    {
        public SettingsUpdateVM? settingsUpdateVM { get; set; }
        public Setting? settingsVM { get; set; }
    }
}
