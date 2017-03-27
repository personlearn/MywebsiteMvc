using Microsoft.AspNet.Identity;
using MyWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        public AccountController()
        {
            //no code
        }

        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// IAuthenticationManager
        /// </summary>
        public Microsoft.Owin.Security.IAuthenticationManager AutherticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <returns></returns>
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        /// <summary>
        ///注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {

            //user
            TiKuUser user = new TiKuUser { Id = Guid.NewGuid(), UserName = "aaa", Password = "XXXXXX" };

            //Context
            Microsoft.Owin.IOwinContext OwinContext = HttpContext.GetOwinContext();

            //用户储存
            Models.TiKuUserStore userStore = new Models.TiKuUserStore();

            //UserManager
            TiKuUserManager UserManager = new TiKuUserManager(userStore);

            IdentityResult result = await UserManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                Response.Write("注册成功！");
            }
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            //验证是否登录
            if (AutherticationManager.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login1(TiKuUser @user)
        {

            if (string.IsNullOrEmpty(@user.UserName)) { return View(); }
            if (string.IsNullOrEmpty(@user.Password)) { return View(); }

            //Context
            Microsoft.Owin.IOwinContext OwinContext = HttpContext.GetOwinContext();

            //实例化UserStore对象
            Models.TiKuUserStore userStore = new Models.TiKuUserStore();

            //UserManager
            Microsoft.AspNet.Identity.UserManager<Models.TiKuUser, Guid> UserManager = new Microsoft.AspNet.Identity.UserManager<Models.TiKuUser, Guid>(userStore);

            //signInManager
            Microsoft.AspNet.Identity.Owin.SignInManager<TiKuUser, Guid> signInManager = new Microsoft.AspNet.Identity.Owin.SignInManager<TiKuUser, Guid>(UserManager, AutherticationManager);


            //登录
            Microsoft.AspNet.Identity.Owin.SignInStatus SignInStatus = await signInManager.PasswordSignInAsync(@user.UserName,
                                                                                                               @user.Password,
                                                                                                               true,
                                                                                                               shouldLockout: false);

            //状态
            switch (SignInStatus)
            {
                //成功
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Success:

                    //标示
                    //System.Security.Claims.ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    //授权登陆
                    //AutherticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = true }, identity);
                    return RedirectToAction("index", "home");
                //锁定
                case Microsoft.AspNet.Identity.Owin.SignInStatus.LockedOut:
                    Response.Write("LockedOut!");
                    break;
                //要求验证
                case Microsoft.AspNet.Identity.Owin.SignInStatus.RequiresVerification:
                    Response.Write("RequiresVerification!");
                    break;
                //登录失败
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Failure:
                    Response.Write("Failure!");
                    break;

            }

            return View(@user);
        }



        /// <summary>
        /// 测试创建登录标示
        /// </summary>
        /// <returns></returns>
        [Obsolete("Debug模式下，测试创建登录标示")]
        private async Task<System.Security.Claims.ClaimsIdentity> CreateIdentity()
        {
            System.Security.Claims.ClaimsIdentity identity = null;

            //用户
            TiKuUser user = new TiKuUser();
            user.Id = Guid.NewGuid();
            user.UserName = "chaoqiangli";

            //Context
            Microsoft.Owin.IOwinContext OwinContext = HttpContext.GetOwinContext();
            Microsoft.AspNet.Identity.UserManager<Models.TiKuUser, Guid> UserManager = Microsoft.AspNet.Identity.Owin.OwinContextExtensions.GetUserManager<Microsoft.AspNet.Identity.UserManager<Models.TiKuUser, Guid>>(OwinContext);
            identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return identity;

        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(TiKuUser @user)
        {

            if (string.IsNullOrEmpty(@user.UserName)) { return View(); }
            if (string.IsNullOrEmpty(@user.Password)) { return View(); }

            //Context
            Microsoft.Owin.IOwinContext OwinContext = HttpContext.GetOwinContext();

            //实例化UserStore对象
            Models.TiKuUserStore userStore = new Models.TiKuUserStore();

            //UserManager
            TiKuUserManager UserManager = new TiKuUserManager(userStore);

            //signInManager
            TiKuSignInManager signInManager = new TiKuSignInManager(UserManager, AutherticationManager);


            //登录
            Microsoft.AspNet.Identity.Owin.SignInStatus SignInStatus = await signInManager.PasswordSignInAsync(@user.UserName,
                                                                                                               @user.Password,
                                                                                                               true,
                                                                                                               shouldLockout: false);

            //状态
            switch (SignInStatus)
            {
                //成功
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Success:

                    //标示
                    //System.Security.Claims.ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    //授权登陆
                    //AutherticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = true }, identity);
                    return RedirectToAction("index", "home");
                //锁定
                case Microsoft.AspNet.Identity.Owin.SignInStatus.LockedOut:
                    Response.Write("LockedOut!");
                    break;
                //要求验证
                case Microsoft.AspNet.Identity.Owin.SignInStatus.RequiresVerification:
                    Response.Write("RequiresVerification!");
                    break;
                //登录失败
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Failure:
                    Response.Write("Failure!");
                    break;

            }

            return View(@user);
        }
    }
}