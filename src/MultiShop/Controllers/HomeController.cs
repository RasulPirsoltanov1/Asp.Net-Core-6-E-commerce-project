using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.DataAccess.Contexts;
using MultiShop.DataAccess.Repositories.Interfaces;

namespace MultiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public HomeController(ICategoryService categoryService, AppDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories= _categoryService.GetAll();
            var products =await _context.Products.Include(i=>i.Images).ToListAsync();
            var settings=await _context.Settings.FindAsync(1);
            HomeVM homeVM = new HomeVM()
            {
                Settings = settings,
                Categories =categories,
                Products=products
            };
            return View(homeVM);
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            var categories = _categoryService.GetAll();
            var product = await _context.Products.Include(i => i.Images).FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return NotFound("Product not found.");
            }
            var products = await _context.Products.Where(c => c.CategoryId == product.CategoryId).Include(i => i.Images).Include(c=>c.Reviews).Include(i=> i.Category).ToListAsync();
            var reviewa = await _context.Reviews.Include(u => u.User).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Categories = categories,
                Products = products,
                Product= product
            };
            return View(homeVM);
        }
    }
}
