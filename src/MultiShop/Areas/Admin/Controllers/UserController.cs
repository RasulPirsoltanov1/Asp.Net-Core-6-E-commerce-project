using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager; 

        public UserController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.Select(u => new UserVM
            {
                Id=u.Id,
                UserName = u.UserName,
                Email = u.Email,
                ProfilePhoto = u.ProfilePhoto,
            }).ToList();

            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Update user role
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);
            UserUpdateVM userUpdateVM = new UserUpdateVM
            {
                roles = roles,
                user=user
            };
            return View(userUpdateVM);
        }
    }
}
