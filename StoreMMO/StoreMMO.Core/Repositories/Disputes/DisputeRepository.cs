using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Disputes
{
    public class DisputeRepository : IDisputeRepository
    {
        private readonly AppDbContext _context;
        public DisputeRepository(AppDbContext dbContext)
        {
            this._context = dbContext;
        }
        public IEnumerable<DisputeViewModels> getAllDispute()
        {
            string sql = @"
    SELECT 
        * 
    FROM 
        Complaints
    WHERE 
        Status = 'reportAdmin'";

            var list = this._context.Database.SqlQueryRaw<DisputeViewModels>(sql).ToList();
            return list;
        }

        public IEnumerable<DisputeViewModels> Getcomstatus()
        {
            var complainlist = this._context.Complaints.Where(x => x.Status.Equals("0")).ToList();
            try
            {

                List<DisputeViewModels> cateviewmodel = complainlist.Select(x => new DisputeViewModels
                {
                    //Id = x.ID,
                    //OrderDetailID = x.OrderDetailID,
                    ////  Commission = x.Commission,
                    //Description = x.Description,
                    //Reply = x.Reply,
                }).ToList();
                return cateviewmodel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<IEnumerable<DisputeViewModels>> getAllAsync()
        {
            var list = await _context.Complaints.ToListAsync(); // Sử dụng ToListAsync()
            var list1 = list.Select(x => new DisputeViewModels
            {
                ID = x.ID,
                OrderDetailID = x.OrderDetailID,
                Description = x.Description,
                CreateDate = x.CreateDate,
                Reply = x.Reply,
                Status = x.Status,
            }).ToList();
            return list1;
        }
      
    }
}
