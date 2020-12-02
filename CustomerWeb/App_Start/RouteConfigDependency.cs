using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerWeb.App_Start
{
    public static class RouteConfigDependency
    {
        public static void AddRouteConfigDependency(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddAuthorization();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
                cookieOptions.LoginPath = "/login";
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/login";
            });

            services.AddRazorPages()
                .AddRazorPagesOptions(options => {
                    options.RootDirectory = "/Views";
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public static void UseRouteDependency(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
