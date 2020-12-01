using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Customer.ViewModel;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using CustomerWeb.Models.Customer.InputModel;

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

        public BaseResult<IEnumerable<CustomerViewModel>> Get(CustomerInputModel customerInputModel)
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodType.Get,
                    Body = customerInputModel
                };

                return _restAPIService.Request<IEnumerable<CustomerViewModel>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<CustomerViewModel>>.NotOK(e.Message);
            }
        }
    }
}
