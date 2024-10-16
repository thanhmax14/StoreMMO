using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.WishLists
{
    public interface IWishListsService
    {
        IEnumerable<WishListViewModels> getAllWishList();
        WishListViewModels getByIDWishList(string id);
        WishListViewModels AddWishList(WishListViewModels wishListViewModels);
        WishListViewModels UpdateWishList(WishListViewModels wishListViewModels);
        void DeleteWishList(string id);
        IEnumerable<WishListViewModels> getAllByUserID(string userid);
    }
}
