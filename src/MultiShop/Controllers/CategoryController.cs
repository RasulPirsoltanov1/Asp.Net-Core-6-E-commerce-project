﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.ViewModels;
using MultiShop.DataAccess.Contexts;

namespace MultiShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var products = await _context.Products.Where(p => p.CategoryId == id).Include(i => i.Images).Include(r => r.Reviews).ToListAsync();
            var categories = await _context.Categories.Include(p=>p.Products).ToListAsync();
            var settings = await _context.Settings.FindAsync(1);
            HomeVM homeVM = new()
            {
                Settings= settings,
                Categories = categories,
                Products =products,
            };
            return View(homeVM);
        }
    }
}
