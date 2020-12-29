using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Class
{
    public class OnlineUsersClass
    {
        private SqlDbConnection database = new SqlDbConnection();
        public IEnumerable<OnlineUsersDepartmentModel> UsersOnlineDepartmentObject { get; set; }
        public IEnumerable<OnlineUsersNameModel> UsersOnlineNameObject { get; set; }
    }

    public class OnlineUsersDepartmentModel
    {
        public dynamic DepartmentName { get; set; }
        public dynamic DepartmentID { get; set; }
    }

    public class OnlineUsersNameModel
    {
        public dynamic Name { get; set; }
        public dynamic Avatar { get; set; }
    }
}