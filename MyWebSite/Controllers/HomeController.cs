using MyWebSite.App_Start;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyWebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Home/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            DB db = new DB();
            if (db.reDt(string.Format("select id from userinfo where username='{0}' and password='{1}'", username, password)).Rows.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                //           1,
                //           username,
                //           DateTime.Now,
                //           DateTime.Now.AddMinutes(30),
                //           false,
                //           "admins,vip",
                //           "/"
                //           );
                //string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                //System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                //System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                return Redirect("../Navigate/Index");
            }
            else
            {
                return Login();
            }
        }

        //
        // GET: /Home/Login
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

    }
}