using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerWeb.Models.Customer.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastPurchase { get; set; }
        public int GenderId { get; set; }
        public int? CityId { get; set; }
        public int? RegionId { get; set; }
        public int? ClassificationId { get; set; }
        public int? UserId { get; set; }
    }
}
