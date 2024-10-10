namespace StoreMMO.Web.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Stock { get; set; }

        public double? Price { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public bool? IsActive { get; set; }

    }
}
