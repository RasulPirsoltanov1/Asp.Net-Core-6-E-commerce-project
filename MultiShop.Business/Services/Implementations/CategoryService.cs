using AutoMapper;
using MultiShop.Business.Exceptions;
using MultiShop.Business.Mappers;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CategoryCreateVM categoryCreateVM)
        {
            if (categoryCreateVM == null)
            {
                throw new ArgumentNullException();
            }
            var category = _mapper.Map<Category>(categoryCreateVM);
            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _categoryRepository.GetAll();
            if (categories == null)
            {
                throw new NotFoundException("Not Found!");
            }
            return categories;
        }

        public Category GetByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
