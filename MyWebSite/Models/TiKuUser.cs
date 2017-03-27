using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MyWebSite.Models
{
    public class TiKuUser : Microsoft.AspNet.Identity.IUser<Guid>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("登录名")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        public string Nick { get; set; }


    }
}