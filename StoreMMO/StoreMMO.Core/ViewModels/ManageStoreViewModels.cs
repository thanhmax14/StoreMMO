using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class ManageStoreViewModels
    {
        public string Id { get; set; }  // Thay đổi kiểu dữ liệu thành Guid
        [Key]
        public string? StoreName { get; set; }
        public string? CategoryName { get; set; }
        public string? PriceRange { get; set; }
        public double Commission { get; set; }
        public int TotalStock { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string? IsAccept { get; set; }
        public string StoreDetailId { get; set; }
        //public IEnumerable<SelectListItem> CategoryOptions { get; set; } // Dropdown cho Category
        //public IEnumerable<SelectListItem> StoreTypeOptions { get; set; } // Dropdown cho StoreType
        //public string Img { get; set; }
        //public string StoreTypeId { get; set; }
        //public string CategoryId { get; set; }
        //public string DescriptionDetail { get; set; }
        //public string SubDescription { get; set; }
        //public string Name { get; set; }
        //public IFormFile InputImage { get; set; }
    }
}
