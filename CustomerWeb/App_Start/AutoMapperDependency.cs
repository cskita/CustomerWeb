using CustomerWeb.Adapter;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerWeb.App_Start
{
    public static class AutoMapperDependency
    {
        public static void AddAutoMapperDependency(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperAdapter.ConfigureAutoMapper());
        }
    }
}
