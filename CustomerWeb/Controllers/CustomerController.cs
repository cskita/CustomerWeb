using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Extensions;
using CustomerWeb.Services.Customer;
using CustomerWeb.Models.Customer.InputModel;
using CustomerWeb.Models.Customer.ViewModel;
using CustomerWeb.Models.Common;
using CustomerWeb.Services.City;
using CustomerWeb.Models.City;
using CustomerWeb.Services.Classification;
using CustomerWeb.Services.Gender;
using CustomerWeb.Services.Region;
using CustomerWeb.Models.Classification;
using CustomerWeb.Models.Gender;
using CustomerWeb.Models.Region;
using CustomerWeb.Models.Seller;
using CustomerWeb.Services.Seller;
using CustomerWeb.Services.Common;

namespace CustomerWeb.Controllers
{
    [AllowAnonymous]
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly ICityService _cityService;
        private readonly IClassificationService _classificationService;
        private readonly IGenderService _genderService;
        private readonly IRegionService _regionService;
        private readonly ISellerService _sellerService;
        private readonly IFieldService _fieldService;

        private bool _isAuthenticated;

        public CustomerController(ICustomerService customerService,
                                  ICityService cityService,
                                  IClassificationService classificationService,
                                  IGenderService genderService,
                                  IRegionService regionService,
                                  ISellerService sellerService,
                                  IFieldService fieldService)
        {
            _customerService = customerService;
            _cityService = cityService;
            _classificationService = classificationService;
            _genderService = genderService;
            _regionService = regionService;
            _sellerService = sellerService;
            _fieldService = fieldService;

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

        [HttpGet]
        public ActionResult GetRegions(int? cityId)
        {
            if (cityId.HasValue)
                return GetRegionsByCityId(cityId.Value);

            return Json(_fieldService.GetDropDownList(_regionService.Get()));
        }

        public ActionResult GetRegionsByCityId(int cityId)
        {
            BaseResult<City> result = _cityService.GetById(cityId);

            if (result.Success && result.Data != null)
            {
                return Json(_fieldService.GetDropDownList(_regionService.GetById(result.Data.RegionId)));
            }

            return null;
        }

        [HttpGet]
        public ActionResult GetCitiesByRegionId(int? regionId)
        {
            BaseResult<City> result = _cityService.GetById(regionId);

            if (result.Success)
            {
                IEnumerable<SelectListItem> regions = new SelectList
                    (
                        (System.Collections.IEnumerable)result.Data,
                        "Id",
                        "Name"
                    );

                return Json(regions);
            }

            return null;
        }

        [HttpPost]
        [Route("customer/list")]
        public JsonResult List(CustomerInputModel customerInputModel)
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

            var result = _customerService.Get(customerInputModel);

            return Json(result);
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

            BaseResult<IEnumerable<CustomerViewModel>> result = _customerService.Get(customerInputModel);

            ModelState.Clear();

            if (!result.Success)
            {
                ViewData["Messages"] = result.Messages;
            }
            else
            {
                ViewData["Customer"] = result.Data;
            }


            var customer = GetFilters();
            customerInputModel.Cities = customer.Cities;
            customerInputModel.Classifications = customer.Classifications;
            customerInputModel.Regions = customer.Regions;
            customerInputModel.Genders = customer.Genders;
            customerInputModel.Sellers = customer.Sellers;

            return View(customerInputModel);
        }

        #region DropDown

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

        private IEnumerable<SelectListItem> GetRegions()
        {
            BaseResult<IEnumerable<Region>> result = _regionService.Get();

            if (result.Success)
            {
                return CreateDropDown(result.Data);
            }

            return null;
        }

        private SelectList CreateDropDown(IEnumerable<object> data)
        {
            return new SelectList
            (
                data,
                "Id",
                "Name"
            );
        }
        #endregion
    }
}
