using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.WishLists
{
    public class WishListRepository : IWishListRepository
    {
        private readonly AppDbContext _context;

        public WishListRepository(AppDbContext context)
        {
            _context = context;
        }

        public WishListViewModels AddWishList(WishListViewModels wishListViewModels)
        {
            var viewModel = new WishList
            {
                Id = wishListViewModels.Id,
                ProductId = wishListViewModels.ProductId,
                UserId = wishListViewModels.UserId,
            };
            _context.WishLists.Add(viewModel);
            _context.SaveChanges();
            return wishListViewModels;
        }

        public void DeleteWishList(string id)
        {
            var findId = _context.WishLists.SingleOrDefault(w => w.Id == id);
            if (findId == null)
            {
                throw new Exception("Not found Id");
            }
            _context.WishLists.Remove(findId);
            _context.SaveChanges();
        }

        public IEnumerable<WishListViewModels> getAllByUserID(string userID)
        {
            var find = _context.WishLists.Where(w => w.UserId == userID).ToList();
            List<WishListViewModels> temp = find.Select(item => new WishListViewModels
            {
                Id = item.Id,

                ProductId = item.ProductId,
                UserId = item.UserId
            }).ToList();

            return temp;
        }


        public IEnumerable<WishListViewModels> getAllWishList()
        {
            var list = _context.WishLists.ToList();
            List<WishListViewModels> temp = list.Select(item => new WishListViewModels 
            { Id = item.Id,
                
                ProductId = item.ProductId,
                UserId = item.UserId }).ToList();
           
            return temp;
        }

        public WishListViewModels getByIDWishList(string id)
        {
            var findId = _context.WishLists.SingleOrDefault(w => w.Id == id);
            if (findId == null)
            {
                throw new Exception("Not found Id");
            }
            var viewModel = new WishListViewModels
            {
                Id = findId.Id,
                ProductId = findId.ProductId,
                UserId = findId.UserId,
            };
            return viewModel;
        }

        public WishListViewModels UpdateWishList(WishListViewModels wishListViewModels)
        {
            var viewModel = new WishList
            {
                Id = wishListViewModels.Id,
                ProductId = wishListViewModels.ProductId,
                UserId = wishListViewModels.UserId,
            };
            _context.WishLists.Update(viewModel);
            _context.SaveChanges();
            return wishListViewModels;
        }
    }
}
