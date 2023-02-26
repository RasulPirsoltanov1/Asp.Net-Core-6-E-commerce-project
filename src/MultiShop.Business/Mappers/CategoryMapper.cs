using AutoMapper;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.Mappers
{


    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryCreateVM>().ReverseMap();
            CreateMap<Category, CategoryUpdateVM>().ReverseMap();
            CreateMap<Product, ProductCreateVM>().ReverseMap();
            CreateMap<Setting, SettingsUpdateVM>().ReverseMap();
        }
    }
}
