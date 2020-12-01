using System;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using CityModel = CustomerWeb.Models.City.City;

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

        public BaseResult<IEnumerable<CityModel>> Get()
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodType.Get
                };

                return _restAPIService.Request<IEnumerable<CityModel>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<CityModel>>.NotOK(e.Message);
            }
        }

        public BaseResult<CityModel> GetById(int? id)
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = $"{_route}/{id}",
                    MethodType = RequestMethodType.Get
                };

                return _restAPIService.Request<CityModel>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<CityModel>.NotOK(e.Message);
            }
        }
    }
}
