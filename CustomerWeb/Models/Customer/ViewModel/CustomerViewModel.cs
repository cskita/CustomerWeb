using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerWeb.Models.Customer.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime LastPurchase { get; set; }
        public int GenderId { get; set; }
        public string GenderDescription { get; set; }
        public int? CityId { get; set; }
        public string CityDescription { get; set; }
        public int? RegionId { get; set; }
        public string RegionDescription { get; set; }
        public int? ClassificationId { get; set; }
        public string ClassificationDescription { get; set; }
        public int? SellerId { get; set; }
        public string SellerName { get; set; }
    }
}
