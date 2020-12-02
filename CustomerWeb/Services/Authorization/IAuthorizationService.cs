using CustomerWeb.Models.Common;
using CustomerWeb.Models.Authorization;

namespace CustomerWeb.Services.Authorization
{
    public interface IAuthorizationService
    {
        BaseResult<AuthorizationResponse> RequestUserSession(AuthorizationRequest login);
    }
}
