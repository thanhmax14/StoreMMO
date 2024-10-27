using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using BusinessLogic.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreMMO.Web.Pages.Seller.Complain
{
    public class ComplainListModel : PageModel
    {


        private readonly IComplaintsService _complaintsServices;
        private readonly AppDbContext _context;
        [TempData]
        public string successreportad { get; set; }
        [TempData]
        public string failreportad { get; set; }
        //    public string UserId { get; private set; }

        //  var currentUserId = HttpContext.Session.GetString("UserID");

        public ComplainListModel(IComplaintsService complaintsServices, AppDbContext appDbContext)
        {
            _complaintsServices = complaintsServices;
            _context = appDbContext;
        }

        public IEnumerable<ComplaintsMapper> listcomplaints { get; set; } = new List<ComplaintsMapper>();

        public void OnGet()
        {
            //  string UserId = HttpContext.Session.GetString("UserID");
            string UserId = "1f0dbbe2-2a81-43e9-8272-117507ac9c45";
            listcomplaints = _complaintsServices.GetAll(UserId);
        }
        [BindProperty]
        public string MyProperty { get; set; }

        public IActionResult OnPostReportAdmin()
        {
            var id = Request.Form["id"];
            var result = _complaintsServices.ReportAdmin(id, "ReportAdmin");

            if (result)
            {
                successreportad = "Report Admin success";
                return RedirectToPage("ComplainList");
            }
            else
            {
                failreportad = "Report Admin fail";
            }
            return Page();
        }

        public IActionResult OnPostBackMoney()
        {
            var priceStr = Request.Form["price"];
            var idseller = Request.Form["idseller"].ToString();
            var iduser = Request.Form["iduser"].ToString();
            var ordercode = Request.Form["ordercode"].ToString();
            var idorderdetail = Request.Form["idorderdetail"].ToString();

            if (!decimal.TryParse(priceStr, out var price))
            {
                ModelState.AddModelError("", "Invalid price format.");
                return Page();
            }

            var seller = _context.Users.FirstOrDefault(x => x.Id == idseller);
            var user = _context.Users.FirstOrDefault(x => x.Id == iduser);
            var orderdetail = _context.OrderDetails.FirstOrDefault(x => x.ID == idorderdetail);

            if (seller == null || user == null || orderdetail == null)
            {
                ModelState.AddModelError("", "User or seller or order detail not found.");
                return Page();
            }

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
                    Status = Constants.BalanceStatus.PAID.ToString(),
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
                    Status = Constants.BalanceStatus.PAID.ToString(),
                    Amount = price,
                    ApprovalDate = DateTime.Now,
                    Description = "User received refund from seller",
                    TransactionDate = DateTime.Now,
                    OrderCode = ""
                };

                _context.Balances.AddRange(sellerTransaction, userTransaction);
                _context.SaveChanges();

                
                // Cập nhật trạng thái đơn hàng và khiếu nại
                orderdetail.status = "done";
                _context.OrderDetails.Update(orderdetail);
                _complaintsServices.ReportAdmin(Request.Form["id"], "done");

                _context.SaveChanges();
                transaction.Commit();
                successreportad = "Refund success";
                return RedirectToPage("ComplainList");
            }
            catch (Exception)
            {
                failreportad = "Refund fail";
                transaction.Rollback();
                ModelState.AddModelError("", "An error occurred while processing the refund.");
                //  return RedirectToPage("ComplainList");
                return Page();
            }
        }

        public IActionResult OnPostWarrant()
        {
            var idstore = Request.Form["idstore"].ToString();
            var iduser = Request.Form["iduser"].ToString();
            var idproducttype = Request.Form["idproducttype"].ToString();
            var idproduct = Request.Form["idproduct"].ToString();
            var protype = _context.ProductTypes.FirstOrDefault(x => x.Id == idproducttype);

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
                    OrderCode = BusinessLogic.Services.Encrypt.EncryptSupport.GenerateRandomString(10),
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
                var comid = _context.Complaints.FirstOrDefault(x => x.ID == Request.Form["id"].ToString());
               
                var od = _context.OrderDetails.FirstOrDefault(x => x.ID == comid.OrderDetailID);
                od.status = "done";
                _context.OrderDetails.Update(od);
                _context.Add(orderDetail);
                _complaintsServices.ReportAdmin(Request.Form["id"].ToString(), "done");
                _context.SaveChanges();

                successreportad = "Warrant success";
            }
            return RedirectToPage("ComplainList");
        }

    }
}
