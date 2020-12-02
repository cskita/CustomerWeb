using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using CustomerWeb.Models.AppSettings;
using CustomerWeb.Models.Common;
using CustomerWeb.Extensions;
using CustomerWeb.Models.Enumerable;

namespace CustomerWeb.Services.Common
{
    public class RestAPIService : IRestAPIService
    {
        private readonly CustomerAPIOptions _customerAPIOptions;
        private readonly IHttpContextAccessor _session;
        private readonly IFieldService _fieldService;

        public RestAPIService(CustomerAPIOptions customerAPIOptions,
                              IHttpContextAccessor session,
                              IFieldService fieldService)
        {
            _customerAPIOptions = customerAPIOptions;
            _session = session;
            _fieldService = fieldService;
        }

        private HttpClient RequestHeader()
        {
            var userToken = _session.HttpContext.Session.GetUserToken();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (userToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = null;

            return httpClient;
        }

        public BaseResult<T> Request<T>(RequestAPI requestAPI) where T : class
        {
            try
            {
                string url = $"{_customerAPIOptions.EndPointUrl}/{requestAPI.Route}";

                HttpResponseMessage response;

                var httpClient = RequestHeader();

                if (requestAPI.MethodType == RequestMethodTypeEnum.Get)
                {
                    if (requestAPI.Body != null)
                    {
                        var queryParams = _fieldService.GetQueryString(requestAPI.Body);

                        if (!String.IsNullOrEmpty(queryParams))
                            url = $"{url}?{queryParams}";
                    }

                    response = httpClient.GetAsync(url).Result;
                }
                else
                {
                    response = httpClient.PostAsync(
                        url,
                        new StringContent(
                            JsonConvert.SerializeObject(requestAPI.Body),
                            Encoding.UTF8,
                            requestAPI.ContentType)).Result;
                }

                string content = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseAPI<T> responseAPI = JsonConvert.DeserializeObject<ResponseAPI<T>>(content);

                    if (responseAPI.Success && responseAPI.Data != null)
                        return BaseResult<T>.OK(responseAPI.Data);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return BaseResult<T>.NotOK("Unauthorized. Please logg in.", (int)response.StatusCode);
                }

                return BaseResult<T>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<T>.NotOK(e.Message);
            }
        }
    }
}
