using CustomerWeb.Models.User.ViewModel;

namespace CustomerWeb.Models.Authorization.ViewModel
{
    public class AuthorizationViewModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserViewModel User { get; set; }
    }
}
