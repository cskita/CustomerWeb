using System;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using GenderModel = CustomerWeb.Models.Gender.Gender;

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

        public BaseResult<IEnumerable<GenderModel>> Get()
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodTypeEnum.Get
                };

                return _restAPIService.Request<IEnumerable<GenderModel>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<GenderModel>>.NotOK(e.Message);
            }
        }
    }
}
