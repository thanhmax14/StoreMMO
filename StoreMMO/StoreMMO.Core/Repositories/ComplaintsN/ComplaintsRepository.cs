using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.ComplaintsN
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ComplaintsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ComplaintsMapper> GetAll(string id)
        {
            // Lấy tất cả các complaint liên quan đến seller với ID chỉ định, không phụ thuộc vào Status
            var complaints = _context.Complaints
                .Where(c => c.OrderDetail.orderBuy.Store.User.Id == id)
                .Include(c => c.OrderDetail)                        // Include OrderDetail của Complaint
                    .ThenInclude(od => od.orderBuy)                // Then Include OrderBuy của OrderDetail
                .Include(c => c.OrderDetail.orderBuy.AppUser)       // Include AppUser của OrderBuy
                .Include(c => c.OrderDetail.orderBuy.Store.User)
                .Include(c => c.OrderDetail.orderBuy.Store)         // Include Store của OrderBuy
                .Include(c => c.OrderDetail.Product)                // Include Product của OrderDetail
                    .ThenInclude(p => p.ProductType)               // Then Include ProductType của Product
                .ToList();

            // Lọc ra chỉ các complaint có Status "none"
            var filteredComplaints = complaints
                .Where(c => c.Status == "none")
                .ToList();

            var mappedComplaints = _mapper.Map<List<ComplaintsMapper>>(filteredComplaints);
            return mappedComplaints;
        }





        public IEnumerable<ComplaintsMapper> GetAllReportAdmin()
        {
            var complaints = _context.Complaints
               // .Where(c => c.OrderDetail.orderBuy.Store.User.Id == id)
                .Include(c => c.OrderDetail)                        // Include OrderDetail của Complaint
                    .ThenInclude(od => od.orderBuy)                // Then Include OrderBuy của OrderDetail
                .Include(c => c.OrderDetail.orderBuy.AppUser)       // Include AppUser của OrderBuy
                .Include(c => c.OrderDetail.orderBuy.Store.User)
                .Include(c => c.OrderDetail.orderBuy.Store)         // Include Store của OrderBuy
                .Include(c => c.OrderDetail.Product)                // Include Product của OrderDetail
                    .ThenInclude(p => p.ProductType)               // Then Include ProductType của Product
                .ToList();

            // Lọc ra chỉ các complaint có Status "none"
            var filteredComplaints = complaints
                .Where(c => c.Status.ToLower() == "reportadmin")
                .ToList();

            var mappedComplaints = _mapper.Map<List<ComplaintsMapper>>(filteredComplaints);
            return mappedComplaints;
        }

        public bool ReportAdmin(string id,string status)
        {
           var complaint = _context.Complaints.FirstOrDefault(c => c.ID == id);
            if (complaint == null)
            {
                return false;
            }
            complaint.Status = status;
            _context.SaveChanges();
            return true;
        }

        public UserMapper GetUserById(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            var mappedUser = _mapper.Map<UserMapper>(user);
            return mappedUser;
        }

       // public bool Wanrant(string producttypeid, )
    }
}
