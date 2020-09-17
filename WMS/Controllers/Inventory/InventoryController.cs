using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Inventory;
using WMS.ViewModels;

namespace WMS.Controllers.Inventory
{
    public class InventoryController : Controller
    {
        //private WMSContext db = new WMSContext();
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inventory/Details/5
        public JsonResult GetRecord()
        {
            try {
                using (var context = new WMSEntities())
                {
                    var record = from A in context.View_Inventory 
                                 join B in context.View_Category
                                 on A.CategoryID equals B.CategoryID
                                 //join C in context.View_Subcategory
                                 //on A.SubCategoryID equals C.SubCategoryID
                                 join D in context.View_Location
                                 on A.WarehouseID equals D.WarehouseID
                                 select new
                                 {
                                     B.CategoryID,
                                     B.CategoryName,
                                     //C.SubCategoryID,
                                     //C.SubCategoryName,
                                     D.WarehouseID,
                                     D.WarehouseName,
                                     A.ItemID,
                                     A.ItemName,
                                     A.ItemDescription,
                                     A.Stocks,
                                     A.UsedStocks,
                                     A.Unit,
                                     A.MISNo,
                                     A.MaterialCode,
                                     A.Origin,
                                     A.ReceivedDate,
                                     A.Remarks,
                                     A.CreatedBy,
                                     A.CreatedDate,
                                     A.ModifiedBy,
                                     A.ModifiedDate,
                                     A.DeletedBy,
                                     A.DeletedDate
                                 };
                    return Json(record.ToList(), JsonRequestBehavior.AllowGet);

                }
            } catch (Exception e) {
                InventoryModel model = new InventoryModel();
                model.msg = Convert.ToString(e);
                return Json(model.msg, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
