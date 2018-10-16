using MiniFacebook.Controllers;
using System.Collections.Generic;

namespace MiniFacebook.Models.User
{
    public class UserPageModel
    {
        public string UserLogin;
        public List<UserProfile> UserFriends;

        public UserPageModel(string userLogin, List<UserProfile> userFriends)
        {
            UserLogin = userLogin;
            UserFriends = userFriends;
        }
    }
}