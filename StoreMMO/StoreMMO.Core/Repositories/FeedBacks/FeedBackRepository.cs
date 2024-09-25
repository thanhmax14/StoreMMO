using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.FeedBacks
{

    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly AppDbContext _context;
        public FeedBackRepository(AppDbContext context)
        {
            _context = context;
        }
        public FeedBackViewModels AddFeedBacK(FeedBackViewModels feedBack)
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
            };
            _context.FeedBacks.Add(feebBack1);
            _context.SaveChanges();

            return feedBack;

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

        public IEnumerable<FeedBack> getAll()
        {
            var list = _context.FeedBacks.ToList();
            return list;
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
    }
}
