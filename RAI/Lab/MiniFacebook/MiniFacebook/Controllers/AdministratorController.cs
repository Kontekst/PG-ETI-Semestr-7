using MiniFacebook.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MiniFacebook.Controllers
{
    public class AdministratorController : Controller
    {

        [HttpGet]
        [Route("Index")]
        public ActionResult Index()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() != "admin"))
            {
                return HttpNotFound();
            }
            ViewBag.UserLogin = Session["UserLogin"].ToString();

            var model = new IndexModel(UserController.Users);
            if (UserController.Users.Count < 2)
            {
                model.IsDatabaseInitialized = false;
            }

            if (TempData["InitializationUsersSuccess"] != null)
            {
                ViewBag.InitializationUsersSuccess = true;
            }
            if (TempData["AdminCannotBeDeleted"] != null)
            {
                ViewBag.AdminCannotBeDeleted = true;
            }
            if (TempData["DeleteUserSuccess"] != null)
            {
                ViewBag.DeleteUserSuccess = true;
            }
            if (TempData["DeleteUserNotFound"] != null)
            {
                ViewBag.DeleteUserNotFound = true;
            }
            if (TempData["InitializationAlreadyDone"] != null)
            {
                ViewBag.InitializationAlreadyDone = true;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Init")]
        public ActionResult Init()
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() != "admin"))
            {
                return HttpNotFound();
            }

            if (UserController.Users.Count < 2)
            {
                UserController.Users.AddRange(new List<UserProfile>() {
                    new UserProfile("Tomek",DateTime.Now,new List<string>(){"Dawid","Ewelina","Martyna"}),
                    new UserProfile("Dawid",DateTime.Now,new List<string>(){"Tomek","Ewelina"}),
                    new UserProfile("Ewelina",DateTime.Now,new List<string>(){"Tomek","Dawid"}),
                    new UserProfile("Martyna",DateTime.Now,new List<string>(){"Tomek","Witold"}),
                    new UserProfile("Witold",DateTime.Now,new List<string>(){"Martyna","Krystian"}),
                    new UserProfile("Krystian",DateTime.Now,new List<string>(){"Witold" })
                });
                TempData["InitializationUsersSuccess"] = true;
            }
            else
            {
                TempData["InitializationAlreadyDone"] = true;
            }
            return RedirectToAction("Index");
        }

        [Route("User/Add")]
        public ActionResult AddUser(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() != "admin"))
            {
                return HttpNotFound();
            }

            var addUserModel = new AddUserModel(login);
            return View(addUserModel);
        }

        [HttpPost]
        [Route("AddNewUser")]
        public ActionResult AddNewUser(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() != "admin"))
            {
                return HttpNotFound();
            }

            if (UserController.ContainsUser(login))
            {
                ViewBag.UserAlreadyExist = true;
                return View("AddUser");
            }
            UserController.Users.Add(new UserProfile(login, new List<string>()));

            return RedirectToAction("Index");
        }

        [Route("User/Del")]
        [Route("User/Del/{login}")]
        public ActionResult DeleteUser(string login)
        {
            if ((Session["UserLogin"] == null) || (Session["UserLogin"] != null && Session["UserLogin"].ToString() != "admin"))
            {
                return HttpNotFound();
            }

            if (login == "admin")
            {
                TempData["AdminCannotBeDeleted"] = true;
            }
            else if (UserController.ContainsUser(login))
            {
                UserController.Users.RemoveAll(a => a.Login == login);

                foreach (var user in UserController.Users)
                {
                    user.UserFriends.Remove(login);
                }
                TempData["DeleteUserSuccess"] = true;
            }
            else
            {
                TempData["DeleteUserNotFound"] = true;
            }
            return RedirectToAction("Index");
        }
    }
}