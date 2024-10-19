using AutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Services.AutoMapper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            CreateMap<Product, ProductViewModels>().ReverseMap();
            CreateMap<InputProductViewModel, ProductViewModels>().ReverseMap();
            CreateMap<InputProductViewModel, ProductTypesViewModels>().ReverseMap();
            CreateMap<InputProductTypeViewModel, ProductTypesViewModels>().ReverseMap();
            CreateMap<ProductType, InputProductTypeViewModel>().ReverseMap();
            CreateMap<StoreAddViewModels, ViewProductModels>().ReverseMap();
            CreateMap<StoreDetailViewModels, ViewProductModels>().ReverseMap();


        }
    }
}
