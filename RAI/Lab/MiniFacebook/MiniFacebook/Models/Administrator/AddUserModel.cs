namespace MiniFacebook.Models.Administrator
{
    public class AddUserModel
    {
        public string UserLogin;

        public AddUserModel(string userLogin)
        {
            UserLogin = userLogin;
        }
    }
}