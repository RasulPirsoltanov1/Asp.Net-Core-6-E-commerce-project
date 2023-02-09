using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Task<CategoryCreateVM> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateVM categoryCreateVM);
        Task UpdateAsync(CategoryUpdateVM categoryUpdateVM);
        Task DeleteAsync(int id);
    }
}
