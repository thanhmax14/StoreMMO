using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.ProductConnects
{
    public interface IProductConnectService
    {
        IEnumerable<ProductConnect> getAllProductConnect();
        ProductConnectViewModels getByIdProductConnect(string id);
        ProductConnectViewModels AddProductConnect(ProductConnectViewModels productConnect);
        ProductConnectViewModels UpdateProductConnect(ProductConnectViewModels productConnect);
        void DeleteProductConnect(string id);
    }
}
