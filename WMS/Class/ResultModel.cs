using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Class
{
    public class ResultModel
    {
        public int result { get; set; }
        public dynamic msg { get; set; }
        public string i_catId { get; set; }
        public string i_subcatId { get; set; }
    }
}