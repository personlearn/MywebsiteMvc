using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebSite.Models
{
    public class HttpHeader
    {
        public string contentType { get; set; }

        public string accept { get; set; }

        public string userAgent { get; set; }

        public string method { get; set; }

        public int maxTry { get; set; }
    }
}