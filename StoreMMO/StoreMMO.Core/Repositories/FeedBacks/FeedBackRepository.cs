using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.FeedBacks
{

    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly AppDbContext _context;
        public FeedBackRepository(AppDbContext context)
        {
            _context = context;
        }
		public async Task<FeedBackViewModels> AddFeedBacKAsync(FeedBackViewModels feedBack)
		{
			var feebBack1 = new FeedBack
			{
				Id = feedBack.Id,
				UserId = feedBack.UserId,
				StoreDetailId = feedBack.StoreDetailId,
				Comments = feedBack.Comments,
				CreatedDate = DateTime.Now,
				Stars = feedBack.Stars,
				Relay = feedBack.Relay,
				DateRelay = feedBack.DateRelay,
				IsActive = feedBack.IsActive,
				OrderBuyId = feedBack.OrderBuyId
			};

		 var add =	await _context.FeedBacks.AddAsync(feebBack1);
            var tem = add;
			var result = await _context.SaveChangesAsync();

			return result > 0 ? feedBack : null; // Trả về feedBack nếu có thay đổi
		}


		public void DeleteFeedBack(string id)
        {
            var FindId = _context.FeedBacks.SingleOrDefault(x => x.Id == id);
            if (FindId == null)
            {
                throw new Exception("Not found ID");
            }
            _context.FeedBacks.Remove(FindId);
            _context.SaveChanges();
        }



        public FeedBackViewModels getById(string id)
        {
            var p = _context.FeedBacks.SingleOrDefault(x => x.Id == id);
            if (p == null)
            {
                throw new Exception("Not found Id");
            }
            var feebBack = new FeedBackViewModels
            {
                Id = p.Id,
                UserId = p.UserId,
                StoreDetailId = p.StoreDetailId,
                Comments = p.Comments,
                CreatedDate = p.CreatedDate,
                Stars = p.Stars,
                Relay = p.Relay,
                DateRelay = p.DateRelay,
                IsActive = p.IsActive,
            };
            return feebBack;

        }

        public FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack)
        {
            var feebBackUpdate = new FeedBack
            {
                Id = feedBack.Id,
                UserId = feedBack.UserId,
                StoreDetailId = feedBack.StoreDetailId,
                Comments = feedBack.Comments,
                CreatedDate = DateTime.Now,
                Stars = feedBack.Stars,
                Relay = feedBack.Relay,
                DateRelay = feedBack.DateRelay,
                IsActive = feedBack.IsActive,
            };
            _context.FeedBacks.Update(feebBackUpdate);
            _context.SaveChanges();
            return feedBack;
        }

        public IEnumerable<FeedBackViewModels> getAll(string StoreOwnerId)
        {

            string sql = $"SELECT        FeedBacks.*, StoreDetails.Name AS [StoreName], OrderBuys.OrderCode, \r\n                         Users.UserName, Stores.UserId AS [StoreOwnerId]\r\nFROM            FeedBacks INNER JOIN\r\n                         OrderBuys ON FeedBacks.OrderBuyId = OrderBuys.ID INNER JOIN\r\n                         StoreDetails ON FeedBacks.StoreDetailId = StoreDetails.Id INNER JOIN\r\n                         Users ON FeedBacks.UserId = Users.Id AND OrderBuys.UserID = Users.Id INNER JOIN\r\n                         Stores ON OrderBuys.StoreID = Stores.Id \r\n\t\t\t\t\t\t where Stores.UserId = '{StoreOwnerId}'";

            var list = this._context.Database.SqlQueryRaw<FeedBackViewModels>(sql).ToList();
            return list;
        }



        public IEnumerable<FeedBackViewModels> getFeedbackCustomerById(string feedbackID)
        {
            string sql = $"SELECT        FeedBacks.*, StoreDetails.Name AS [StoreName], OrderBuys.OrderCode, \r\n                         Users.UserName, Stores.UserId AS [StoreOwnerId]\r\nFROM            FeedBacks INNER JOIN\r\n                         OrderBuys ON FeedBacks.OrderBuyId = OrderBuys.ID INNER JOIN\r\n                         StoreDetails ON FeedBacks.StoreDetailId = StoreDetails.Id INNER JOIN\r\n                         Users ON FeedBacks.UserId = Users.Id AND OrderBuys.UserID = Users.Id INNER JOIN\r\n                         Stores ON OrderBuys.StoreID = Stores.Id \r\n\t\t\t\t\t\t where FeedBacks.Id = '{feedbackID}'";

            var list = this._context.Database.SqlQueryRaw<FeedBackViewModels>(sql).ToList();
            return list;
        }

        public FeedBack replyFeedback(string id, string reply)
        {
            var temp = this._context.FeedBacks.FirstOrDefault(x => x.Id == id);
            if (temp != null)
            {
                temp.Relay = reply;
                _context.SaveChanges();
                return temp;
            }
            return null;
        }
    }
}
