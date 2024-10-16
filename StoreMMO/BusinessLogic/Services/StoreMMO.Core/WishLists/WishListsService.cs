using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.WishLists;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.WishLists
{
    public class WishListsService : IWishListsService
    {
        private readonly IWishListRepository _wil;
        public WishListsService(IWishListRepository  wishList)
        {
            this._wil = wishList;
        }
        public WishListViewModels AddWishList(WishListViewModels wishListViewModels)
        {
            return this._wil.AddWishList(wishListViewModels);
        }

        public void DeleteWishList(string id)
        {
            this._wil.DeleteWishList(id);
        }

        public IEnumerable<WishListViewModels> getAllByUserID(string userid)
        {
           return this._wil.getAllByUserID(userid);
        }

        public IEnumerable<WishListViewModels> getAllWishList()
        {
            return this._wil.getAllWishList();
        }

        public WishListViewModels getByIDWishList(string id)
        {
            return this._wil.getByIDWishList(id);
        }

        public WishListViewModels UpdateWishList(WishListViewModels wishListViewModels)
        {
            return this._wil.UpdateWishList(wishListViewModels);
        }
    }
}
