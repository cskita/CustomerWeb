using System;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using ClassificationModel = CustomerWeb.Models.Classification.Classification;

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

        public BaseResult<IEnumerable<ClassificationModel>> Get()
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodType.Get
                };

                return _restAPIService.Request<IEnumerable<ClassificationModel>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<ClassificationModel>>.NotOK(e.Message);
            }
        }
    }
}
