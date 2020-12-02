using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CustomerWeb.Services.Authorization;
using CustomerWeb.Services.City;
using CustomerWeb.Services.Classification;
using CustomerWeb.Services.Common;
using CustomerWeb.Services.Customer;
using CustomerWeb.Services.Gender;
using CustomerWeb.Services.Region;
using CustomerWeb.Services.Seller;

namespace CustomerWeb.App_Start
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRestAPIService, RestAPIService>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ISellerService, SellerService>();
        }
    }
}
