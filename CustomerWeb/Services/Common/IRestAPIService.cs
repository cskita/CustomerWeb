using System.Net.Http;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Common
{
    public interface IRestAPIService
    {
        BaseResult<T> Request<T>(RequestAPI requestAPI) where T : class;
    }
}
