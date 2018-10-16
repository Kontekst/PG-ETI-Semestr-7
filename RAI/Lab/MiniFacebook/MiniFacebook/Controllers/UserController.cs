using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MiniFacebook.Models.User;

namespace MiniFacebook.Controllers
{
    [RoutePrefix("Friends")]
    public class UserController : Controller
    {
        public static List<UserProfile> Users = new List<UserProfile>() { new UserProfile("admin", DateTime.Now, new List<string>()) };

        [HttpGet]
        [Route("UserPage")]
        public ActionResult UserPage()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            string userLogin = Session["UserLogin"].ToString();
            var userPageModel = new UserPageModel(userLogin, GetUserFriends(userLogin));

            return View(userPageModel);
        }

        [HttpGet, HttpPost] //TODO TEST
        [Route("Add")]
        public ActionResult AddFriend(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            //TODO
            return View();
        }

        [HttpGet, HttpPost] //TODO TEST
        [Route("Del")]
        public ActionResult RemoveFriend(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            //TODO
            return View();
        }

        [HttpGet]
        [Route("Export")]
        public ActionResult ExportFriendsListToFile()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            //TODO
            return View();
        }

        [HttpGet]
        [Route("Import")]
        public ActionResult ImportFriendsListFromFile()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            //TODO
            return View();
        }

        public static bool ContainsUser(string login)
        {
            foreach (var user in Users)
            {
                if (user.Login == login)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> GetUsersLoginsList()
        {
            var usersLoginsList = new List<string>();

            foreach (var user in Users)
            {
                usersLoginsList.Add(user.Login);
            }
            return usersLoginsList;
        }

        public static List<UserProfile> GetUserFriends(string login)
        {
            List<UserProfile> userFriendListProfiles = new List<UserProfile>();

            foreach (var userProfileLogin in Users.Where(user => user.Login == login).Select(user => user.UserFriends).FirstOrDefault())
            {
                var userProfile = Users.Where(user => user.Login == userProfileLogin).FirstOrDefault();
                userFriendListProfiles.Add(userProfile);
            }
            return userFriendListProfiles;
        }
    }

    public class UserProfile
    {
        public string Login;
        public DateTime AccountCreationDate;
        public List<string> UserFriends;

        public UserProfile(string login, DateTime accountCreationDate, List<string> userFriends)
        {
            Login = login;
            AccountCreationDate = accountCreationDate;
            UserFriends = userFriends;
        }

        public UserProfile(string login, List<string> userFriends)
        {
            Login = login;
            AccountCreationDate = DateTime.Now;
            UserFriends = userFriends;
        }
    }

}