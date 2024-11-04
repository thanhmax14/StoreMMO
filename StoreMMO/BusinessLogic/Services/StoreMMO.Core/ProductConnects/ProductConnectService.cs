using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.ProductsConnect;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.ProductConnects
{
    public class ProductConnectService : IProductConnectService
    {
        private readonly IProductConnectRepository _productConnectRepository;

        public ProductConnectService(IProductConnectRepository productConnectRepository)
        {
            _productConnectRepository = productConnectRepository;
        }

        public ProductConnectViewModels AddProductConnect(ProductConnectViewModels productConnect)
        {
            return _productConnectRepository.AddProductConnect(productConnect);
        }

        public void DeleteProductConnect(string id)
        {
            _productConnectRepository.DeleteProductConnect(id);
        }

        public IEnumerable<ProductConnect> getAllProductConnect()
        {
            return _productConnectRepository.getAllProductConnect();
        }

        public ProductConnectViewModels getByIdProductConnect(string id)
        {
            return _productConnectRepository.getByIdProductConnect(id);
        }

        public ProductConnectViewModels UpdateProductConnect(ProductConnectViewModels productConnect)
        {
            return _productConnectRepository.UpdateProductConnect(productConnect);  

        }
    }
}
