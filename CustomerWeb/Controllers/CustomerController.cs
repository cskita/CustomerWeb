﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Extensions;
using CustomerWeb.Services.Customer;
using CustomerWeb.ViewModels.Customer;
using CustomerWeb.Models.Common;
using CustomerWeb.Services.City;
using CustomerWeb.Models.City;
using CustomerWeb.Services.Classification;
using CustomerWeb.Services.Gender;
using CustomerWeb.Services.Region;
using CustomerWeb.Services.Seller;
using CustomerWeb.Services.Common;
using AutoMapper;
using CustomerWeb.Models;
using CustomerWeb.Models.Customer;

namespace CustomerWeb.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly ICityService _cityService;
        private readonly IClassificationService _classificationService;
        private readonly IGenderService _genderService;
        private readonly IRegionService _regionService;
        private readonly ISellerService _sellerService;
        private readonly IFieldService _fieldService;
        private readonly IMapper _mapper;

        private bool _isAuthenticated;

        public CustomerController(ICustomerService customerService,
                                  ICityService cityService,
                                  IClassificationService classificationService,
                                  IGenderService genderService,
                                  IRegionService regionService,
                                  ISellerService sellerService,
                                  IFieldService fieldService,
                                  IMapper mapper)
        {
            _customerService = customerService;
            _cityService = cityService;
            _classificationService = classificationService;
            _genderService = genderService;
            _regionService = regionService;
            _sellerService = sellerService;
            _fieldService = fieldService;
            _mapper = mapper;

            ViewData["Messages"] = null;
            ViewData["IsAdmin"] = false;
            ViewData["LoggedIn"] = _isAuthenticated;

        }

        public ActionResult Index()
        {
            _isAuthenticated = HttpContext.Session.IsAuthenticated();
            ViewData["IsAdmin"] = false;
            ViewData["LoggedIn"] = _isAuthenticated;

            if (_isAuthenticated)
                ViewData["IsAdmin"] = HttpContext.Session.GetUserSession().IsAdmin;

            var customer = GetFilters();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CustomerInputModel customerInputModel)
        {
            _isAuthenticated = HttpContext.Session.IsAuthenticated();

            var userSession = HttpContext.Session.GetUserSession();
            ViewData["IsAdmin"] = userSession?.IsAdmin ?? false;
            ViewData["LoggedIn"] = _isAuthenticated;

            ViewData["Messages"] = null;

            if (!(userSession?.IsAdmin ?? false))
            {
                customerInputModel.SellerId = userSession.Id;
            }

            var customerFilter = _mapper.Map<CustomerFilter>(customerInputModel);

            var result = _customerService.Get(customerFilter);

            ModelState.Clear();

            if (!result.Success)
            {
                ViewData["Messages"] = result.Messages;
            }
            else
            {
                ViewData["Customer"] = _mapper.Map<IEnumerable<CustomerViewModel>>(result.Data);

                var customer = GetFilters();
                customerInputModel.Cities = customer.Cities;
                customerInputModel.Classifications = customer.Classifications;

                if (customerInputModel.CityId.HasValue)
                    customerInputModel.Regions = GetRegionsByCityId(customerInputModel.CityId.Value);
                else
                    customerInputModel.Regions = customer.Regions;

                customerInputModel.Genders = customer.Genders;
                customerInputModel.Sellers = customer.Sellers;
            }

            return View(customerInputModel);
        }

        public CustomerInputModel GetFilters()
        {
            var customer = new CustomerInputModel()
            {
                Cities = _fieldService.GetDropDownList(_cityService.Get()),
                Classifications = _fieldService.GetDropDownList(_classificationService.Get()),
                Regions = _fieldService.GetDropDownList(_regionService.Get()),
                Genders = _fieldService.GetDropDownList(_genderService.Get()),
                Sellers = _fieldService.GetDropDownList(_sellerService.Get())
            };

            return customer;
        }

        [HttpGet]
        public ActionResult GetRegions(int? cityId)
        {
            if (cityId.HasValue)
                return Json(GetRegionsByCityId(cityId.Value));

            return Json(_fieldService.GetDropDownList(_regionService.Get()));
        }

        public IEnumerable<SelectListItem> GetRegionsByCityId(int cityId)
        {
            BaseResult<City> result = _cityService.GetById(cityId);

            if (result.Success && result.Data != null)
            {
                return _fieldService.GetDropDownList(_regionService.GetById(result.Data.RegionId));
            }

            return null;
        }
    }
}
