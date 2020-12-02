namespace CustomerWeb.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
