using MyWebSite.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace MyWebSite.Controllers
{
    [Authorize]
    public class AnimaMagController : Controller
    {
        // GET: AnimaMag
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult pageSearchAnima(int? id = 1, string title = null, string year = null)
        {
            DB db = new DB();
            string sql = "select * from resource where 1=1 ";
            sql = string.IsNullOrEmpty(title) ? sql : string.Format(" and title like '%{0}%'", title);
            sql = string.IsNullOrEmpty(year) ? sql : sql + string.Format(" and DATEPART(year,years)={0}", year);
            DataTable dt = db.reDt(sql);
            string str = ViewBag.str = DB.DataTableToJsonWithJavaScriptSerializer(dt);

            //信息列表(此处使用分页控件提出数据)
            int totalCount = 0;
            int pageIndex = id ?? 1;
            int pageSize = 100;
            List<Models.AnimaMagInfo> list = DB.JsonToObj(str, typeof(List<Models.AnimaMagInfo>)) as List<Models.AnimaMagInfo>;
            PagedList<Models.AnimaMagInfo> InfoPager = list.AsQueryable().ToPagedList(pageIndex, pageSize);
            InfoPager.TotalItemCount = totalCount = dt.Rows.Count;
            InfoPager.CurrentPageIndex = (int)(id ?? 1);
            //数据组装到viewModel
            Models.searchAnimaInfo index = new Models.searchAnimaInfo();
            index.Infos = InfoPager;
            //------------------使用ViewBig变量传递数据---------------//
            //ViewBag.PagerData = InfoPager;
            return View(index);

            //return View();
        }

        public string AJAXdownload()
        {
            try
            {
                Type qqdown;//类型
                object qqdowobj;//一个对象
                object[] parameter = new object[6];//参数，因为我们调用的方法有6和参数。 所以定义了6个
                string url = Request.Form["magnet"].ToString();
                if (url != null && url.Length > 0)
                {
                    qqdown = Type.GetTypeFromProgID("QQMiniDL.RightClick.1");//从progid创建类型
                    qqdowobj = Activator.CreateInstance(qqdown);//然后创建实例
                    parameter[0] = url;//第一个参数是url
                    parameter[1] = url;//第二个参数是引用url,这里我们填成一样的
                    parameter[2] = "";//注释信息
                    parameter[3] = null;
                    parameter[4] = 0;
                    parameter[5] = null;
                    qqdown.InvokeMember("sendUrl2", BindingFlags.InvokeMethod, null, qqdowobj, parameter);//使用invokemember调用方法

                    //BindingFlags 中有很多枚举，分别是用于调用方法或者属性的设置 获取等绑定标记。 你可以参考一下msdn
                }

                //QQIEHELPERLib.QQRightClick qqxf = new QQIEHELPERLib.QQRightClick();
                //qqxf.AddTask(url, "", "qwe.lrc");
                //qqxf.CommitTasks2(1);
                //qqxf.SendMultiTask();
                return Request.Form["magnet"].ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}