using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Class
{
    public class ProjectsClass
    {
        public IEnumerable<ProjectsModel> ProjectsObject { get; set; }
        public IEnumerable<ProjectsExpandModel> ProjectExpandObject { get; set; }
        public IEnumerable<ProjectsExpandModelV2> ProjectExpandObjectv2 { get; set; }
    }

    public class ProjectsModel
    {
        public dynamic ProjectSite { get; set; }
    }

    public class ProjectsExpandModel
    {
        public dynamic ProjectID { get; set; }
        public dynamic ProjectName { get; set; }
    }

    public class ProjectsExpandModelV2
    {
        public dynamic ProjectID { get; set; }
        public dynamic ProjectName { get; set; }
        public dynamic ProjectAccomplishment { get; set; }
    }

}