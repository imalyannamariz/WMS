using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS.DataAccess.Models;

namespace WMS.Models.Inventory
{    
    public class InventoryModel
    {
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int WarehouseID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Stocks { get; set; }
        public int UsedStocks { get; set; }
        public string Unit { get; set; }
        public string MaterialCode { get; set; }
        public long MISNo { get; set; }
        public string Origin { get; set; }
        public string ReceivedDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedDate { get; set; }
        public string msg { get; set; }

        //public IEnumerable<Location> Locations { get; set; }
        //public IEnumerable<Category> Categories { get; set; }
        //public IEnumerable<SubCategory> Subcategories { get; set; }

    }
}