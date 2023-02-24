using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.Utilities;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;
        private IWebHostEnvironment _env;


        public SettingsController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _context.Settings.FindAsync(1);
            var settingVM = _mapper.Map<SettingsVM>(settings);
            return View(settingVM);
            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SettingsVM settingsVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(settingsVM);
                }
                var setting = _mapper.Map<Setting>(settingsVM);
                if (settingsVM.SiteIcon != null)
                {
                    var imageUrl = await settingsVM.SiteIcon.SaveFileAsync(_env.WebRootPath, "wwwroot", "uploads", "Product_Images");
                    setting.SiteIcon = imageUrl;

                    //string filePath = "uploads/" +imageUrl;

                    //// Check if the file exists
                    //if (System.IO.File.Exists(filePath))
                    //{
                    //    // Delete the file
                    //    System.IO.File.Delete(filePath);
                    //}
                }
                setting.SiteIcon = null;
                setting.Id = 1;
                _context.Settings.Update(setting);
                await _context.SaveChangesAsync();
                return View();
            }
            catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
    }
}
