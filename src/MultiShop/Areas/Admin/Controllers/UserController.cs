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
                UserName = u.UserName,
                Email = u.Email,
                ProfilePhoto = u.ProfilePhoto,
                Role = u.Role.Join(_context.Roles, userRole => userRole.RoleId, role => role.Id, (userRole, role) => role.Name)
            }).ToList();

            return View(users);
        }
    }
}
