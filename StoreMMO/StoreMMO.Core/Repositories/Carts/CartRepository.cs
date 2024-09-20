using StoreMMO.Core.Models;
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

        public Cart Add(Cart cart)
        {
             _context.Carts.Add(cart);
            _context.SaveChanges(); 
            return cart;
            
        }

        public Cart Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> getAll()
        {
            throw new NotImplementedException();
        }

        public Cart getById(string id)
        {
           var cart = _context.Carts.FirstOrDefault(x => x.Id == id);
            return cart;
        }

        public Cart Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
