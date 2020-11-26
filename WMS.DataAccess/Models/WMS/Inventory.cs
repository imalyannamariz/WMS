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
    
    public partial class Inventory
    {
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> WarehouseID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> Stocks { get; set; }
        public Nullable<int> UsedStocks { get; set; }
        public string Unit { get; set; }
        public string MaterialCode { get; set; }
        public Nullable<long> MISNo { get; set; }
        public string Origin { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Location Location { get; set; }
    }
}
