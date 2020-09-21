using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Warehouse;

namespace WMS.Controllers.Warehouse
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        public ActionResult Index()
        {
            return View();
        }

        // GET: Warehouse/GetRecord/5
        public JsonResult GetRecord()
        {
            try
            {
                using (WMSEntities context = new WMSEntities()) {
                    var query = from warehouse in context.View_Location select warehouse;
                    return Json(query.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e) {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }            
        }

        // GET: Warehouse/GetRowDetails
        public JsonResult GetRowDetails(int WarehouseID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = from warehouse in context.View_Location where warehouse.WarehouseID == WarehouseID select warehouse;
                    return Json(query.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Warehouse/CreateUpdate
        [HttpPost]
        public ActionResult CreateUpdate(WarehouseModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    if (x.Mode == 1)
                    {
                        var query = context.Locations.FirstOrDefault(b => b.WarehouseID == x.WarehouseID);
                        query.WarehouseName = x.WarehouseName;
                        query.WarehouseAddress = x.WarehouseAddress;
                        //query.ModifiedBy = x.ModifiedBy;
                        query.ModifiedDate = DateTime.Now.ToString("yyyy"+ "-"+"MM"+"-"+"dd"+" "+"HH"+":"+"mm"+":"+"ss");
                        
                        context.SaveChanges();
                        TempData["message"] = "Success!";
                        return RedirectToAction("Index");
                    }
                    else 
                    {
                        var checker = context.Locations.FirstOrDefault(b => b.WarehouseName == x.WarehouseName);

                        if (checker == null)
                        {
                            Location query = new Location()
                            {
                                WarehouseName = x.WarehouseName,
                                WarehouseAddress = x.WarehouseAddress,
                                //query.CreatedBy = Session();
                                CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss")),
                                IsDeleted = false
                            };

                            context.Locations.Add(query);
                            context.SaveChanges();
                            TempData["message"] = "Success!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["message"] = "Data Already Exist!";
                            return RedirectToAction("Index");
                        }
                    }                    
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return View();
            }
        }
        
        // GET: Warehouse/Delete/5
        public JsonResult GetDelete(WarehouseModel x)
        {
            using (WMSEntities context = new WMSEntities())
            {
                var query = from warehouse in context.View_Location where warehouse.WarehouseID == x.WarehouseID select warehouse;
                return Json(query.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }            
        }

        // POST: Warehouse/Delete/5
        [HttpPost]
        public ActionResult Delete(WarehouseModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities()) {

                    var query = context.Locations.FirstOrDefault(b => b.WarehouseID == x.WarehouseID);
                    query.IsDeleted = true;
                    //query.DeletedBy = x.DeletedBy;
                    query.DeletedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss"));

                    context.SaveChanges();
                    TempData["message"] = "Success!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return View();
            }
        }
    }
}
