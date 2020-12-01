using System;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using RegionModel = CustomerWeb.Models.Region.Region;

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

        public BaseResult<IEnumerable<RegionModel>> Get()
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodType.Get
                };

                return _restAPIService.Request<IEnumerable<RegionModel>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<RegionModel>>.NotOK(e.Message);
            }
        }

        public BaseResult<RegionModel> GetById(int? id)
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = $"{_route}/{id}",
                    MethodType = RequestMethodType.Get
                };

                return _restAPIService.Request<RegionModel>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<RegionModel>.NotOK(e.Message);
            }
        }

    }
}
