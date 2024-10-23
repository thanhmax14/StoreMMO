namespace StoreMMO.Core.ViewModels
{
    public class FeedBackViewModels
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string StoreDetailId { get; set; }

        public string Comments { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public double? Stars { get; set; }

        public string? Relay { get; set; }

        public DateTimeOffset? DateRelay { get; set; }

        public bool? IsActive { get; set; }
        public string OrderBuyId { get; set; }

        public string StoreName { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string StoreOwnerId { get; set; }

    }
}
