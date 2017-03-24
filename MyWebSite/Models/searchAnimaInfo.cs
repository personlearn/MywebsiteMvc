using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace MyWebSite.Models
{
    public class searchAnimaInfo
    {
        //信息列表
        public PagedList<Models.AnimaMagInfo> Infos { get; set; }
    }
}