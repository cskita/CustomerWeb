using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CustomerWeb.Filters;

namespace CustomerWeb.App_Start
{
    public static class SessionDependency
    {
        public static void AddSessionDependency(this IServiceCollection services)
        {
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(5));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(o => o.Filters.Add(new AthorizationFilter()));
        }
    }
}
