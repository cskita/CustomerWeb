using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Extensions;
using CustomerWeb.Services.Customer;
using CustomerWeb.Models.Customer.InputModel;
using CustomerWeb.Models.Customer.ViewModel;
using CustomerWeb.Models.Common;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerWeb.Services.City;
using CustomerWeb.Models.City.ViewModel;
using CustomerWeb.Services.Classification;
using CustomerWeb.Services.Gender;
using CustomerWeb.Services.Region;
using CustomerWeb.Models.Classification.ViewModel;
using CustomerWeb.Models.Gender.ViewModel;
using CustomerWeb.Models.Region.ViewModel;
using Microsoft.AspNetCore.Authorization;

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

        private bool _isAuthenticated;

        public CustomerController(ICustomerService customerService,
                                  ICityService cityService,
                                  IClassificationService classificationService,
                                  IGenderService genderService,
                                  IRegionService regionService)
        {
            _customerService = customerService;
            _cityService = cityService;
            _classificationService = classificationService;
            _genderService = genderService;
            _regionService = regionService;

            ViewData["Messages"] = null;
            ViewData["IsAdmin"] = false;
            ViewData["LoggedIn"] = _isAuthenticated;

            InitializeDropDowns();
        }

        public ActionResult Index()
        {
            _isAuthenticated = HttpContext.Session.IsAuthenticated();
            ViewData["IsAdmin"] = false;
            ViewData["LoggedIn"] = _isAuthenticated;

            if (_isAuthenticated)
                ViewData["IsAdmin"] = HttpContext.Session.GetUserSession().IsAdmin;

            InitializeDropDowns();

            return View();
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
                customerInputModel.UserId = userSession.Id;
            }

            BaseResult<IEnumerable<CustomerViewModel>> result = _customerService.Get(customerInputModel);

            if (!result.Success)
            {
                ModelState.Clear();
                ViewData["Messages"] = result.Messages;
            }
            else
            {
                ViewData["Customer"] = result.Data;
            }

            InitializeDropDowns();

            return View();
        }

        #region DropDown
        private void InitializeDropDowns()
        {
            InitializeDropDownCities();
            InitializeDropDownClassifications();
            InitializeDropDownGenders();
            InitializeDropDownRegions();
        }

        private void InitializeDropDownCities()
        {
            BaseResult<IEnumerable<CityViewModel>> result = _cityService.Get();

            ViewData["Cities"] = null;
            if (result.Success)
            {
                CreateDropDown("Cities", result.Data);
            }
        }

        private void InitializeDropDownClassifications()
        {
            BaseResult<IEnumerable<ClassificationViewModel>> result = _classificationService.Get();

            ViewData["Classifications"] = null;
            if (result.Success)
            {
                CreateDropDown("Classifications", result.Data);
            }
        }

        private void InitializeDropDownGenders()
        {
            BaseResult<IEnumerable<GenderViewModel>> result = _genderService.Get();

            ViewData["Genders"] = null;
            if (result.Success)
            {
                CreateDropDown("Genders", result.Data);
            }
        }

        private void InitializeDropDownRegions()
        {
            BaseResult<IEnumerable<RegionViewModel>> result = _regionService.Get();

            ViewData["Regions"] = null;
            if (result.Success)
            {
                CreateDropDown("Regions", result.Data);
            }
        }

        private void CreateDropDown(string name, IEnumerable<object> data)
        {
            ViewData[name] = new SelectList
            (
                data,
                "Id",
                "Name"
            );
        }
        #endregion
    }
}
