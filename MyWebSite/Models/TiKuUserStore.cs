using Microsoft.AspNet.Identity;
using MyWebSite.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyWebSite.Models
{
    //IUserLockoutStore<User, TKey>: 在尝试一定的失败次数后允许锁定一个账号
    //IUserEmailStore<User, TKey>: 使用邮件地址做确认 (例如通过邮件进行确认)
    //IUserPhoneNumberStore<User, TKey>: 使用手机号码做确认(例如通过短信进行确认)
    //IUserTwoFactorStore<User, TKey>: 启用2中途径进行安全验证(例如通过用户名/密码和通过邮件或者短信的令牌)，当用户密码可能存在不安全隐患的时候，系统会以短信或邮件的方式向用户发送安全码
    public class TiKuUserStore : Microsoft.AspNet.Identity.IUserStore<TiKuUser, Guid>,
                                 IUserPasswordStore<TiKuUser, Guid>,
                                 IUserClaimStore<TiKuUser, Guid>,
                                 IUserLockoutStore<TiKuUser, Guid>,
                                 IUserEmailStore<TiKuUser, Guid>,
                                 IUserPhoneNumberStore<TiKuUser, Guid>,
                                 IUserTwoFactorStore<TiKuUser, Guid>
    {

        /// <summary>
        /// 声明
        /// </summary>
        public IList<System.Security.Claims.Claim> Claims = null;

        /// <summary>
        /// 实例化
        /// </summary>
        public TiKuUserStore()
        {
            //声明
            Claims = new List<System.Security.Claims.Claim>();
        }

        /// <summary>
        /// 用户
        /// </summary>
        public TiKuUser UserIdentity = null;


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task CreateAsync(TiKuUser user)
        {
            return Task.Run(() =>
            {
                string strInsertCmd = @"INSERT INTO[tb_User](ID,UserName,UserPwd) VALUES(@UserID,@UserName,@UserPwd);";
                SqlParameter[] parameters = {
                                               new SqlParameter("@UserName",SqlDbType.NVarChar,30),
                                               new SqlParameter("@UserPwd",SqlDbType.NVarChar,100),
                                               new SqlParameter("@UserID",SqlDbType.UniqueIdentifier)
                                              };
                parameters[0].Value = user.UserName;
                parameters[1].Value = user.Password;
                parameters[2].Value = user.Id;

                int iResult = DB.ExecuteNonQuery(strInsertCmd, parameters);
            });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task DeleteAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 2>通过用户ID，获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task<TiKuUser> FindByIdAsync(Guid userId)
        {
            return Task<TiKuUser>.Run<TiKuUser>(() =>
            {
                if (UserIdentity != null) { return UserIdentity; }

                string strCmd = "SELECT * FROM [tb_User] WHERE ID=@UserID;";
                SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) };
                parameters[0].Value = userId;
                List<TiKuUser> list = new List<TiKuUser>();
                using (IDataReader data = DB.ExecuteReader(strCmd, parameters))
                {
                    while (data.Read())
                    {
                        //model
                        TiKuUser user = new TiKuUser();
                        user.Id = Guid.Parse(data["ID"].ToString());
                        user.UserName = data["UserName"].ToString();
                        user.Password = data["UserPwd"].ToString();

                        list.Add(user);
                    }
                }
                UserIdentity = list.FirstOrDefault();
                return UserIdentity;
            });
        }

        /// <summary>
        /// 1>通过用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task<TiKuUser> FindByNameAsync(string userName)
        {
            return Task<TiKuUser>.Run<TiKuUser>(() =>
            {
                if (UserIdentity != null) { return UserIdentity; }

                string strCmd = "SELECT * FROM [tb_User] WHERE UserName=@UserName;";
                SqlParameter[] parameters = { new SqlParameter("@UserName", SqlDbType.NVarChar, 30) };
                parameters[0].Value = userName;
                List<TiKuUser> list = new List<TiKuUser>();
                using (IDataReader data = DB.ExecuteReader(strCmd, parameters))
                {

                    while (data.Read())
                    {
                        //model
                        TiKuUser user = new TiKuUser();
                        user.Id = Guid.Parse(data["ID"].ToString());
                        user.UserName = data["UserName"].ToString();
                        user.Password = data["UserPwd"].ToString();
                        list.Add(user);
                    }
                }

                //模拟数据库
                UserIdentity = list.FirstOrDefault();

                return UserIdentity;
            });
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task UpdateAsync(TiKuUser user)
        {
            return Task.Run(() =>
            {

            });
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TiKuUser user)
        {
            return Task<string>.Run(() =>
            {
                return user.Password;
            });
        }

        /// <summary>
        /// 是否有密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TiKuUser user)
        {
            return Task.FromResult<bool>(!string.IsNullOrEmpty(user.Password));
        }

        /// <summary>
        /// 密码进行加密
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TiKuUser user, string passwordHash)
        {
            return Task.Run(() =>
            {
                user.Password = passwordHash;//加密后
            });
        }


        /// <summary>
        /// 添加一个声明
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public Task AddClaimAsync(TiKuUser user, System.Security.Claims.Claim claim)
        {
            return Task.Run(() => { Claims.Add(claim); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TiKuUser user)
        {
            return Task.Run<IList<System.Security.Claims.Claim>>(() =>
            {
                IList<System.Security.Claims.Claim> list = new List<System.Security.Claims.Claim>();

                //声明
                //System.Security.Claims.Claim claimUserName = new System.Security.Claims.Claim("nick", user.UserName);//UserName
                //System.Security.Claims.Claim claimUserId = new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString());//UserId
                //list.Add(claimUserName);
                //list.Add(claimUserId);

                return list;
            });
        }

        /// <summary>
        /// 移除声明
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TiKuUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取访问失败次数
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(TiKuUser user)
        {
            return Task<Int32>.FromResult<Int32>(1);
        }

        /// <summary>
        /// 获取锁定状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TiKuUser user)
        {
            return Task<bool>.Run<bool>(() =>
            {
                return false;
            });
        }

        /// <summary>
        /// 获取锁定结束时间
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重置访问时间计数
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(TiKuUser user)
        {
            return Task.FromResult(false);
        }

        #region  LockOut
        /// <summary>
        /// 修改锁定状态
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TiKuUser user, bool enabled)
        {
            return Task.Run(() =>
            {

            });
        }

        /// <summary>
        /// 设置锁定时间
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public Task SetLockoutEndDateAsync(TiKuUser user, DateTimeOffset lockoutEnd)
        {
            return Task.Run(() =>
            {

            });
        }
        #endregion

        #region Email

        /// <summary>
        /// 通过邮箱获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<TiKuUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户邮箱
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 确认邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task SetEmailAsync(TiKuUser user, string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(TiKuUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Phone

        /// <summary>
        /// 获取手机号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TiKuUser user)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task SetPhoneNumberAsync(TiKuUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetPhoneNumberConfirmedAsync(TiKuUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TiKuUser user)
        {
            return Task.Run<bool>(() =>
            {
                return false;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetTwoFactorEnabledAsync(TiKuUser user, bool enabled)
        {
            return Task.Run(() =>
            {

            });
        }
    }
}