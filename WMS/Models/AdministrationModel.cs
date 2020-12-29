using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class AdministrationModel
    {
        public string NewAccounts { get; set; }
        public string DisabledAccounts { get; set; }
        public string AllAccounts { get; set; }
    }


    public class UserAccountsModel
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyEmail { get; set; }
        public string Company { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Avatar { get; set; }
    }
}