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
    }
}
