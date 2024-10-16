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
        ProductViewModels getByIDProduct(string id);
        ProductViewModels AddProduct(ProductViewModels productViewModels);
        ProductViewModels Update(ProductViewModels productViewModels);
        void DeleteProduct(string id);  
    }
}
