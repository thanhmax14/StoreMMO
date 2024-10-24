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
        public IEnumerable<ComplaintsMapper> GetAll()
        {
            var complaints = _context.Complaints
                .Where(c => c.Status == "none")      
          .Include(c => c.OrderDetail)                        // Include OrderDetail của Complaint
              .ThenInclude(od => od.orderBuy)                // Then Include OrderBuy của OrderDetail
          .Include(c => c.OrderDetail.orderBuy.AppUser)       // Include AppUser của OrderBuy
          .Include(c => c.OrderDetail.orderBuy.Store)         // Include Store của OrderBuy
          .Include(c => c.OrderDetail.Product)                // Include Product của OrderDetail
              .ThenInclude(p => p.ProductType)               // Then Include ProductType của Product
          .ToList();


            var mappedComplaints = _mapper.Map<List<ComplaintsMapper>>(complaints);

            return mappedComplaints;
        }

        public IEnumerable<ComplaintsMapper> GetAllReportAdmin()
        {
            var complaints = _context.Complaints
                .Where(c => c.Status == "ReportAdmin")
          .Include(c => c.OrderDetail)                        // Include OrderDetail của Complaint
              .ThenInclude(od => od.orderBuy)                // Then Include OrderBuy của OrderDetail
          .Include(c => c.OrderDetail.orderBuy.AppUser)       // Include AppUser của OrderBuy
          .Include(c => c.OrderDetail.orderBuy.Store)         // Include Store của OrderBuy
          .Include(c => c.OrderDetail.Product)                // Include Product của OrderDetail
              .ThenInclude(p => p.ProductType)               // Then Include ProductType của Product
          .ToList();


            var mappedComplaints = _mapper.Map<List<ComplaintsMapper>>(complaints);

            return mappedComplaints;
        }

        public bool ReportAdmin(string id)
        {
           var complaint = _context.Complaints.FirstOrDefault(c => c.ID == id);
            if (complaint == null)
            {
                return false;
            }
            complaint.Status = "ReportAdmin";
            _context.SaveChanges();
            return true;
        }
    }
}
