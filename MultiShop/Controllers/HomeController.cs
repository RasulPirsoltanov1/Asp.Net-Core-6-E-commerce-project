using Microsoft.AspNetCore.Mvc;
using MultiShop.Business.Services.Interfaces;
using MultiShop.DataAccess.Repositories.Interfaces;

namespace MultiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }
    }
}
