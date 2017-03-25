using MyWebSite.App_Start;
using System.Web.Mvc;

namespace MyWebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //public string say()
        //{
        //    return "hello world";
        //}
        public ViewResult list()
        {
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            DB db = new DB();
            if (db.sqlEx(string.Format("select * from userinfo where username='{0}' and password='{1}'", username, password)) > 0)
            {
                return Redirect("../Navigate/Index");
            }
            else
            {
                return Login();
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }
        
    }
}