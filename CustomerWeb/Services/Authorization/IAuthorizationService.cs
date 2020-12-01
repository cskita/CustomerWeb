using CustomerWeb.Models.Authorization.InputModel;
using CustomerWeb.Models.Authorization.ViewModel;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Authorization
{
    public interface IAuthorizationService
    {
        BaseResult<AuthorizationViewModel> RequestToken(AuthorizationInputModel login);
    }
}
