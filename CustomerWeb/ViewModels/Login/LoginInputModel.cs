using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerWeb.ViewModels.Login
{
    public class LoginInputModel
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
    }
}
