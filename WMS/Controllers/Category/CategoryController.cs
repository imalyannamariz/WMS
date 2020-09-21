using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Category;

namespace WMS.Controllers.Category
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        // GET: Category/Details/5
        public JsonResult GetRecords()
        {
            try
            {
                using (WMSEntities context = new WMSEntities()) {
                    var query = from x in context.View_Category select x;

                    return Json(query.ToList(), JsonRequestBehavior.AllowGet);
                }                
            }
            catch (Exception e) {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Category/Create
        public JsonResult GetRowDetails(int CategoryID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = from x in context.View_Category where x.CategoryID == CategoryID select x;

                    return Json(query.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Category/CreateUpdate
        [HttpPost]
        public ActionResult CreateUpdate(CategoryModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities()) {
                    if (x.mode == 1)
                    {
                        var query = context.Categories.FirstOrDefault(m => m.CategoryID == x.CategoryID);
                        query.CategoryName = x.CategoryName;
                        query.CategoryDescription = x.CategoryDescription;
                        //query.ModifiedBy = category.ModifiedBy;
                        query.ModifiedDate = DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss");

                        context.SaveChanges();
                        TempData["Message"] = "Success!";
                        return RedirectToAction("Index");
                    }
                    else {
                        var checker = context.Categories.FirstOrDefault(b => b.CategoryName == x.CategoryName);

                        if (checker == null)
                        {
                            DataAccess.Models.Category query = new DataAccess.Models.Category()
                            {
                                CategoryName = x.CategoryName,
                                CategoryDescription = x.CategoryDescription,
                                //query.CreatedBy = Session();
                                CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss")),
                                IsDeleted = false
                            };

                            context.Categories.Add(query);
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
            catch
            {
                return RedirectToAction("Index");
            }
        }              

        // GET: Category/Delete/5
        public JsonResult GetDelete(int CategoryID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities()) {
                    var query = from x in context.View_Category where x.CategoryID == CategoryID select x;

                    return Json(query.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e) {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(CategoryModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = context.Categories.FirstOrDefault(m=>m.CategoryID == x.CategoryID);
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
    }
}
