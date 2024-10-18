using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Products
{
    public interface IProductRepository
    {
        IEnumerable<Models.Product> getAllProduct();
        ProductViewModels getByIdProduct(string id);
        ProductViewModels AddProduct(ProductViewModels inforAddViewModels);
        ProductViewModels UpdateProduct(ProductViewModels inforAddViewModels);
        void DeleteProduct(string id);
        IEnumerable<ManageStoreViewModels> ManageStore();
    }
}
