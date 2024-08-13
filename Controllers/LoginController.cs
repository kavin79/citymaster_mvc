using citymaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace citymaster.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginContext db;

        public LoginController()
        {
            db = new LoginContext();
        }

        public LoginController(LoginContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                // Session is active, no need to use cookies
                return RedirectToAction("Index", "Home");
            }

            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null && cookie["remember_me"] == "true")
            {
                ViewBag.username = cookie["username"];
                ViewBag.password = cookie["password"];
                ViewBag.remember_me = true;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(users u, bool remember_me = false)
        {
            if(ModelState.IsValid == true)
            {
               HttpCookie cookie = new HttpCookie("User");

               if(remember_me)
                {
                    cookie["username"] = u.username;
                    cookie["password"] = u.password;
                    cookie["remember_me"] = "true";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.Cookies.Add(cookie);
                }

                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(cookie);
                }

                var credentials = db.user.Where(m => m.username == u.username &&  m.password == u.password).FirstOrDefault();
                if(credentials == null)
                {
                    ViewBag.ErrorMessage = "Login Failed";
                    return View();
                }

                else
                {
                    Session["username"] = u.username;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        { 
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}
