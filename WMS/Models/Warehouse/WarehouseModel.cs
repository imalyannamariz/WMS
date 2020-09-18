using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WMS.Models.Warehouse
{
    public class WarehouseModel
    {
        public int WarehouseID { get; set; }

        [Required(ErrorMessage = "Warehouse Name is required")]
        [DisplayName("Warehouse Name")]
        public string WarehouseName { get; set; }
        [Required(ErrorMessage = "Warehouse Address is required")]
        [DisplayName("Warehouse Address")]
        public string WarehouseAddress { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedDate { get; set; }
        public string msg { get; set; }
        public int Mode { get; set; }
    }
}