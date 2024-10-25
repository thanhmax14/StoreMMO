using AutoMapper;
using MailKit.Search;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
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
            CreateMap<StoreDetailViewModels, InputProductTypeViewModel>().ReverseMap();
            CreateMap<StoreDetail, InputProductTypeViewModel>().ReverseMap();
            CreateMap<ProductTypesViewModels, ProductViewModels>().ReverseMap();



            // cau hinh mapper compaint cuar Ngoc
            // ComplaintsMapper mapping
            CreateMap<Complaint, ComplaintsMapper>()
                .ForMember(dest => dest.OrderDetailmap, opt => opt.MapFrom(src => src.OrderDetail));

            // OrderBuysMapper mapping
            CreateMap<OrderBuy, OrderBuysMapper>()
                .ForMember(dest => dest.UserMap, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.StoreMap, opt => opt.MapFrom(src => src.Store));

            // OrderDetailsMapper mapping
            CreateMap<OrderDetail, OrderDetailsMapper>()
                .ForMember(dest => dest.orderBuymap, opt => opt.MapFrom(src => src.orderBuy))
                .ForMember(dest => dest.productMapper, opt => opt.MapFrom(src => src.Product));

            // ProductMapper mapping
            CreateMap<Product, ProductMapper>()
                .ForMember(dest => dest.ProductTypemap, opt => opt.MapFrom(src => src.ProductType));

            // ProductTypeMapper mapping
            CreateMap<ProductType, ProductTypeMapper>();

            // StoreMapper mapping
            CreateMap<Store, StoreMapper>()
                .ForMember(dest => dest.Usermapper, opt => opt.MapFrom(src => src.User));

            // UserMapper mapping
            //
            //CreateMap<AppUser, UserMapper>();
            CreateMap<AppUser, UserMapper>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            // New BalanceMapper mapping
            CreateMap<Balance, BalanceMapper>()
                .ForMember(dest => dest.Usermapforbalance, opt => opt.MapFrom(src => src.User)); // Map User to Usermapforbalance
        }
    }
}
