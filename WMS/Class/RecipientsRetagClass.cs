using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Class
{
    public class RecipientsRetagModel
    {
        public dynamic EID { get; set; }
        public dynamic EName { get; set; }
    }

    public class RecipientsRetagClass
    {
        public IEnumerable<RecipientsRetagModel> RecipientsRetagObject { get; set; }

    }
}