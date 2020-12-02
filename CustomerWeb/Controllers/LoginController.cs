using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Models.Authorization.InputModel;
using CustomerWeb.Extensions;
using Microsoft.AspNetCore.Authorization;
using IAuthorizationService = CustomerWeb.Services.Authorization.IAuthorizationService;
using CustomerWeb.Models.Enumerable;

namespace CustomerWeb.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        private readonly IAuthorizationService _authorizationService;

        public LoginController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            var isAthenticated = HttpContext.Session.IsAuthenticated();

            if (isAthenticated)
                return RedirectToActionPermanent("Index", "Customer");

            return View();
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionEnum.UserSession.ToString());

            return View("Login");
        }

        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AuthorizationInputModel authorizationInputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorizationInputModel);
            }

            ViewData["Messages"] = null;
            authorizationInputModel.Password = AuthorizationInputModel.GetPasswordHash(authorizationInputModel.Password);

            var result = _authorizationService.RequestToken(authorizationInputModel);

            if (!result.Success)
            {
                ModelState.Clear();
                ViewData["Messages"] = result.Messages;

                return View();
            }

            return RedirectToActionPermanent("Index", "Customer");

        }


    }
}
