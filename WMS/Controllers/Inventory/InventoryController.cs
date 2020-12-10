using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            using (WMSEntities context = new WMSEntities())
            {
                var category = new SelectList(context.View_Category.ToList(), "CategoryID", "CategoryName");
                ViewData["CategoryList"] = category;
                
                var location = new SelectList(context.View_Location.ToList(), "WarehouseID", "WarehouseName");
                ViewData["LocationList"] = location;
                return View();
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
                        if ((x.UsedStocks + x.ReservedStocks) > x.Stocks) 
                        {
                            TempData["message"] = "Used/Reserved Stocks cannot be greater than Stocks.";
                            return RedirectToAction("Index");
                        }
                        else if (x.UsedStocks > x.Stocks)
                        {
                            TempData["message"] = "Used Stocks cannot be greater than Stocks.";
                            return RedirectToAction("Index");
                        }
                        else if (x.ReservedStocks > x.Stocks)
                        {
                            TempData["message"] = "Reserved Stocks cannot be greater than Stocks.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            query.CategoryID = x.CategoryID;
                            query.WarehouseID = x.WarehouseID;
                            query.ItemName = x.ItemName;
                            query.ItemDescription = x.ItemDescription;
                            query.Stocks = x.Stocks;
                            query.UsedStocks = x.UsedStocks;
                            query.ReservedStocks = x.ReservedStocks;
                            query.Unit = x.Unit;
                            query.MaterialCode = x.MaterialCode;
                            query.Origin = x.Origin;
                            query.MISNo = x.MISNo;
                            query.ReceivedDate = Convert.ToDateTime(x.ReceivedDate);
                            query.Remarks = x.Remarks;
                            //query.ModifiedBy = "";
                            query.ModifiedDate = DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss");

                            context.SaveChanges();
                            TempData["message"] = "Success!";
                            return RedirectToAction("Index");
                        }                        
                    }
                    else
                    {
                        var checker = context.Inventories.FirstOrDefault(m => m.ItemName == x.ItemName);

                        if (checker == null)
                        {
                            if (x.UsedStocks > x.Stocks)
                            {
                                TempData["message"] = "Used Stocks cannot be greater than Stocks.";
                                return RedirectToAction("Index");
                            }
                            else if (x.ReservedStocks > x.Stocks) 
                            {
                                TempData["message"] = "Reserved Stocks cannot be greater than Stocks.";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                DataAccess.Models.Inventory model = new DataAccess.Models.Inventory()
                                {
                                    CategoryID = x.CategoryID,
                                    WarehouseID = x.WarehouseID,
                                    ItemName = x.ItemName,
                                    ItemDescription = x.ItemDescription,
                                    Stocks = x.Stocks,
                                    UsedStocks = x.UsedStocks,
                                    ReservedStocks = x.ReservedStocks,
                                    Unit = String.IsNullOrEmpty(x.Unit) ? "" : x.Unit,
                                    MaterialCode = x.MaterialCode,
                                    Origin = String.IsNullOrEmpty(x.Origin) ? "" : x.Origin,
                                    MISNo = x.MISNo,
                                    ReceivedDate = Convert.ToDateTime(x.ReceivedDate),
                                    Remarks = String.IsNullOrEmpty(x.Remarks) ? "" : x.Remarks,
                                    //CreatedBy = "",
                                    CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss")),
                                    IsDeleted = false
                                };
                                context.Inventories.Add(model);
                                context.SaveChanges();
                                TempData["message"] = "Success!";
                                return RedirectToAction("Index");
                            }                            
                        }
                        TempData["message"] = "Data Already Exist!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                Response.Output.Write(e);
                return RedirectToAction("Index");
            }
        }

        // GET: Inventory/GetHistory
        public JsonResult GetHistory(int ItemID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var record = from A in context.View_InventoryLog
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

                    var result = record.Where(m => m.ItemID == ItemID).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                InventoryModel model = new InventoryModel();
                model.msg = Convert.ToString(e);
                return Json(model.msg, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Category/Delete/5
        public JsonResult GetDelete(int ItemID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = from x in context.View_Inventory where x.ItemID == ItemID select x;

                    return Json(query.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(InventoryModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = context.Inventories.FirstOrDefault(m => m.ItemID == x.ItemID);
                    query.IsDeleted = true;
                    //query.DeletedBy = x.DeletedBy;
                    query.DeletedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss"));
                    context.SaveChanges();
                    TempData["Message"] = "Success!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return View();
            }
        }

        public JsonResult ExportHistory(int ItemID)
        {
            try
            {
                string excelPath;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                    worksheet.View.ShowGridLines = true;

                    worksheet.Row(1).Style.Font.Bold = true;
                    worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1].Value = "Category";
                    worksheet.Cells[1, 2].Value = "Warehouse";
                    worksheet.Cells[1, 3].Value = "Item";
                    worksheet.Cells[1, 4].Value = "Item Description";
                    worksheet.Cells[1, 5].Value = "Stocks";
                    worksheet.Cells[1, 6].Value = "Used Stocks";
                    worksheet.Cells[1, 7].Value = "Reserved Stocks";
                    worksheet.Cells[1, 8].Value = "Available Stocks";
                    worksheet.Cells[1, 9].Value = "Unit";
                    worksheet.Cells[1, 10].Value = "MIS No";
                    worksheet.Cells[1, 11].Value = "Material Code";
                    worksheet.Cells[1, 12].Value = "Origin";
                    worksheet.Cells[1, 13].Value = "Received Date";
                    worksheet.Cells[1, 14].Value = "Created Date";
                    worksheet.Cells[1, 15].Value = "Created By";
                    worksheet.Cells[1, 14].Value = "Modified Date";
                    worksheet.Cells[1, 15].Value = "Modified By";
                    worksheet.Cells[1, 16].Value = "Remarks";
                    worksheet.Cells[1, 17].Value = "Location";
                    worksheet.Cells[1, 18].Value = "Subcategory";

                    using (WMSEntities context = new WMSEntities())
                    {
                        var record = from A in context.View_InventoryLog
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

                        var result = record.Where(m => m.ItemID == ItemID).ToList();

                        int row = 2;
                        for (int i = 0; i <= result.Count() - 1; i++)
                        {
                            worksheet.Cells[row, 1].Value = result[i].CategoryName;
                            worksheet.Cells[row, 2].Value = result[i].WarehouseName;
                            worksheet.Cells[row, 3].Value = result[i].ItemName;
                            worksheet.Cells[row, 4].Value = result[i].ItemDescription;
                            worksheet.Cells[row, 5].Value = result[i].Stocks;
                            worksheet.Cells[row, 6].Value = result[i].UsedStocks;
                            worksheet.Cells[row, 7].Value = result[i].ReservedStocks;
                            worksheet.Cells[row, 8].Value = result[i].AvailableStocks;
                            worksheet.Cells[row, 9].Value = result[i].Unit;
                            worksheet.Cells[row, 10].Value = result[i].MISNo;
                            worksheet.Cells[row, 11].Value = result[i].MaterialCode;
                            worksheet.Cells[row, 12].Value = result[i].Origin;
                            worksheet.Cells[row, 13].Value = result[i].ReceivedDate;
                            worksheet.Cells[row, 14].Value = Convert.ToString(result[i].CreatedDate);
                            worksheet.Cells[row, 15].Value = result[i].CreatedBy;
                            worksheet.Cells[row, 14].Value = Convert.ToString(result[i].ModifiedDate);
                            worksheet.Cells[row, 15].Value = result[i].ModifiedBy;
                            worksheet.Cells[row, 16].Value = result[i].Remarks;

                            row++;
                        }
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    //saving file
                    var stream = new MemoryStream(excelPackage.GetAsByteArray());
                    stream.Seek(0, SeekOrigin.Begin);

                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            Byte[] xbyte = ms.GetBuffer();
                            excelPath = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + Convert.ToBase64String(xbyte);
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["message"] = ex.ToString();
                        excelPath = "";
                        return Json(excelPath, JsonRequestBehavior.AllowGet);
                    }

                    return Json(excelPath, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(TempData["message"] = ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
