using System.Net.Http;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Common
{
    public interface IRestAPIService
    {
        HttpResponseMessage Request(RequestAPI requestAPI);
        BaseResult<T> Request<T>(RequestAPI requestAPI) where T : class;
        HttpResponseMessage RequestUserSession(RequestAPI requestAPI);
    }
}
