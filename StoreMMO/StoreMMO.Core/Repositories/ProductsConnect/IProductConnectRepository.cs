using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.ProductsConnect
{
    public interface IProductConnectRepository
    {
        IEnumerable<ProductConnect> getAllProductConnect();
        ProductConnectViewModels getByIdProductConnect(string id);
        ProductConnectViewModels AddProductConnect(ProductConnectViewModels productConnect);
        ProductConnectViewModels UpdateProductConnect(ProductConnectViewModels productConnect);
        void DeleteProductConnect(string id);
    }
}
