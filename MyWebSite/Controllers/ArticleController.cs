using MyWebSite.App_Start;
using MyWebSite.BLL;
using MyWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MyWebSite.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string TYArticleGet(string url)
        {
            
            HttpHeader header = new HttpHeader();
            header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.contentType = "application/x-www-form-urlencoded";
            //header.method = "POST";
            header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.maxTry = 300;

            int pagecount = 0;
            string title = RegexpHelper.TYRegexTitle(HTMLHelper.GetHtml(url, header), ref pagecount);
            List<string> urllist = RegexpHelper.TYRegexUrl(url, pagecount);
            string ex = "";
            foreach (string urlx in urllist)
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append(RegexpHelper.TYRegexArticle(HTMLHelper.GetHtml(urlx, header)) + "\n");
                MatchCollection articles = RegexpHelper.TYRegexArticle(HTMLHelper.GetHtml(urlx, header));
                foreach (Match item in articles)
                {
                    sb.Append(item.Groups["author"].Value.Trim().WipeOffHTMLSign() + "\n");
                    sb.Append(item.Groups["article"].Value.Trim().WipeOffHTMLSign() + "\n");
                }
                if (ArticletoXml.toXml(articles, "source//tianya", title + ".xml", "TYArticle", "Article", ref ex))
                {
                    sb.Append("XML SUCCUESSSSSSSSS\r\n");
                }
                else
                {
                    sb.Append(ex + "  \n\n");
                }

                if (ArticletoXml.toTxt(sb.ToString(), "source//tianya", title + ".txt"))
                {
                    sb.Append("TXT SUCCUESSSSSSSSS\r\n");
                }
            }
            return "获取成功！";
        }


        public ViewResult TYArticleGet()
        {
            return View();
        }

    }
}