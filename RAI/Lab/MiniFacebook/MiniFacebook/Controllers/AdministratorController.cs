using System.Collections.Generic;
using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    public class AdministratorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //TODO
            return View();
        }

        [HttpGet]
        public ActionResult Init()
        {
            AuthorizationController.Users.AddRange(new List<string>() { "Tomek", "Dawid", "Ewelina", "Martyna", "Witold", "Krystian" }); //TODO is there possibility to shorten 

            var usersFriendsDictionary = new Dictionary<string, string>()
            {
                {"Tomek","Dawid"},
                {"Tomek","Martyna"},
                {"Tomek","Ewelina"},
                {"Dawid","Witold"},
                {"Ewelina","Martyna"},
                {"Martyna","Witold"},
                {"Witold","Krystian"{}
            };
            foreach (var friends in usersFriendsDictionary)
                UserController.UsersFriendsDictionary.Add(friends.Key, friends.Value);

            return View();
        }

        [HttpGet, HttpPost] //TODO
        public ActionResult AddUser()
        {
            //TODO
            return View();
        }

        [HttpGet, HttpPost] //TODO
        public ActionResult DeleteUser()
        {
            //TODO
            return View();
        }
    }
}