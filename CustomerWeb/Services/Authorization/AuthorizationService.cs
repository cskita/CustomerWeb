using System.Net;
using Microsoft.AspNetCore.Http;
using CustomerWeb.Models.Authorization;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;
using CustomerWeb.Extensions;

namespace CustomerWeb.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRestAPIService _restAPIService;
        private readonly IHttpContextAccessor _session;

        private string _route = "auth";
        private string _contentType = "application/json";

        public AuthorizationService(IRestAPIService restAPIService,
                                    IHttpContextAccessor session)
        {
            _restAPIService = restAPIService;
            _session = session;
        }

        public BaseResult<AuthorizationResponse> RequestUserSession(AuthorizationRequest login)
        {
            var requestAPI = new RequestAPI
            {
                Body = login,
                ContentType = _contentType,
                Route = _route,
                MethodType = RequestMethodTypeEnum.Post
            };

            var responseAPI = _restAPIService.Request<AuthorizationResponse>(requestAPI);

            if (responseAPI.Success && responseAPI.Data != null)
            {
                _session.HttpContext.Session.SetUserSession(responseAPI.Data);
            }
            else if (responseAPI.Code == (int)HttpStatusCode.Unauthorized)
            {
                return BaseResult<AuthorizationResponse>.NotOK("The email and/or password entered is invalid. Please try again.", responseAPI.Code);
            }

            return responseAPI;
        }
    }
}
