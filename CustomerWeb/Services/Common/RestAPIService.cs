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

        public HttpResponseMessage Request(RequestAPI requestAPI)
        {
            string url = $"{_customerAPIOptions.EndPointUrl}/{requestAPI.Route}";

            var httpClient = RequestHeader();

            if (requestAPI.MethodType == RequestMethodType.Get)
            {
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
