using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
