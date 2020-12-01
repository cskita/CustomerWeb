using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Customer.InputModel;
using CustomerWeb.Models.Customer.ViewModel;

namespace CustomerWeb.Services.Customer
{
    public interface ICustomerService
    {
        BaseResult<IEnumerable<CustomerViewModel>> Get(CustomerInputModel customerInputModel);
    }
}
