using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerModel = CustomerWeb.Models.Customer;

namespace CustomerWeb.Services.Customer
{
    public interface ICustomerService
    {
        BaseResult<IEnumerable<CustomerModel.Customer>> Get(CustomerModel.CustomerFilter customerInputModel);
    }
}
