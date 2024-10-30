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
            CreateMap<StoreTypeViewModels, Store>().ReverseMap();
            CreateMap<ProductTypesViewModels, ProductViewModels>().ReverseMap();
            CreateMap<ViewProductModels, InputProductTypeViewModel>().ReverseMap();
            CreateMap<ViewProductModels, ProductTypesViewModels>().ReverseMap();



            // ComplaintsMapper mapping
            CreateMap<Complaint, ComplaintsMapper>()
                .ForMember(dest => dest.OrderDetailmap, opt => opt.MapFrom(src => src.OrderDetail));

            // OrderBuysMapper mapping
            CreateMap<OrderBuy, OrderBuysMapper>()
                .ForMember(dest => dest.UserMap, opt => opt.MapFrom(src => src.AppUser))       // Ánh xạ User từ OrderBuy.AppUser
                .ForMember(dest => dest.StoreMap, opt => opt.MapFrom(src => src.Store));        // Ánh xạ Store từ OrderBuy.Store

            // OrderDetailsMapper mapping
            CreateMap<OrderDetail, OrderDetailsMapper>()
                .ForMember(dest => dest.orderBuymap, opt => opt.MapFrom(src => src.orderBuy))   // Ánh xạ OrderBuy từ OrderDetail.orderBuy
                .ForMember(dest => dest.productMapper, opt => opt.MapFrom(src => src.Product)); // Ánh xạ Product từ OrderDetail.Product

            // ProductMapper mapping
            CreateMap<Product, ProductMapper>()
                .ForMember(dest => dest.ProductTypemap, opt => opt.MapFrom(src => src.ProductType));

            // ProductTypeMapper mapping
            CreateMap<ProductType, ProductTypeMapper>();

            // StoreMapper mapping
            CreateMap<Store, StoreMapper>()
                .ForMember(dest => dest.Usermapper, opt => opt.MapFrom(src => src.User));       // Ánh xạ User (Seller) từ Store.User

            // UserMapper mapping
            CreateMap<AppUser, UserMapper>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));                  // Đảm bảo ánh xạ ID từ AppUser.Id

            // BalanceMapper mapping
            CreateMap<Balance, BalanceMapper>()
                .ForMember(dest => dest.Usermapforbalance, opt => opt.MapFrom(src => src.User)); // Maps User from Balance.User



        }
    }
}
