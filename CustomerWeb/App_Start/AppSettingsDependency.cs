using CustomerWeb.Models.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CustomerWeb.App_Start
{
    public static class AppSettingsDependency
    {
        public static void AddAppSettingsDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CustomerAPIOptions>(configuration.GetSection("CustomerAPI"))
                    .AddSingleton(sp => sp.GetRequiredService<IOptions<CustomerAPIOptions>>().Value);
        }
    }
}
