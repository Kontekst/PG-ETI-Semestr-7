using MiniFacebook.Controllers;
using System.Collections.Generic;

namespace MiniFacebook.Models.Administrator
{
    public class IndexModel
    {

        public List<UserProfile> Users;

        public bool IsDatabaseInitialized = true;
        public IndexModel(List<UserProfile> users)
        {
            Users = users;
        }

    }
}