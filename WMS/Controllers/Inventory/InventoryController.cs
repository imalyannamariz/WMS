using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Inventory;
using WMS.Models.Subcategory;
using WMS.ViewModels;

namespace WMS.Controllers.Inventory
{
    public class InventoryController : Controller
    {
        //private WMSContext db = new WMSContext();
        // GET: Inventory
        public ActionResult Index()
        {
            using (WMSEntities context = new WMSEntities())
            {
                var category = new SelectList(context.View_Category.ToList(), "CategoryID", "CategoryName");
                ViewData["CategoryList"] = category;

                //var query = new SelectList(context.View_Subcategory.Where().ToList(), "CategoryID", "CategoryName");
                //ViewData["SubCategoryList"] = "Select Subcategory";

                var location = new SelectList(context.View_Location.ToList(), "WarehouseID", "WarehouseName");
                ViewData["LocationList"] = location;
                return View();
            }
        }

        public JsonResult SubcategoryOptions(int CategoryID)
        {
            using (WMSEntities context = new WMSEntities())
            {
                var query = from x in context.View_Subcategory where x.CategoryID == CategoryID select new {x.SubCategoryID, x.SubCategoryName };
                return Json(query.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Inventory/Details/5
        public JsonResult GetRecord()
        {
            try {
                using (WMSEntities context = new WMSEntities())
                {
                    var record = from A in context.View_Inventory 
                                 join B in context.View_Category
                                 on A.CategoryID equals B.CategoryID
                                 join C in context.View_Subcategory
                                 on A.SubCategoryID equals C.SubCategoryID
                                 join D in context.View_Location
                                 on A.WarehouseID equals D.WarehouseID
                                 select new
                                 {
                                     B.CategoryID,
                                     B.CategoryName,
                                     C.SubCategoryID,
                                     C.SubCategoryName,
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

        // GET: Inventory/GetRowDetails
        public JsonResult GetRowDetails(int ItemID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var record = from A in context.View_Inventory
                                 join B in context.View_Category
                                 on A.CategoryID equals B.CategoryID
                                 join C in context.View_Subcategory
                                 on A.SubCategoryID equals C.SubCategoryID
                                 join D in context.View_Location
                                 on A.WarehouseID equals D.WarehouseID
                                 select new
                                 {
                                     B.CategoryID,
                                     B.CategoryName,
                                     C.SubCategoryID,
                                     C.SubCategoryName,
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
                    return Json(record.FirstOrDefault(m=>m.ItemID == ItemID), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                InventoryModel model = new InventoryModel();
                model.msg = Convert.ToString(e);
                return Json(model.msg, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult CreateUpdate(InventoryModel x)
        {
            try
            {                
                using (WMSEntities context = new WMSEntities())
                {
                    if (x.mode == 1)
                    {
                        DataAccess.Models.Inventory query = context.Inventories.FirstOrDefault(m => m.ItemID == x.ItemID);
                        query.CategoryID = x.CategoryID;
                        query.SubCategoryID = x.SubCategoryID;
                        query.WarehouseID = x.WarehouseID;
                        query.ItemName = x.ItemName;
                        query.ItemDescription = x.ItemDescription;
                        query.Stocks = x.Stocks;
                        query.UsedStocks = x.UsedStocks;
                        query.Unit = x.Unit;
                        query.MaterialCode = x.MaterialCode;
                        query.Origin = x.Origin;
                        query.MISNo = x.MISNo;
                        query.ReceivedDate = Convert.ToDateTime(x.ReceivedDate);
                        query.Remarks = x.Remarks;
                        //query.ModifiedBy = "";
                        query.ModifiedDate = DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss");

                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var checker = context.Inventories.FirstOrDefault(m => m.ItemName == x.ItemName);

                        if (checker == null)
                        {
                            DataAccess.Models.Inventory model = new DataAccess.Models.Inventory()
                            {
                                CategoryID = x.CategoryID,
                                SubCategoryID = x.SubCategoryID,
                                WarehouseID = x.WarehouseID,
                                ItemName = x.ItemName,
                                ItemDescription = x.ItemDescription,
                                Stocks = x.Stocks,
                                UsedStocks = x.UsedStocks,
                                Unit = x.Unit,
                                MaterialCode = x.MaterialCode,
                                Origin = x.Origin,
                                MISNo = x.MISNo,
                                ReceivedDate = Convert.ToDateTime(x.ReceivedDate),
                                Remarks = x.Remarks,
                                //CreatedBy = "",
                                CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss")),
                                IsDeleted = false
                            };
                            context.Inventories.Add(model);
                            context.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return RedirectToAction("Index");
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
