using System.Collections.Generic;
using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    public class AuthorizationController : Controller
    {
        public static List<string> Users = null;

        public ActionResult LoginPage()
        {
            //TODO
            return View();
        }

        public ActionResult Login(string login)
        {
            if (Users.Contains(login))
            {
                Session["UserLogin"] = login;
            }
            //  TODO
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            //TODO
            return View();
        }
    }
}