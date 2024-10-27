using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.OrderDetails;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.orderDetailViewModels
{
    public class OrderDeailsRepository : IOrderDeailsRepository
    {
        private readonly AppDbContext _context;

        public OrderDeailsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(OrderDetailsViewModels orderDetailViewModels)
        {
            try
            {
                var orderDetailViewModel = new OrderDetail
                {
                    ID = orderDetailViewModels.ID,
                    OrderBuyID = orderDetailViewModels.OrderBuyID,
                    ProductID = orderDetailViewModels.ProductID,
                    AdminMoney = orderDetailViewModels.AdminMoney,
                    SellerMoney = orderDetailViewModels.SellerMoney,
                    quantity = orderDetailViewModels.quantity,
                    Dates = orderDetailViewModels.Dates,
                    status = orderDetailViewModels.status,
                    stasusPayment = orderDetailViewModels.stasusPayment,
                    Price = orderDetailViewModels.Price,
                };

                await _context.OrderDetails.AddAsync(orderDetailViewModel); // Sử dụng AddAsync để thêm
                await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync để lưu
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(OrderDetailsViewModels orderDetailViewModels)
        {
            try
            {
                var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(b => b.ID == orderDetailViewModels.ID);
                if (orderDetail != null)
                {
                    _context.OrderDetails.Remove(orderDetail);
                    await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<OrderDetailsViewModels> GetOrderDeailByIDAsync(string id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                return new OrderDetailsViewModels
                {
                    ID = orderDetail.ID,
                    OrderBuyID = orderDetail.OrderBuyID,
                    ProductID = orderDetail.ProductID,
                    AdminMoney = orderDetail.AdminMoney,
                    SellerMoney = orderDetail.SellerMoney,
                    quantity = orderDetail.quantity,
                    Dates = orderDetail.Dates,
                    status = orderDetail.status,
                    stasusPayment = orderDetail.stasusPayment,
                    Price = orderDetail.Price,
                };
            }
            return null;
        }

        public async Task<OrderDetailsViewModels> GetOrderDetailByOrderCodeAsync(string productID)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(b => b.ProductID == productID.ToString());
            if (orderDetail != null)
            {
                return new OrderDetailsViewModels
            {
                ID = orderDetail.ID,
                OrderBuyID = orderDetail.OrderBuyID,
                ProductID = orderDetail.ProductID,
                AdminMoney = orderDetail.AdminMoney,
                SellerMoney = orderDetail.SellerMoney,
                quantity = orderDetail.quantity,
                Dates = orderDetail.Dates,
                status = orderDetail.status,
                stasusPayment = orderDetail.stasusPayment,
                Price = orderDetail.Price,
            
                };
            }
            return null;
        }

        public async Task<IEnumerable<OrderDetailsViewModels>> GetOrderDetailsByOrderBuyIDAsync(string orderBuyID)
        {
            return await _context.OrderDetails
               .Where(b => b.OrderBuyID == orderBuyID)
               .Select(b => new OrderDetailsViewModels
               {
                   ID = b.ID,
                   OrderBuyID = b.OrderBuyID,
                   ProductID = b.ProductID,
                   AdminMoney = b.AdminMoney,
                   SellerMoney = b.SellerMoney,
                   quantity = b.quantity,
                   Dates = b.Dates,
                   status = b.status,
                   stasusPayment = b.stasusPayment,
                   Price = b.Price,
               })
               .ToListAsync();
        }

        public async Task<bool> EditAsync(SaleHistoryViewModels orderDetail)
        {
            try
            {
                var orderDetails = await _context.OrderDetails.FirstOrDefaultAsync(b => b.ProductID == orderDetail.OrderID);
                if (orderDetail != null)
                {
                    orderDetail.OrderID = orderDetail.OrderID;
                    orderDetail.Dates = orderDetail.Dates;
                    orderDetail.NguoiMua = orderDetail.NguoiMua;
                    orderDetail.StoreName = orderDetail.StoreName;
                   orderDetail.Productype = orderDetail.Productype;
                    orderDetail.Price   = orderDetail.Price;
                    orderDetail.totalMoney = orderDetail.totalMoney;
                    orderDetail.AdminMoney = orderDetail.AdminMoney;
                   

                    _context.OrderDetails.Update(orderDetails);
                    await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public  Task<bool> UpdateAsync(SaleHistoryViewModels SaleHistoryViewModels)
        {
           return EditAsync(SaleHistoryViewModels);
        }

        public IEnumerable<SaleHistoryViewModels> getAll()
        {
            string sql = $"SELECT OrderBuys.ID AS [OrderID], od.Dates, OrderBuys.OrderCode, us.UserName AS [NguoiMua], sd.Name AS [StoreName], pt.Name AS [Productype], od.Price, OrderBuys.totalMoney, od.AdminMoney, od.stasusPayment FROM OrderBuys INNER JOIN OrderDetails od ON OrderBuys.ID = od.OrderBuyID INNER JOIN Stores st ON OrderBuys.StoreID = st.Id INNER JOIN StoreDetails sd ON st.Id = sd.StoreId INNER JOIN ProductTypes pt ON OrderBuys.ProductTypeId = pt.Id INNER JOIN Users us ON OrderBuys.UserID = us.Id AND st.UserId = us.Id;\r\n";
            var list =  this._context.Database.SqlQueryRaw<SaleHistoryViewModels>(sql).ToList();
            return list;
        }

        public IEnumerable<GetOrderDetailsViewModel> getOrderDetails(string orderID)
        {
            string sql = @"SELECT Products.Account, Products.Pwd AS Password, OrderDetails.Quantity, OrderDetails.Price, OrderDetails.Dates, OrderDetails.stasusPayment, OrderDetails.Status FROM OrderDetails INNER JOIN Products ON OrderDetails.ProductID = Products.Id WHERE OrderDetails.OrderBuyID = OrderBuyID;
";

            var parameters = new[] { new SqlParameter("@orderID", orderID) };
            var result = _context.Database.SqlQueryRaw<GetOrderDetailsViewModel>(sql, parameters).ToList();

            return result;
        }

		public async Task<bool> UpdateDetailAsync(OrderDetailsViewModels orderDetailViewModels)
		{
			try
			{
				var complaint = await _context.OrderDetails.FindAsync(orderDetailViewModels.ID);
				if (complaint == null)
				{
					return false;
				}

				complaint.status = orderDetailViewModels.status;
				complaint.SellerMoney = orderDetailViewModels.SellerMoney;
				complaint.OrderBuyID = orderDetailViewModels.OrderBuyID;
				complaint.AdminMoney = orderDetailViewModels.AdminMoney;
				complaint.Dates = orderDetailViewModels.Dates;
				complaint.ID = orderDetailViewModels.ID;
				complaint.ProductID = orderDetailViewModels.ProductID;
				complaint.stasusPayment = orderDetailViewModels.stasusPayment;
                complaint.Price = orderDetailViewModels.Price;
                complaint.quantity = orderDetailViewModels.quantity;
				_context.OrderDetails.Update(complaint);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
