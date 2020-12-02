using AutoMapper;
using CustomerWeb.Models.Customer;
using CustomerWeb.ViewModels.Customer;

namespace CustomerWeb.Adapter
{
    public class AutoMapperAdapter
    {
        public static IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region ViewModel
                cfg.CreateMap<Customer, CustomerViewModel>();
                #endregion

                #region InputModel
                cfg.CreateMap<CustomerInputModel, CustomerFilter>();
                #endregion
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
