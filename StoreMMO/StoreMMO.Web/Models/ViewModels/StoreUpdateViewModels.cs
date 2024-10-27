using Microsoft.AspNetCore.Mvc.Rendering;
using StoreMMO.Core.Models;

namespace StoreMMO.Web.Models.ViewModels
{
    public class StoreUpdateViewModels
    {
        public string Id { get; set; } // Id từ StoreDetails
        public string Name { get; set; }
        public string SubDescription { get; set; }
        public string DescriptionDetail { get; set; } // Thêm DescriptionDetail
        public DateTimeOffset CreatedDate { get; set; }
        public string Img { get; set; } // Thêm Img
        public IFormFile InputImage { get; set; } // File upload
        public string CategoryId { get; set; }
        public string StoreTypeId { get; set; }
        public string StoreId { get; set; }
        public string CategoryName { get; set; }
        public string StoreTypeName { get; set; }
        public IEnumerable<SelectListItem> CategoryOptions { get; set; } // Dropdown cho Category
        public IEnumerable<SelectListItem> StoreTypeOptions { get; set; } // Dropdown cho StoreType
    }
}
