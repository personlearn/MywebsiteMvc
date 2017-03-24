using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebSite.Models
{
    public class AnimaMagInfo
    {
        [Display(Name = "id")]
        public int id { set; get; }
        [Display(Name = "磁链")]
        public string magnet { set; get; }
        [Display(Name = "标题")]
        public string title { set; get; }
        [Display(Name = "标签")]
        public string tag { set; get; }
        [Display(Name = "类型")]
        public string atype { set; get; }
        [Display(Name = "年份")]
        public DateTime? years { set; get; }
        [Display(Name = "添加时间")]
        public DateTime? addtime { set; get; }
    }
}