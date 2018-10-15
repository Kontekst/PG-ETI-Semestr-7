using System;
using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    public class UserController : Controller
    {
        public static Tuple<string, string> UsersFriendsDictionary;

        public ActionResult Index()
        {
            //TODO
            return View();
        }

        public ActionResult AddFriend(string login)
        {
            //TODO
            return View();
        }

        public ActionResult RemoveFriend(string login)
        {
            //TODO
            return View();
        }

        public ActionResult ExportFriendsListToFile()
        {
            //TODO
            return View();
        }

        public ActionResult ImportFriendsListFromFile()
        {
            //TODO
            return View();
        }

    }
}