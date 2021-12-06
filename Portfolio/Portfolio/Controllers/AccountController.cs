using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Microsoft.Owin.Security;

namespace Portfolio.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public AccountController()
        {
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext("identityConnection")));
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLogin login,  string returnUrl, bool isPersistent)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = userManager.Find(login.Username, login.Password);

                if (user != null)
                {
                    var auth = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");

                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = isPersistent
                    };

                    auth.SignOut();
                    auth.SignIn(authProperties, identity);

                    return Redirect(String.IsNullOrEmpty(returnUrl) ? "/Admin" : returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "İstifadəçi adı və ya şifrə yalnışdır");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut();
            return RedirectToAction("Login");
        }

    }
}