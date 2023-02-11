using MultiShop.Business.Exceptions;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.Services.Implementations
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetAll()
        {
            var products = _productRepository.GetAll();
            if (products == null)
            {
                throw new NotFoundException("Not Found!");
            }
            return products;
        }
    }
}
