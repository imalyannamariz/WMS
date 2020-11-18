using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WMS.DataAccess.Models;

namespace WMS.Models.Inventory
{
    public class InventoryModel
    {
        [Required(ErrorMessage = "Category Name is required")]
        [DisplayName("Category Name")]
        public int CategoryID { get; set; }


        [DisplayName("Subcategory Name")]
        public int SubCategoryID { get; set; }


        [Required(ErrorMessage = "Location Name is required")]
        [DisplayName("Location Name")]
        public int WarehouseID { get; set; }


        public int ItemID { get; set; }

        [Required(ErrorMessage = "Item Name is required")]
        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [DisplayName("Item Description")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Stocks is required")]
        public int Stocks { get; set; }

        [DisplayName("Used Stocks")]
        public int UsedStocks { get; set; }
        public string Unit { get; set; }

        [DisplayName("Material Code")]
        public string MaterialCode { get; set; }
        public long MISNo { get; set; }
        public string Origin { get; set; }

        [DisplayName("Received Date")]
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
        public int mode { get; set; }
    }
}