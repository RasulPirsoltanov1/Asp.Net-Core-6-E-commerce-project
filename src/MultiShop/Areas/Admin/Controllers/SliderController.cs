using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.Utilities;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _conext;
        private IWebHostEnvironment _env;


        public SliderController(AppDbContext conext, IWebHostEnvironment env)
        {
            _conext = conext;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var slides =await _conext.Sliders.ToListAsync();
            return View(slides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM sliderCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sliderCreateVM);
            }
            var imgUrl =await sliderCreateVM.Image.SaveFileAsync(_env.WebRootPath,"wwwroot", "uploads", "Slide");
            Slider sliderItem = new Slider()
            {
                Title=sliderCreateVM.Title,
                Description=sliderCreateVM.Description,
                Created_at=DateTime.UtcNow,
                Updated_at=DateTime.UtcNow,
                Image =imgUrl,
            };
            await _conext.Sliders.AddAsync(sliderItem);
            await _conext.SaveChangesAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var slide = await _conext.Sliders.FindAsync(Id);
            if(slide == null)
            {
                return BadRequest("Slide not found");
            }
            _conext.Sliders.Remove(slide);
            await _conext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
