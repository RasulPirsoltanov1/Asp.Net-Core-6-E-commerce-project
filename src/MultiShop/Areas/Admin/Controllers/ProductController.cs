using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.Utilities;
using System.IO;
using System.Xml.Linq;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "ModeratorPolicy,AdminPolicy")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        private IMapper _mapper;

        public ProductController(IProductService productService, AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Images).Include(c => c.Category).ToListAsync();
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
            var imageUrl = string.Empty;
            if (images.Count != 0)
            {
                foreach (var image in images)
                {

                    if (image.Length > 0)
                    {
                        imageUrl = await image.SaveFileAsync(_env.WebRootPath, "wwwroot", "uploads", "Product_Images");
                        productImages.Add(new Image
                        {
                            Url = imageUrl
                        });
                    }
                }
            }
            else
            {
                imageUrl = @"wwwroot/uploads/Product.png";
                productImages.Add(new Image
                {
                    Url = imageUrl
                });
            }
            Product newProduct = new Product()
            {
                Name = productCreateVM.Name,
                Price = productCreateVM.Price,
                Raiting = 0,
                Quantity = productCreateVM.Quantity,
                CategoryId = productCreateVM.CategoryId,
                Description = productCreateVM.Description,
                Information = productCreateVM.Informaation,
            };
            newProduct.Images = productImages;
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.Include(i => i.Images).Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            foreach (var item in product.Images)
            {
                string filePath = "uploads/Product_Images/" + item.Url;

                // Check if the file exists
                if (System.IO.File.Exists(filePath))
                {
                    // Delete the file
                    System.IO.File.Delete(filePath);
                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            ViewData["categories"] = _context.Categories;
            var viewProduct = _mapper.Map<ProductCreateVM>(product);
            return View(viewProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductCreateVM productCreateVM, IFormFileCollection images)
        {
            if (!ModelState.IsValid)
            {
                ViewData["categories"] = _context.Categories;
                return View(productCreateVM);
            }
            var product = await _context.Products.FindAsync(id);
            product.Name = productCreateVM.Name;
            product.Price = productCreateVM.Price;
            product.Raiting = 0;
            product.Quantity = productCreateVM.Quantity;
            product.CategoryId = productCreateVM.CategoryId;
            product.Description = productCreateVM.Description;
            if (images != null)
            {
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
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
