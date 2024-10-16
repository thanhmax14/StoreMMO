using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.ProductsTypes
{
    public interface IProductTypeRepository
    {
        IEnumerable<ProductType> GetAllProduct();
        ProductTypesViewModels getByIDProduct(string id);
        ProductTypesViewModels AddProduct(ProductTypesViewModels productViewModels);
        ProductTypesViewModels Update(ProductTypesViewModels productViewModels);
        void DeleteProduct(string id);  
    }
}
