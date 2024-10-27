using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
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
            var complaints = _context.Complaints
                .Where(c => c.Status == "none" &&
                c.OrderDetail.orderBuy.Store.User.Id == id
                )      
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

		public async Task<bool> AddAsync(complantViewModels complaints)
		{
			try
			{
				var complaint = new Complaint
				{
					ID = complaints.ID,
					CreateDate = complaints.CreateDate,
					Description = complaints.Description,
					OrderDetailID = complaints.OrderDetailID,
					Reply = complaints.Reply,
					Status = complaints.Status,
				};

				await _context.Complaints.AddAsync(complaint);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}


		public async Task<bool> EditAsync(complantViewModels complaintsMapper)
		{
			try
			{
				var complaint = await _context.Complaints.FindAsync(complaintsMapper.ID);
				if (complaint == null)
				{
					return false;
				}

				complaint.CreateDate = complaintsMapper.CreateDate;
				complaint.Description = complaintsMapper.Description;
				complaint.OrderDetailID = complaintsMapper.OrderDetailID;
				complaint.Reply = complaintsMapper.Reply;
				complaint.Status = complaintsMapper.Status;

				_context.Complaints.Update(complaint);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}


		public async Task<bool> DeleteAsync(complantViewModels complaintsMapper)
		{
			try
			{
				var complaint = await _context.Complaints.FindAsync(complaintsMapper.ID);
				if (complaint == null)
				{
					return false;
				}

				_context.Complaints.Remove(complaint);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public async Task<complantViewModels> GetByIDAsync(string id)
		{
			try
			{
				var complaint = await _context.Complaints.FindAsync(id);
				if (complaint == null)
				{
					return null;
				}

				return new complantViewModels
				{
					ID = complaint.ID,
					CreateDate = complaint.CreateDate,
					Description = complaint.Description,
					OrderDetailID = complaint.OrderDetailID,
					Reply = complaint.Reply,
					Status = complaint.Status,
				};
			}
			catch (Exception)
			{
				return null;
			}
		}

		
	}
}
