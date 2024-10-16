using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.ProductsConnect
{
    public class ProductConnectRepository : IProductConnectRepository
    {
        private readonly AppDbContext _context;
        public ProductConnectRepository(AppDbContext context)
        {
            _context = context;
        }

        public ProductConnectViewModels AddProductConnect(ProductConnectViewModels productConnect)
        {
            var viewModel = new ProductConnect
            {
                Id = productConnect.Id,
                StoreDetailId = productConnect.StoreDetailId,
                ProductTypeId = productConnect.ProductId,
            };
            _context.ProductConnects.Add(viewModel);
            _context.SaveChanges();
            return productConnect;
        }

        public void DeleteProductConnect(string id)
        {
            var findId = _context.ProductConnects.FirstOrDefault(x => x.Id == id);
            if(findId == null)
            {
                throw new Exception("Not found ID");
            }
            _context.ProductConnects.Remove(findId);
            _context.SaveChanges();
        }

        public IEnumerable<ProductConnect> getAllProductConnect()
        {
            var list = _context.ProductConnects.ToList();
            return list;
        }

        public ProductConnectViewModels getByIdProductConnect(string id)
        {
            var findId = _context.ProductConnects.FirstOrDefault(x => x.Id == id);
            if (findId != null)
            {
                throw new Exception("Not found ID");

            }
            var viewModel = new ProductConnectViewModels
            {
                Id = findId.Id,
                StoreDetailId = findId.StoreDetailId,
                ProductId = findId.ProductTypeId,
            };
            return viewModel;
        }

        public ProductConnectViewModels UpdateProductConnect(ProductConnectViewModels productConnect)
        {
            var viewModel = new ProductConnect
            {
                Id = productConnect.Id,
                StoreDetailId = productConnect.StoreDetailId,
                ProductTypeId = productConnect.ProductId,
            };
            _context.ProductConnects.Update(viewModel);
            _context.SaveChanges();

            return productConnect;
        }
    }
}
