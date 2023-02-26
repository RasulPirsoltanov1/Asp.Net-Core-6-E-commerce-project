using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            ViewData["SiteIcon"] = settings.SiteIcon;
            return View(settings);
            //return View();
        }

        public async Task<IActionResult> Update()
        {
            var settings = await _context.Settings.FindAsync(1);
            ViewData["SiteIcon"] = settings.SiteIcon;
            var settingsUpdateVM = _mapper.Map<SettingsUpdateVM>(settings);
            //settingsUpdateVM.SiteIcon = null;
            return View(settingsUpdateVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SettingsUpdateVM settingsVM,IFormFile? siteIcon)
        {
            try
            {
                var oldSetting =await _context.Settings.FindAsync(1);
                if (!ModelState.IsValid)
                {
                    if (!TryValidateModel(settingsVM))
                    {
                        string errors = string.Empty;
                        foreach (var key in ModelState.Keys)
                        {
                            if (ModelState[key].ValidationState == ModelValidationState.Invalid)
                            {
                                var errorMessages = ModelState[key].Errors.Select(e => e.ErrorMessage);
                                errors += $"{key}: {string.Join(", ", errorMessages)}<br>";
                            }
                        }
                        return Content(errors);
                    }
                }
                settingsVM.Id = 1;
                if (siteIcon != null)
                {
                    var imageUrl = await siteIcon.SaveFileAsync(_env.WebRootPath, "wwwroot", "uploads");

                    string filePath = Path.Combine(_env.WebRootPath,"uploads",oldSetting.SiteIcon);
                    oldSetting.SiteIcon = imageUrl;
                    // Check if the file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        // Delete the file
                        System.IO.File.Delete(filePath);
                    }
                }
                else
                {
                    oldSetting.SiteIcon = oldSetting.SiteIcon;
                }
                oldSetting.Text= settingsVM.Text;
                oldSetting.Email= settingsVM.Email;
                oldSetting.FacebookLink= settingsVM.FacebookLink;
                oldSetting.InstagramLink= settingsVM.InstagramLink;
                oldSetting.LinkedinLink= settingsVM.LinkedinLink;
                oldSetting.Location = settingsVM.Location;
                oldSetting.Phone = settingsVM.Phone;
                oldSetting.SiteName = settingsVM.SiteName;
                oldSetting.TwitterLink = settingsVM.TwitterLink;
                _context.Settings.Update(oldSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Update));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
