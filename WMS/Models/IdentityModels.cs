using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class LoginViewModel
    {

        // public string EmployeeId { get; set; }
        public dynamic EmployeeId { get; set; }
        //public dynamic Password { get; set; }
        //public dynamic EmployeeName { get; set; }
        //public dynamic RoleGroup { get; set; }

        //public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string EmployeeName { get; set; }
        public string RoleGroup { get; set; }

        public bool BoolChangePassword { get; set; }
    }

    public class LoginAutoCompleteModel
    {
        //public dynamic value { get; set; }
        //public dynamic title { get; set; }
        //public dynamic text { get; set; }
        public string value { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    }
}