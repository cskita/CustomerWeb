using UserModel = CustomerWeb.Models.User.User;

namespace CustomerWeb.Models.Authorization
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public UserModel User { get; set; }
    }
}
