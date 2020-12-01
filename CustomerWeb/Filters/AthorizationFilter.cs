using CustomerWeb.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace CustomerWeb.Filters
{
    public class AthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestPath = filterContext.HttpContext.Request.Path;

            if (!requestPath.Value.Contains("login"))
            {
                var isAthenticated = filterContext.HttpContext.Session.IsAuthenticated();

                if (!isAthenticated)
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                area = "",
                                controller = "Login",
                                action = "Login"
                            }));
            }
        }
    }
}
