namespace WebApplication1.Models
{
    public class UserModel :BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserModel(string userName, string pass, string email)
        {
            UserName = userName;
            Password = pass;
            Email = email;
        }
    }
}