using System;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using CustomerModel = CustomerWeb.Models.Customer;

namespace CustomerWeb.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "customer";
        private string _contentType = "application/json";

        public CustomerService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<IEnumerable<CustomerModel.Customer>> Get(CustomerModel.CustomerFilter customerInputModel)
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodTypeEnum.Get,
                    Body = customerInputModel
                };

                return _restAPIService.Request<IEnumerable<CustomerModel.Customer>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<CustomerModel.Customer>>.NotOK(e.Message);
            }
        }
    }
}
