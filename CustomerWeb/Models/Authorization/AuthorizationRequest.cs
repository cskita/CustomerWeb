using System.Security.Cryptography;
using System.Text;

namespace CustomerWeb.Models.Authorization
{
    public class AuthorizationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public static string GetPasswordHash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes("custumer_V_"+input));

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
