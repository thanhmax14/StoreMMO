namespace StoreMMO.Web.Models.ViewModels
{
    public class ProductViewModel
    {
        /* public string Id { get; set; }*/

        public string ProductTypeId { get; set; }

        public string Account { get; set; }

        public string Pwd { get; set; }
        public string StatusUpload { get; set; }

        public string Status { get; set; }

      public DateTimeOffset? CreatedDate { get; set; }

    }
}
