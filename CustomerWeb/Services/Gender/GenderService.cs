using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Gender.ViewModel;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;

namespace CustomerWeb.Services.Gender
{
    public class GenderService : IGenderService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "gender";
        private string _contentType = "application/json";

        public GenderService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<IEnumerable<GenderViewModel>> Get()
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
                    var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<GenderViewModel>>>(content);

                    if (responseAPI.Success && responseAPI.Data != null)
                        return BaseResult<IEnumerable<GenderViewModel>>.OK(responseAPI.Data);
                }

                return BaseResult<IEnumerable<GenderViewModel>>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<GenderViewModel>>.NotOK(e.Message);
            }
        }
    }
}
