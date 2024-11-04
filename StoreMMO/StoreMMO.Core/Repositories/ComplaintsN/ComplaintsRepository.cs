using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

        public ComplaintsMapper GetAllReportAdminbyid(string idcomplaint)
        {
            var complaint = _context.Complaints
                .Where(c => c.ID == idcomplaint)
                .Include(c => c.OrderDetail)
                    .ThenInclude(od => od.orderBuy)
                .Include(c => c.OrderDetail.orderBuy.AppUser)
                .Include(c => c.OrderDetail.orderBuy.Store.User)
                .Include(c => c.OrderDetail.orderBuy.Store)
                .Include(c => c.OrderDetail.Product)
                    .ThenInclude(p => p.ProductType)
                .FirstOrDefault(c => c.Status.ToLower() == "reportadmin"); // Lọc complaint có status là "reportadmin"

            // Nếu không tìm thấy complaint phù hợp, trả về null
            if (complaint == null)
            {
                return null;
            }

            // Ánh xạ đối tượng complaint sang ComplaintsMapper
            var mappedComplaint = _mapper.Map<ComplaintsMapper>(complaint);
            return mappedComplaint;
        }
        public ComplaintsMapper GetAllNonebyid(string idcomplaint)
        {
            var complaint = _context.Complaints
                .Where(c => c.ID == idcomplaint )
                .Include(c => c.OrderDetail)
                    .ThenInclude(od => od.orderBuy)
                .Include(c => c.OrderDetail.orderBuy.AppUser)
                .Include(c => c.OrderDetail.orderBuy.Store.User)
                .Include(c => c.OrderDetail.orderBuy.Store)
                .Include(c => c.OrderDetail.Product)
                    .ThenInclude(p => p.ProductType)
                .FirstOrDefault(c => c.Status.ToLower() == "none"); // Lọc complaint có status là "reportadmin"

            // Nếu không tìm thấy complaint phù hợp, trả về null
            if (complaint == null)
            {
                return null;
            }

            // Ánh xạ đối tượng complaint sang ComplaintsMapper
            var mappedComplaint = _mapper.Map<ComplaintsMapper>(complaint);
            return mappedComplaint;
        }
        public bool Warrant(string idcomplaint, string ordercode)
        {
            bool a = false;
            var cominfor = GetAllReportAdminbyid(idcomplaint);
            var idstore = cominfor.OrderDetailmap.orderBuymap.StoreID;
            var iduser = cominfor.OrderDetailmap.orderBuymap.UserID;
            var idproducttype = cominfor.OrderDetailmap.productMapper.ProductTypeId;
            var idproduct = cominfor.OrderDetailmap.ProductID;
            var protype = _context.ProductTypes.FirstOrDefault(x => x.Id == idproducttype);

            if (protype != null)
            {
                protype.Stock = (int.Parse(protype.Stock) - 1).ToString();
                _context.ProductTypes.Update(protype);
                _context.SaveChanges();

                var pro = _context.Products.FirstOrDefault(x => x.ProductTypeId == idproducttype && x.Status.ToLower() == "new");

                if (pro != null)
                {
                    var proid = pro.Id.ToString();
                    pro.Status = "Paid";
                    pro.StatusUpload = DateTime.Now.ToString();
                    _context.Products.Update(pro);
                    _context.SaveChanges();

                    var orderBuy = new OrderBuy
                    {
                        ID = Guid.NewGuid().ToString(),
                        UserID = iduser,
                        StoreID = idstore,
                        ProductTypeId = idproducttype,
                        Status = "paid",
                        OrderCode = ordercode,
                        totalMoney = "0"
                    };

                    _context.Add(orderBuy);
                    _context.SaveChanges();

                    var orderDetail = new OrderDetail
                    {
                        ID = Guid.NewGuid().ToString(),
                        OrderBuyID = orderBuy.ID,
                        ProductID = proid,
                        quantity = "1",
                        stasusPayment = "paid",
                        AdminMoney = "0",
                        SellerMoney = "0",
                        Dates = DateTime.Now,
                        status = "refun",
                        Price = "0"
                    };

                    var comid = _context.Complaints.FirstOrDefault(x => x.ID == idcomplaint);
                    var od = _context.OrderDetails.FirstOrDefault(x => x.ID == comid.OrderDetailID);
                    od.status = "done";
                    _context.OrderDetails.Update(od);
                    _context.Add(orderDetail);
                    ReportAdmin(idcomplaint, "done");
                    _context.SaveChanges();

                    // Đặt a = true khi tất cả các bước đã thực hiện thành công
                    a = true;
                }
            }

            return a;
        }

        public bool BackMoney(string id)
        {
            var cominfor = GetAllReportAdminbyid(id);
            var priceStr = cominfor.OrderDetailmap.Price;
            var price = decimal.Parse(priceStr);
            var idseller = cominfor.OrderDetailmap.orderBuymap.StoreMap.UserId;
            var iduser = cominfor.OrderDetailmap.orderBuymap.UserID;
            var idorderdetail = cominfor.OrderDetailmap.ID;
            var ordercode = cominfor.OrderDetailmap.orderBuymap.OrderCode;

            var seller = _context.Users.FirstOrDefault(x => x.Id == idseller);
            var user = _context.Users.FirstOrDefault(x => x.Id == iduser);
            var orderdetail = _context.OrderDetails.FirstOrDefault(x => x.ID == idorderdetail);

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Cập nhật số dư cho người bán và người dùng
                seller.CurrentBalance -= price;
                user.CurrentBalance += price;

                _context.Users.Update(seller);
                _context.Users.Update(user);
                _context.SaveChanges();

                // Tạo giao dịch cho người bán
                var sellerTransaction = new Balance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = idseller,
                    TransactionType = "complaint",
                    Status = "PAID",
                    Amount = -price,
                    ApprovalDate = DateTime.Now,
                    Description = "Seller refunded money to user",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                // Tạo giao dịch cho người dùng
                var userTransaction = new Balance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = iduser,
                    TransactionType = "complaint",
                    Status = "PAID",
                    Amount = price,
                    ApprovalDate = DateTime.Now,
                    Description = "User received refund from seller",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                _context.Balances.AddRange(sellerTransaction, userTransaction);
                _context.SaveChanges();

                orderdetail.status = "done";
                _context.OrderDetails.Update(orderdetail);
                ReportAdmin(id, "done");

                _context.SaveChanges();
                transaction.Commit();

                return true; // Trả về true nếu giao dịch thành công
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false; // Trả về false nếu có lỗi xảy ra
            }
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

        public bool checkStockProductType(string idcomplaint)
        {
      
            var complaintbyid = GetAllReportAdminbyid(idcomplaint);

            var stock = int.Parse(complaintbyid.OrderDetailmap.productMapper.ProductTypemap.Stock);
            if(stock < 1)
            {
                return false;
            }
            else
            { return true;}

        }

        public bool BackMoneySeller(string idcomplant, string sellerid)
        {
            var cominfor = GetAllNonebyid(idcomplant);
            var priceStr = cominfor.OrderDetailmap.Price;
            var price = decimal.Parse(priceStr);
            var idseller = cominfor.OrderDetailmap.orderBuymap.StoreMap.UserId;
            var iduser = cominfor.OrderDetailmap.orderBuymap.UserID;
            var idorderdetail = cominfor.OrderDetailmap.ID;
            var ordercode = cominfor.OrderDetailmap.orderBuymap.OrderCode;

            var seller = _context.Users.FirstOrDefault(x => x.Id == idseller);
            var user = _context.Users.FirstOrDefault(x => x.Id == iduser);
            var orderdetail = _context.OrderDetails.FirstOrDefault(x => x.ID == idorderdetail);

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Cập nhật số dư cho người bán và người dùng
                seller.CurrentBalance -= price;
                user.CurrentBalance += price;

                _context.Users.Update(seller);
                _context.Users.Update(user);
                _context.SaveChanges();

                // Tạo giao dịch cho người bán
                var sellerTransaction = new Balance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = idseller,
                    TransactionType = "complaint",
                    Status = "PAID",
                    Amount = -price,
                    ApprovalDate = DateTime.Now,
                    Description = "Seller refunded money to user",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                // Tạo giao dịch cho người dùng
                var userTransaction = new Balance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = iduser,
                    TransactionType = "complaint",
                    Status = "PAID",
                    Amount = price,
                    ApprovalDate = DateTime.Now,
                    Description = "User received refund from seller",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                _context.Balances.AddRange(sellerTransaction, userTransaction);
                _context.SaveChanges();

                orderdetail.status = "done";
                _context.OrderDetails.Update(orderdetail);
                ReportAdmin(idseller, "done");

                _context.SaveChanges();
                transaction.Commit();

                return true; // Trả về true nếu giao dịch thành công
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

        public bool BackMoneyforseller(string id)
        {
            var cominfor = GetAllNonebyid(id);
            var priceStr = cominfor.OrderDetailmap.Price;
            var price = decimal.Parse(priceStr);
            var idseller = cominfor.OrderDetailmap.orderBuymap.StoreMap.UserId;
            var iduser = cominfor.OrderDetailmap.orderBuymap.UserID;
            var idorderdetail = cominfor.OrderDetailmap.ID;
            var ordercode = cominfor.OrderDetailmap.orderBuymap.OrderCode;

            var seller = _context.Users.FirstOrDefault(x => x.Id == idseller);
            var user = _context.Users.FirstOrDefault(x => x.Id == iduser);
            var orderdetail = _context.OrderDetails.FirstOrDefault(x => x.ID == idorderdetail);

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Cập nhật số dư cho người bán và người dùng
                seller.CurrentBalance -= price;
                user.CurrentBalance += price;

                _context.Users.Update(seller);
                _context.Users.Update(user);
                _context.SaveChanges();

                // Tạo giao dịch cho người bán
                var sellerTransaction = new Balance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = idseller,
                    TransactionType = "complaint",
                    Status = "PAID",
                    Amount = -price,
                    ApprovalDate = DateTime.Now,
                    Description = "Seller refunded money to user",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                // Tạo giao dịch cho người dùng
                var userTransaction = new Balance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = iduser,
                    TransactionType = "complaint",
                    Status = "PAID",
                    Amount = price,
                    ApprovalDate = DateTime.Now,
                    Description = "User received refund from seller",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                _context.Balances.AddRange(sellerTransaction, userTransaction);
                _context.SaveChanges();

                orderdetail.status = "done";
                _context.OrderDetails.Update(orderdetail);
                ReportAdmin(id, "done");

                _context.SaveChanges();
                transaction.Commit();

                return true; // Trả về true nếu giao dịch thành công
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

        public bool WarrantSeller(string idcomplaint, string ordercode,string sellerid)
        {
            bool a = false;
            var cominfor = GetAllNonebyid(idcomplaint);
            var idstore = cominfor.OrderDetailmap.orderBuymap.StoreID;
            var iduser = cominfor.OrderDetailmap.orderBuymap.UserID;
            var idproducttype = cominfor.OrderDetailmap.productMapper.ProductTypeId;
            var idproduct = cominfor.OrderDetailmap.ProductID;
            var protype = _context.ProductTypes.FirstOrDefault(x => x.Id == idproducttype);

            if (protype != null)
            {
                protype.Stock = (int.Parse(protype.Stock) - 1).ToString();
                _context.ProductTypes.Update(protype);
                _context.SaveChanges();

                var pro = _context.Products.FirstOrDefault(x => x.ProductTypeId == idproducttype && x.Status.ToLower() == "new");

                if (pro != null)
                {
                    var proid = pro.Id.ToString();
                    pro.Status = "Paid";
                    pro.StatusUpload = DateTime.Now.ToString();
                    _context.Products.Update(pro);
                    _context.SaveChanges();

                    var orderBuy = new OrderBuy
                    {
                        ID = Guid.NewGuid().ToString(),
                        UserID = iduser,
                        StoreID = idstore,
                        ProductTypeId = idproducttype,
                        Status = "paid",
                        OrderCode = ordercode,
                        totalMoney = "0"
                    };

                    _context.Add(orderBuy);
                    _context.SaveChanges();

                    var orderDetail = new OrderDetail
                    {
                        ID = Guid.NewGuid().ToString(),
                        OrderBuyID = orderBuy.ID,
                        ProductID = proid,
                        quantity = "1",
                        stasusPayment = "paid",
                        AdminMoney = "0",
                        SellerMoney = "0",
                        Dates = DateTime.Now,
                        status = "refun",
                        Price = "0"
                    };

                    var comid = _context.Complaints.FirstOrDefault(x => x.ID == idcomplaint);
                    var od = _context.OrderDetails.FirstOrDefault(x => x.ID == comid.OrderDetailID);
                    od.status = "done";
                    _context.OrderDetails.Update(od);
                    _context.Add(orderDetail);
                    ReportAdmin(idcomplaint, "done");
                    _context.SaveChanges();

                    // Đặt a = true khi tất cả các bước đã thực hiện thành công
                    a = true;
                }
            }

            return a;
        }

    }
    // public bool Wanrant(string producttypeid, )
}

