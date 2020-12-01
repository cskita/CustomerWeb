using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerWeb.App_Start
{
    public static class RouteConfig
    {
        public static void AddRouteConfig(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
                cookieOptions.LoginPath = "/login";
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/login";
                config.AccessDeniedPath = "/login";
            });

            services.AddRazorPages()
                .AddRazorPagesOptions(options => {
                    options.RootDirectory = "/Views";
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
    }
}
