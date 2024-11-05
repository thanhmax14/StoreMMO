using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductViewModels = StoreMMO.Core.ViewModels.ProductViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Products
{
    public interface IProductService
    {
        IEnumerable<Product> getAllProduct();
        ProductViewModels getByIdProduct(string id);
        ProductViewModels AddProduct(ProductViewModels inforAddViewModels);
        ProductViewModels UpdateProduct(ProductViewModels inforAddViewModels);
        void DeleteProduct(string id);
		IEnumerable<ManageStoreViewModels> ManageStore(string userId);
        IEnumerable<ViewProductModels> GetProductsByStoreId(string storeId);
        IEnumerable<Product> getProductsByTypeID(string id);

        IEnumerable<Product> getProductsByTypeID1(string id);

        IEnumerable<ManageStoreViewModels> ManageStoreDetail(string userId);

        public Product GetByAccount(string account, string productTypeId);

    }
}
