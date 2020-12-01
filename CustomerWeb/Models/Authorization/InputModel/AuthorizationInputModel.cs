using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CustomerWeb.Models.Authorization.InputModel
{
    public class AuthorizationInputModel
    {
        [StringLength(50)]
        [Required]
        [EmailAddress]
        [Description("E-mail")]
        public string Email { get; set; }

        [StringLength(40)]
        [Required]
        [DataType(DataType.Password)]
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
