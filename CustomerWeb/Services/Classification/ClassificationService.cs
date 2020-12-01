using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Classification.ViewModel;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;

namespace CustomerWeb.Services.Classification
{
    public class ClassificationService : IClassificationService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "classification";
        private string _contentType = "application/json";

        public ClassificationService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<IEnumerable<ClassificationViewModel>> Get()
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
                    var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<ClassificationViewModel>>>(content);

                    if (responseAPI.Success && responseAPI.Data != null)
                        return BaseResult<IEnumerable<ClassificationViewModel>>.OK(responseAPI.Data);
                }

                return BaseResult<IEnumerable<ClassificationViewModel>>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<ClassificationViewModel>>.NotOK(e.Message);
            }
        }
    }
}
