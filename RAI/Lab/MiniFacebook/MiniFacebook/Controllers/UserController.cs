using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MiniFacebook.Models.User;

namespace MiniFacebook.Controllers
{
    public class UserController : Controller
    {
        public static List<UserProfile> Users = new List<UserProfile>() { new UserProfile("admin", DateTime.Now, new List<string>()) };

        [HttpGet]
        [Route("List")]
        public ActionResult List()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            string userLogin = Session["UserLogin"].ToString();
            var userPageModel = new UserPageModel(userLogin, GetUserFriends(userLogin));

            if (TempData["UserRemovedSuccessfully"] != null)
            {
                ViewBag.UserRemovedSuccessfully = true;
            }
            if (TempData["UserCanNotBeRemoved"] != null)
            {
                ViewBag.UserCanNotBeRemoved = true;
            }
            if (TempData["FriendAddedSuccessfully"] != null)
            {
                ViewBag.FriendAddedSuccessfully = true;
            }
            if (TempData["FriendAddingError"] != null)
            {
                ViewBag.FriendAddingError = true;
            }

            return View(userPageModel);
        }

        [Route("Friends/Add")]
        [Route("Friends/Add/{login}")]
        public ActionResult AddFriend(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            if (IsFriendAddedSucessfully(Session["UserLogin"].ToString(), login))
            {
                TempData["FriendAddedSuccessfully"] = true;
            }
            else
            {
                TempData["FriendAddingError"] = true;
            }
            return RedirectToAction("List");
        }

        [Route("Friends/Del")]
        [Route("Friends/Del/{login}")]
        public ActionResult RemoveFriend(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            if (IsUserFriendRemovedSuccesfully(Session["UserLogin"].ToString(), login))
            {
                TempData["UserRemovedSuccessfully"] = true;
            }
            else
            {
                TempData["UserCanNotBeRemoved"] = true;
            }
            return RedirectToAction("List");
        }

        [Route("Friends/Export")]
        public ActionResult ExportFriendsListToFile()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() == "admin"))
            {
                return HttpNotFound();
            }

            //TODO
            return View();
        }

        [Route("Friends/Import")]
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

        public static bool IsUserFriendRemovedSuccesfully(string userLogin, string friendLogin)
        {
            if (Users.Where(user => user.Login == userLogin).Select(user => user.Login).FirstOrDefault() == friendLogin)
            {
                return false;
            }
            Users.Where(user => user.Login == userLogin).FirstOrDefault().UserFriends.Remove(friendLogin);
            Users.Where(user => user.Login == friendLogin).FirstOrDefault().UserFriends.Remove(userLogin);
            return true;
        }

        public static bool IsFriendAddedSucessfully(string userLogin, string friendLogin)
        {
            bool friendExist = false;
            foreach (var userProfile in Users)
            {
                if (userProfile.Login == friendLogin)
                {
                    friendExist = true;
                }
            }

            if (!friendExist)
                return false;

            if (!Users.Where(user => user.Login == userLogin).Select(user => user.UserFriends).FirstOrDefault().Contains(friendLogin))
            {
                Users.Where(user => user.Login == userLogin).FirstOrDefault().UserFriends.Add(friendLogin);
            }
            if (!Users.Where(user => user.Login == friendLogin).Select(user => user.UserFriends).FirstOrDefault().Contains(userLogin))
            {
                Users.Where(user => user.Login == friendLogin).FirstOrDefault().UserFriends.Add(userLogin);
            }
            return true;
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