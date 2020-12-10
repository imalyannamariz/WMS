using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Report;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using System.Windows.Forms;
using System.Threading;

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
                    var filterByDate = record.Where(x => x.CreatedDate >= start && x.CreatedDate <= end);

                    if (CategoryID != 0 && WarehouseID != 0)
                    {
                        var filterByCategory = filterByDate.Where(x => x.CategoryID == CategoryID);
                        var filterByWarehouse = filterByCategory.Where(x => x.WarehouseID == WarehouseID);
                        var result = filterByWarehouse.ToList();
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else if (CategoryID != 0)
                    {
                        var filterByCategory = filterByDate.Where(x => x.CategoryID == CategoryID);
                        var result = filterByCategory.ToList();
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else if (WarehouseID != 0)
                    {
                        var filterByWarehouse = filterByDate.Where(x => x.WarehouseID == WarehouseID);
                        var result = filterByWarehouse.ToList();
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        var result = filterByDate.ToList();
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                ReportModel model = new ReportModel();
                model.msg = Convert.ToString(e);
                return Json(model.msg, JsonRequestBehavior.AllowGet);
            }
        }
                
        public JsonResult Export(int CategoryID, int WarehouseID, string StartDate, string EndDate)
        {
            try
            {
                string excelPath;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage()) {

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
                    worksheet.Cells[1, 16].Value = "Remarks";
                    worksheet.Cells[1, 17].Value = "Location";
                    worksheet.Cells[1, 18].Value = "Subcategory";
                    
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
                        var filterByDate = record.Where(x => x.CreatedDate >= start && x.CreatedDate <= end);

                        if (CategoryID != 0 && WarehouseID != 0)
                        {
                            var filterByCategory = filterByDate.Where(x => x.CategoryID == CategoryID);
                            var filterByWarehouse = filterByCategory.Where(x => x.WarehouseID == WarehouseID);
                            var result = filterByWarehouse.ToList();

                            int row = 2;
                            for (int i = 0; i <= result.Count() - 1; i++) {
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
                                worksheet.Cells[row, 14].Value = Convert.ToDateTime(result[i].CreatedDate);
                                worksheet.Cells[row, 15].Value = result[i].CreatedBy;
                                worksheet.Cells[row, 16].Value = result[i].Remarks;
                                
                                row++;
                            }
                        }
                        else if (CategoryID != 0)
                        {
                            var filterByCategory = filterByDate.Where(x => x.CategoryID == CategoryID);
                            var result = filterByCategory.ToList();

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
                                worksheet.Cells[row, 16].Value = result[i].Remarks;

                                row++;
                            }
                        }
                        else if (WarehouseID != 0)
                        {
                            var filterByWarehouse = filterByDate.Where(x => x.WarehouseID == WarehouseID);
                            var result = filterByWarehouse.ToList();

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
                                worksheet.Cells[row, 16].Value = result[i].Remarks;

                                row++;
                            }
                        }
                        else
                        {
                            var result = filterByDate.ToList();

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
                                worksheet.Cells[row, 16].Value = result[i].Remarks;

                                row++;
                            }
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
                    catch (Exception ex) {
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
