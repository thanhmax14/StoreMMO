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
    }
}
