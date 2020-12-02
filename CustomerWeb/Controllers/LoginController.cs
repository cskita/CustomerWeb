using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CustomerWeb.Extensions;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Models.Authorization;
using CustomerWeb.ViewModels.Login;
using IAuthorizationService = CustomerWeb.Services.Authorization.IAuthorizationService;

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
        public IActionResult Login(LoginInputModel loginInputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInputModel);
            }

            ViewData["Messages"] = null;
            var authorizationRequest = new AuthorizationRequest
            {
                Email = loginInputModel.Email,
                Password = AuthorizationRequest.GetPasswordHash(loginInputModel.Password)
            };

            var result = _authorizationService.RequestUserSession(authorizationRequest);

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
