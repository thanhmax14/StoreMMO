using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.OrderBuys
{
    public class OrderBuysRepository : IOrderBuysRepository
    {
        private readonly AppDbContext _context;
        public OrderBuysRepository(AppDbContext context)
        {
            _context = context;
        }
        public OrderViewModel AddOrder(OrderViewModel inforAddViewModels)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderBuy> getAllOrderBuy()
        {
            var list = _context.OrderBuys.ToList();
            return list;
        }

        public OrderViewModel getByIdOrder(string id)
        {
            var findId = _context.OrderBuys.FirstOrDefault(x => x.ID == id);
            if (findId == null)
            {
                throw new Exception("Not found ID");
            }
            // Mapping to ProductViewModels
            var viewModel = new OrderViewModel
            {
                ID = findId.ID,
                UserID = findId.UserID,
                StoreID = findId.StoreID,
                ProductTypeId = findId.ProductTypeId,
                Status = findId.Status,
                OrderCode = findId.OrderCode,
                totalMoney = findId.totalMoney,
            };

            return viewModel;
        }

        public OrderViewModel UpdateOrder(OrderViewModel inforAddViewModels)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == inforAddViewModels.ID);
            if (existingProduct != null)
            {
                // Tách thực thể đã theo dõi
                _context.Entry(existingProduct).State = EntityState.Detached;
            }
            var viewModel = new Models.OrderBuy
            {
                ID = inforAddViewModels.ID,
                UserID = inforAddViewModels.ProductTypeId,
                StoreID = inforAddViewModels.StoreID,
                ProductTypeId = inforAddViewModels.ProductTypeId,
                Status = inforAddViewModels.Status,
                OrderCode = inforAddViewModels.OrderCode,
                totalMoney = inforAddViewModels.totalMoney,
            };
            /*_context.Products.Update(viewModel);*/
            _context.SaveChanges();
            return inforAddViewModels;
        }
    }
}
