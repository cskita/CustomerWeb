using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerWeb.Models.Customer.InputModel
{
    public class CustomerInputModel
    {
        public string Name { get; set; }
        public int? GenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public int? CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public int? RegionId { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }
        public int? ClassificationId { get; set; }
        public IEnumerable<SelectListItem> Classifications { get; set; }
        public int? SellerId { get; set; }
        public IEnumerable<SelectListItem> Sellers { get; set; }

        [DataType(DataType.Date)]
        public string LastPurchaseInitial { get; set; }

        [DataType(DataType.Date)]
        public string LastPurchaseFinal { get; set; }
    }
}
