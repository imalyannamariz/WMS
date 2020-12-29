using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Class
{
    public class SqlDbConnection
    {
        public dynamic DbConnect(dynamic databaseName)
        {
            var connectionString = string.Empty;

            //TEST()
            //var dataSource = "AALCANTARA;"; // "APRESTOZA;" '"RSLIM\JVIMPERIAL;" ' "RSLIM\JVIMPERIAL;" '"IMPERIAL\JIMPERIAL2014;" '
            //var initialCatalog = databaseName + ";";
            //var initialCatalog = databaseName + ";";
            //var userId = "lester;";
            //var password = "M3g@w0rld123;";

            //PROD()
            //var dataSource = "AALCANTARA;";//"MW-CPDPORTAL01;" '"192.168.97.81;"
            //var dataSource = "(LOCAL)\\MSSQLSERVER;";//"MW-CPDPORTAL01;" '"192.168.97.81;"
            var dataSource = "MW-CPDPORTAL01;";

            var initialCatalog = databaseName + ";";
            var userId = "sa;";
            var password = "26p0rt@Lcpd;";

            //TEST
            //connectionString = "Data Source=" + dataSource + "Initial Catalog=" + initialCatalog + "Integrated Security=True;Trusted_Connection=True;";

            connectionString = "Data Source=" + dataSource + "Initial Catalog=" + initialCatalog + "User Id=" + userId + "Password=" + password;

            return connectionString;
        }


        public dynamic philDbConnect(dynamic databaseName)
        {
            var connectionString = string.Empty;

            //'TEST()
            //var dataSource = "AALCANTARA;"; // "RSLIM\JVIMPERIAL;" '"IMPERIAL\JIMPERIAL2014;" '

            ////var initialCatalog = databaseName + ";";
            ////connectionString = "Data Source=" + dataSource + "Initial Catalog=" + initialCatalog + "Integrated Security=True;";
            
            //var initialCatalog = databaseName + ";";
            //var userId = "lester;";
            //var password = "M3g@w0rld123;";


            var dataSource = "MW-CPDPORTAL01;";

            var initialCatalog = databaseName + ";";
            var userId = "sa;";
            var password = "26p0rt@Lcpd;";

            connectionString = "Data Source=" + dataSource + "Initial Catalog=" + initialCatalog + "User Id=" + userId + "Password=" + password;
                       
            return connectionString;
        }
    }
}