using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.WishLists
{
    public interface IWishListRepository
    {
        IEnumerable<WishList> getAllWishList();
        WishListViewModels getByIDWishList(string id);
        WishListViewModels AddWishList(WishListViewModels wishListViewModels);
        WishListViewModels UpdateWishList(WishListViewModels wishListViewModels);
        void DeleteWishList(string id);
    }
}
