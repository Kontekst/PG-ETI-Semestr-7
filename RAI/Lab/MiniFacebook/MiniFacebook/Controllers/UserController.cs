using System.Collections.Generic;
using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    [RoutePrefix("Friends")]
    public class UserController : Controller
    {
        public static Dictionary<string, string> UsersFriendsDictionary = new Dictionary<string, string>();

        [HttpGet]
        [Route("List")]
        public ActionResult Index()
        {
            //TODO
            return View();
        }

        [HttpGet, HttpPost] //TODO TEST
        [Route("Add")]
        public ActionResult AddFriend(string login)
        {
            //TODO
            return View();
        }

        [HttpGet, HttpPost] //TODO TEST
        [Route("Del")]
        public ActionResult RemoveFriend(string login)
        {
            //TODO
            return View();
        }

        [HttpGet]
        [Route("Export")]
        public ActionResult ExportFriendsListToFile()
        {
            //TODO
            return View();
        }

        [HttpGet]
        [Route("Import")]
        public ActionResult ImportFriendsListFromFile()
        {
            //TODO
            return View();
        }

    }
}