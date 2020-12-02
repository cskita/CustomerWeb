using System;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using SellerModel = CustomerWeb.Models.Seller.Seller;

namespace CustomerWeb.Services.Seller
{
    public class SellerService : ISellerService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "seller";
        private string _contentType = "application/json";

        public SellerService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<IEnumerable<SellerModel>> Get()
        {
            try
            {
                var requestAPI = new RequestAPI
                {
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodTypeEnum.Get
                };

                return _restAPIService.Request<IEnumerable<SellerModel>>(requestAPI);
            }
            catch (Exception e)
            {
                return BaseResult<IEnumerable<SellerModel>>.NotOK(e.Message);
            }
        }

    }
}
