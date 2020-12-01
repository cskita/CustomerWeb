using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.City.ViewModel;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;

namespace CustomerWeb.Services.City
{
    public class CityService : ICityService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "city";
        private string _contentType = "application/json";

        public CityService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<IEnumerable<CityViewModel>> Get()
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
                    var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<CityViewModel>>>(content);

                    if (responseAPI.Success && responseAPI.Data != null)
                        return BaseResult<IEnumerable<CityViewModel>>.OK(responseAPI.Data);
                }

                return BaseResult<IEnumerable<CityViewModel>>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<CityViewModel>>.NotOK(e.Message);
            }
        }
    }
}
