//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WMS.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_Inventory
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
    }
}
