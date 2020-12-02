namespace CustomerWeb.Models.Customer
{
    public class CustomerFilter
    {
        public string Name { get; set; }
        public int? GenderId { get; set; }
        public int? CityId { get; set; }
        public int? RegionId { get; set; }
        public int? ClassificationId { get; set; }
        public int? SellerId { get; set; }
        public string LastPurchaseInitial { get; set; }
        public string LastPurchaseFinal { get; set; }
    }
}
