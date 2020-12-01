using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Report;

namespace WMS.Controllers.GenerateReport
{
    public class GenerateReportController : Controller
    {
        // GET: GenerateReport
        public ActionResult Index()
        {
            using (WMSEntities context = new WMSEntities())
            {
                var category = new SelectList(context.View_Category.ToList(), "CategoryID", "CategoryName");
                ViewData["CategoryList"] = category;

                var location = new SelectList(context.View_Location.ToList(), "WarehouseID", "WarehouseName");
                ViewData["LocationList"] = location;
                return View();
            }
        }

        // GET: GenerateReport/Details/5
        public ActionResult Report(int CategoryID, int WarehouseID, string StartDate, string EndDate)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var record = from A in context.View_Inventory
                                 join B in context.View_Category
                                 on A.CategoryID equals B.CategoryID
                                 join D in context.View_Location
                                 on A.WarehouseID equals D.WarehouseID
                                 select new
                                 {
                                     B.CategoryID,
                                     B.CategoryName,
                                     D.WarehouseID,
                                     D.WarehouseName,
                                     A.ItemID,
                                     A.ItemName,
                                     A.ItemDescription,
                                     A.Stocks,
                                     A.UsedStocks,
                                     A.ReservedStocks,
                                     A.AvailableStocks,
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

                    var start = DateTime.Parse(StartDate);
                    var end = DateTime.Parse(EndDate);
                    var query = record.Where(x => x.CreatedDate >= start && x.CreatedDate <= end);
                    var result = query.ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                ReportModel model = new ReportModel();
                model.msg = Convert.ToString(e);
                return Json(model.msg, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: GenerateReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerateReport/Create
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

        // GET: GenerateReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenerateReport/Edit/5
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

        // GET: GenerateReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenerateReport/Delete/5
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
