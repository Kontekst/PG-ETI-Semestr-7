using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    public class AuthorizationController : Controller
    {
        [HttpGet]
        public ActionResult CheckAuthorization()
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
        [Route("LoginPage")]
        public ActionResult LoginPage()
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
            return View();
        }

        [Route("Login")]
        [Route("Login/{login}")]
        public ActionResult Login(string login)
        {
            if (UserController.ContainsUser(login))
            {
                Session["UserLogin"] = login;
                if (login == "admin")
                    return RedirectToAction("Index", "Administrator");

                return RedirectToAction("List", "User");
            }
            else
            {
                ViewBag.LoginError = true;
                return View("LoginPage");
            }
        }

        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            ViewBag.LogoutSuccess = true;
            return View("LoginPage");
        }
    }
}