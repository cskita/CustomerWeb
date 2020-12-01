using System.Net.Http;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Common
{
    public interface IRestAPIService
    {
        HttpResponseMessage Request(RequestAPI requestAPI);
        HttpResponseMessage RequestUserSession(RequestAPI requestAPI);
    }
}
