using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Region.ViewModel;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;

namespace CustomerWeb.Services.Region
{
    public class RegionService : IRegionService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "region";
        private string _contentType = "application/json";

        public RegionService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<IEnumerable<RegionViewModel>> Get()
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
                    var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<RegionViewModel>>>(content);

                    if (responseAPI.Success && responseAPI.Data != null)
                        return BaseResult<IEnumerable<RegionViewModel>>.OK(responseAPI.Data);
                }

                return BaseResult<IEnumerable<RegionViewModel>>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<RegionViewModel>>.NotOK(e.Message);
            }
        }
    }
}
