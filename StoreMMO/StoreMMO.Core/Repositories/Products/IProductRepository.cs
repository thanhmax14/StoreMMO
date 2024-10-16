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
        InfoAddViewModels getByIdProduct(string id);
        InfoAddViewModels AddProduct(InfoAddViewModels inforAddViewModels);
        InfoAddViewModels UpdateProduct(InfoAddViewModels inforAddViewModels);
        void DeleteProduct(string id);
    }
}
