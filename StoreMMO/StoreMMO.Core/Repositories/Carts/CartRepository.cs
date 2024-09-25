using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Carts
{

    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
