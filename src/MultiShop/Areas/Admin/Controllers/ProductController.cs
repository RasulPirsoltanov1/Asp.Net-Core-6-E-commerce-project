using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.Utilities;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;

        public ProductController(IProductService productService, AppDbContext context, IWebHostEnvironment env)
        {
            _productService = productService;
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            return View(_productService.GetAll());
        }
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = _context.Categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM, IFormFileCollection images)
        {
            if (!ModelState.IsValid)
            {
                ViewData["categories"] = _context.Categories;
                return View(productCreateVM);
            }
            var productImages = new List<Image>();
            foreach (var image in images)
            {

                if (image.Length > 0)
                {
                    var imageUrl = await image.SaveFileAsync(_env.WebRootPath, "wwwroot", "uploads", "Product_Images");
                    productImages.Add(new Image
                    {
                        Url = imageUrl
                    });
                }
            }
            Product newProduct = new Product()
            {
                Name = productCreateVM.Name,
                Price = productCreateVM.Price,
                Raiting = 0,
                Quantity = productCreateVM.Quantity,
                CategoryId = productCreateVM.CategoryId,
            };
            newProduct.Images = productImages;
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.Include(i=>i.Images).Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
        }
        
        public IActionResult NotFoundPage()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product=await _context.Products.FindAsync(id);
            if(product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
