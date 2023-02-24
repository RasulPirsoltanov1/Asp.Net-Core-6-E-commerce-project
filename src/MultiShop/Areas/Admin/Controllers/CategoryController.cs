using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Business.Exceptions;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.Utilities;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "ModeratorPolicy,AdminPolicy")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;


        public CategoryController(ICategoryService categoryService, AppDbContext context, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM categoryCreateVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoryCreateVM);
                }

                string imageUrl = await categoryCreateVM.Image.SaveFileAsync(_env.WebRootPath, "wwwroot", "uploads", "Category");
                Category category = new()
                {
                    Name = categoryCreateVM.Name,
                    Image = imageUrl,
                    created_at = DateTime.UtcNow,
                };
            //C: \Users\user\Desktop\E - Commerce Project MVC\src\MultiShop\wwwroot\uploads\Category\
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVM categoryUpdateVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoryUpdateVM);
                }
                await _categoryService.UpdateAsync(categoryUpdateVM);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest("Category cannot create.");
            }
        }
    }
}
