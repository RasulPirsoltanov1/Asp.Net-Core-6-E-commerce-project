using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Business.Exceptions;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "ModeratorPolicy")]
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
