using System;
using System.Net;
using Newtonsoft.Json;
using CustomerWeb.Models.Authorization.InputModel;
using CustomerWeb.Models.Authorization.ViewModel;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Services.Common;

namespace CustomerWeb.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRestAPIService _restAPIService;

        private string _route = "auth";
        private string _contentType = "application/json";

        public AuthorizationService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public BaseResult<AuthorizationViewModel> RequestToken(AuthorizationInputModel login)
        {
            try
            {
                var response = _restAPIService.RequestUserSession(new RequestAPI { 
                    Body = login,
                    ContentType = _contentType,
                    Route = _route,
                    MethodType = RequestMethodType.Post
                });

                string content = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<AuthorizationViewModel>>(content);

                    if (responseAPI.Success && responseAPI.Data != null);
                        return BaseResult<AuthorizationViewModel>.OK(responseAPI.Data);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return BaseResult<AuthorizationViewModel>.NotOK("The email and / or password entered is invalid.Please try again.");
                }
                
                return BaseResult<AuthorizationViewModel>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<AuthorizationViewModel>.NotOK(e.Message);
            }
        }
    }
}
