using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using System.Security.Claims;

namespace MultiShop.Controllers
{
    [Authorize(Policy = "UserPolicy")]
    public class WishlistController : Controller
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlistProducts =await _context.Wishlists.Where(w => w.AppUserId == userId).Include(w => w.Product.Images).Select(w => w.Product).ToListAsync();
            HomeVM homeVM=new HomeVM()
            {
                Categories=_context.Categories,
                Products=wishlistProducts,
                Settings=await _context.Settings.FindAsync(1),
            };
            return View(homeVM);
        }

        public async Task<IActionResult> Add(int productId)
        {
            if(await _context.Products.FindAsync(productId) == null)
            {
                return BadRequest("Product Not exist");
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Wishlist wishlist = new()
            {
                AppUserId=userId,
                ProductId=productId
            };
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
