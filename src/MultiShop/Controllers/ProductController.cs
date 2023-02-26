using Microsoft.AspNetCore.Mvc;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using System.Security.Claims;

namespace MultiShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(HomeVM homeVM)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var comment = homeVM.ReviewVM.Comment;
            Review review = new Review()
            {
                UserId = userId,
                ProductId = homeVM.ReviewVM.ProductId,
                Comment = homeVM.ReviewVM.Comment,
                IsDelete = false,
                Rating = homeVM.ReviewVM.Rating ?? 0,
                Created_at = DateTime.UtcNow
            };
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            var productDetailParams = new ProductDetailParams()
            {
                id = homeVM.ReviewVM.ProductId,
            };
            return RedirectToAction("ProductDetail", "Home", productDetailParams);
        }
    }
}
public class ProductDetailParams
{
    public int id { get; set; }
}