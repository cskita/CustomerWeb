using System;
using System.ComponentModel;

namespace CustomerWeb.Models.Customer.InputModel
{
    public class CustomerInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [Description("Last Purchase")]
        public DateTime LastPurchase { get; set; }
        public int GenderId { get; set; }
        public int? CityId { get; set; }
        public int? RegionId { get; set; }
        public int? ClassificationId { get; set; }
        public int? UserId { get; set; }
    }
}
