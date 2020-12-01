using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using CustomerWeb.Models.AppSettings;
using CustomerWeb.Models.Authorization.ViewModel;
using CustomerWeb.Models.Common;
using Microsoft.AspNetCore.Http;
using CustomerWeb.Extensions;
using CustomerWeb.Models.Enumerable;
using System.Linq;
using System;
using System.Web;

namespace CustomerWeb.Services.Common
{
    public class RestAPIService : IRestAPIService
    {
        private readonly CustomerAPIOptions _customerAPIOptions;
        private readonly IHttpContextAccessor _session;

        public RestAPIService(CustomerAPIOptions customerAPIOptions,
                              IHttpContextAccessor session)
        {
            _customerAPIOptions = customerAPIOptions;
            _session = session;
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

        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public HttpResponseMessage Request(RequestAPI requestAPI)
        {
            string url = $"{_customerAPIOptions.EndPointUrl}/{requestAPI.Route}";

            var httpClient = RequestHeader();

            if (requestAPI.MethodType == RequestMethodType.Get)
            {
                if (requestAPI.Body != null)
                {
                    var queryParams = GetQueryString(requestAPI.Body);
                    if (!String.IsNullOrEmpty(queryParams))
                        url = $"{url}?{queryParams}";
                }

                return httpClient.GetAsync(url).Result;
            }
            else
            {
                return httpClient.PostAsync(
                    url,
                    new StringContent(
                        JsonConvert.SerializeObject(requestAPI.Body),
                        Encoding.UTF8,
                        requestAPI.ContentType)).Result;
            }
        }

        public BaseResult<T> Request<T>(RequestAPI requestAPI) where T : class
        {
            try
            {
                string url = $"{_customerAPIOptions.EndPointUrl}/{requestAPI.Route}";

                HttpResponseMessage response;

                var httpClient = RequestHeader();

                if (requestAPI.MethodType == RequestMethodType.Get)
                {
                    if (requestAPI.Body != null)
                    {
                        var queryParams = GetQueryString(requestAPI.Body);

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

                return BaseResult<T>.NotOK("An error occurred while communicating with the server. Please try again.");
            }
            catch (Exception e)
            {
                return BaseResult<T>.NotOK(e.Message);
            }
        }

        public HttpResponseMessage RequestUserSession(RequestAPI requestAPI)
        {
            var response = Request(requestAPI);

            string content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<AuthorizationViewModel>>(content);

                if (responseAPI.Success && responseAPI.Data != null)
                {
                    _session.HttpContext.Session.SetUserSession(responseAPI.Data);
                }
            }

            return response;

        }

    }
}
