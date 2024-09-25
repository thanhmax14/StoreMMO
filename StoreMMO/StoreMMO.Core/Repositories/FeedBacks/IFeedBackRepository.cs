using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.FeedBacks
{
    public interface IFeedBackRepository
    {
        IEnumerable<FeedBack> getAll();
        FeedBackViewModels getById(string id);
        FeedBackViewModels AddFeedBacK(FeedBackViewModels feedBack);
        FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack);
        
        void DeleteFeedBack(string id);

    }
}
