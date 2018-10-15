using System.Collections.Generic;
using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    public class AuthorizationController : Controller
    {
        public static List<string> Users = new List<string>() { "admin" };

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserLogin"] != null)
            {
                if (Session["UserLogin"].ToString() == "admin")
                {
                    return RedirectToAction("Index", "Administrator");
                }
                else if (Session["UserLogin"].ToString() != "")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            return RedirectToAction("LoginPage", "Authorization");
        }

        [HttpGet]
        public ActionResult LoginPage()
        {
            //TODO
            return View();
        }

        [HttpGet, HttpPost] // TODO
        [Route("Login/{login}")]
        public ActionResult Login(string login)
        {
            if (Users.Contains(login))
            {
                Session["UserLogin"] = login;
                return RedirectToAction("Index", "User");
            }
            else
            {
                //  TODO
                return View();
            }
        }

        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            //TODO
            return View();
        }
    }
}