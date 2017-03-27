using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebSite.Models
{
    public class TiKuSignInManager : Microsoft.AspNet.Identity.Owin.SignInManager<TiKuUser, Guid>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="UserManager"></param>
        /// <param name="AuthenticationManager"></param>
        public TiKuSignInManager(Microsoft.AspNet.Identity.UserManager<TiKuUser, Guid> UserManager, Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager)
            : base(UserManager, AuthenticationManager)
        {

        }

        /// <summary>
        /// 根据用户名密码，验证用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<Microsoft.AspNet.Identity.Owin.SignInStatus> PasswordSignInAsync(string userName,
                                                                                                                     string password,
                                                                                                                     bool isPersistent,
                                                                                                                     bool shouldLockout)
        {
            return base.PasswordSignInAsync(userName,
                                            password,
                                            isPersistent,
                                            shouldLockout);
        }
    }
}