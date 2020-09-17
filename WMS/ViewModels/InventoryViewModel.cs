using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS.DataAccess.Models;
using WMS.Models.Inventory;
namespace WMS.ViewModels
{
    public class InventoryViewModel
    {
        public IEnumerable<View_Inventory> Inventories { get; set; }
    }
}