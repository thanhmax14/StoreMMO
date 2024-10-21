using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Purchase
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public PurchaseRepository(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            this._contextAccessor = contextAccessor;
        }

        public bool add(OrderBuyViewModels orderBuyViewModels)
        {
            if(orderBuyViewModels == null) return false;
            try
            {
                var tem = new OrderBuy {

                    ID = orderBuyViewModels.ID,
                    OrderCode = orderBuyViewModels.OrderCode,
                    ProductTypeId = orderBuyViewModels.ProductTypeId,
                    Status = orderBuyViewModels.Status,
                    StoreID = orderBuyViewModels.StoreID,
                    totalMoney = orderBuyViewModels.totalMoney,
                    UserID = orderBuyViewModels.UserID,
                };

                this._context.OrderBuys.Add(tem);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(OrderBuyViewModels orderBuyViewModels)
        {
            var find = this._context.OrderBuys.Find(orderBuyViewModels.ID);
              if(find == null)
            {
                return false;
            }
            return Save();
        }

        public bool Edit(OrderBuyViewModels orderBuyViewModels)
        {
            var find = _context.OrderBuys.Find(orderBuyViewModels.ID);
            if (find != null)
            {
                find.totalMoney = orderBuyViewModels.totalMoney;
                find.OrderCode = orderBuyViewModels.OrderCode;
                find.Status = orderBuyViewModels.Status;
                find.ProductTypeId = orderBuyViewModels.ProductTypeId;
                find.StoreID = orderBuyViewModels.StoreID;
                find.UserID = orderBuyViewModels.UserID;
                return Save();
            }
            return false;
        }
        public OrderBuyViewModels GetByID(string id)
        {
            var obj = this._context.OrderBuys.Find(id);
            var tem = new OrderBuyViewModels
            {
                ID = obj.ID,
                OrderCode = obj.OrderCode,
                ProductTypeId = obj.ProductTypeId,
                Status = obj.Status,
                StoreID = obj.StoreID,
                totalMoney = obj.totalMoney,
                UserID = obj.UserID,
            };
            return tem;
        }
        public bool Update(OrderBuyViewModels orderBuyViewModels)
        {
            throw new NotImplementedException();
        }
        public List<PurchaseItem> GetProductFromSession()
        {

            var cart = this._contextAccessor.HttpContext.Session.GetString("PurchaseItem");
            return string.IsNullOrEmpty(cart) ? new List<PurchaseItem>() : JsonConvert.DeserializeObject<List<PurchaseItem>>(cart);

        }
        public void SaveProductToSession(List<PurchaseItem> cart)
        {
            if (cart == null)
            {
                _contextAccessor.HttpContext.Session.Remove("PurchaseItem");
            }
            else
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                _contextAccessor.HttpContext.Session.SetString("PurchaseItem", JsonConvert.SerializeObject(cart, jsonSettings));
            }
            
        }
        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }


    }
}
