using Microsoft.AspNetCore.Mvc;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
                await _categoryService.CreateAsync(categoryCreateVM);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest("Category cannot create.");
            }
        }
    }
}
