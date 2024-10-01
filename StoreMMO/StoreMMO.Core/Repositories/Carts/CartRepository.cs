using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Carts
{

    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CartRepository(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            this._contextAccessor = contextAccessor;   
        }

        public CartViewModels Add(CartViewModels cart)
        {
            var cartViewModel = new Cart
            {
                Id = cart.Id,
                UserId= cart.UserId,
                ProductId = cart.ProductId,
            };
           
            _context.Carts.Add(cartViewModel);
            _context.SaveChanges();

            return cart;
        }

        public void Delete(string id)
        {
            var p = _context.Carts.FirstOrDefault(x => x.Id == id);
            if(p == null)
            {
                throw new Exception("Delete not success");
            }
            _context.Carts.Remove(p);
            _context.SaveChanges();

            
        }

        public IEnumerable<Cart> getAll()
        {
            var findList = _context.Carts.ToList();
            return findList;
        }

        public CartViewModels getById(string id)
        {
            var findId = _context.Carts.SingleOrDefault(x => x.Id == id);
            if (findId == null)
            {
                // Trả về null hoặc ném ngoại lệ tùy theo cách bạn muốn xử lý
                return null; // hoặc throw new Exception("Not found ID");
            }
            var CarstViewModel = new CartViewModels
            {
                Id = findId.Id,
                UserId = findId.UserId,
                ProductId = findId.ProductId,
            };
          
            return CarstViewModel;
        }

        public CartViewModels Update(CartViewModels cart)
        {
            var CartViewModel1 = new Cart
            {
                Id = cart.Id,
                UserId = cart.UserId,
                ProductId = cart.ProductId,
            };
            _context.Carts.Update(CartViewModel1);
            _context.SaveChanges();

            return cart;
        }
        public List<CartItem> GetCartFromSession() {

            var cart = this._contextAccessor.HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);

        }
        public void SaveCartToSession(List<CartItem> cart)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            _contextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, jsonSettings));
        }

        public CartItem getProductAddByID(string proid)
        {
            var sql = @"
        SELECT 
            Products.Id AS productID, 
            Products.Name AS proName, 
            StoreDetails.Img AS img, 
            Products.Price AS price 
        FROM 
            StoreDetails 
        INNER JOIN 
            ProductConnects ON StoreDetails.Id = ProductConnects.StoreDetailId 
        INNER JOIN 
            Products ON ProductConnects.ProductId = Products.Id 
        WHERE 
            Products.Id = @ProductId";

            var cartItem = _context.Database.SqlQueryRaw<CartItem>(sql, new SqlParameter("@ProductId", proid));

            return nu;
        }

    }
}
