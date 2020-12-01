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
                var response = _restAPIService.Request(new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodType.Get
                });

                string content = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<CustomerViewModel>>>(content);

                    if (responseAPI.Success && responseAPI.Data != null)
                        return BaseResult<IEnumerable<CustomerViewModel>>.OK(responseAPI.Data);
                }

                return BaseResult<IEnumerable<CustomerViewModel>>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<CustomerViewModel>>.NotOK(e.Message);
            }
        }
    }
}
