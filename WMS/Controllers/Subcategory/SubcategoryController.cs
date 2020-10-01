using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WMS.DataAccess.Models;
using WMS.Models.Subcategory;

namespace WMS.Controllers.Subcategory
{
    public class SubcategoryController : Controller
    {
        // GET: Subcategory
        public ActionResult Index()
        {
            using (WMSEntities context = new WMSEntities())
            {
                var query = new SelectList(context.View_Category.ToList(), "CategoryID", "CategoryName");
                ViewData["CategoryList"] = query;
                return View();
            }
        }

        //GET: Subcategory/GetRecords
        public JsonResult GetRecords()
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = from A in context.View_Subcategory
                                join B in context.View_Category
                                on A.CategoryID equals B.CategoryID
                                select new
                                {
                                    A.CategoryID,
                                    A.SubCategoryID,
                                    A.SubCategoryName,
                                    A.SubCategoryDescription,
                                    B.CategoryName,
                                    A.CreatedBy,
                                    A.CreatedDate,
                                    A.ModifiedBy,
                                    A.ModifiedDate,
                                    A.IsDeleted,
                                    A.DeletedBy,
                                    A.DeletedDate
                                };

                    return Json(query.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e) {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //GET: Subcategory/GetRowDetails
        public JsonResult GetRowDetails(int SubCategoryID)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    var query = from A in context.View_Subcategory
                                join B in context.View_Category
                                on A.CategoryID equals B.CategoryID
                                where A.SubCategoryID == SubCategoryID
                                select new
                                {
                                    A.CategoryID,
                                    A.SubCategoryID,
                                    A.SubCategoryName,
                                    A.SubCategoryDescription,
                                    B.CategoryName,
                                    A.CreatedBy,
                                    A.CreatedDate,
                                    A.ModifiedBy,
                                    A.ModifiedDate,
                                    A.IsDeleted,
                                    A.DeletedBy,
                                    A.DeletedDate
                                };

                    return Json(query.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Subcategory/Create
        [HttpPost]
        public ActionResult CreateUpdate(SubcategoryModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities())
                {
                    if (x.mode == 1)
                    {
                        SubCategory query = context.SubCategories.FirstOrDefault(m => m.SubCategoryID == x.SubCategoryID);
                        query.SubCategoryName = x.SubCategoryName;
                        query.SubCategoryDescription = x.SubCategoryDescription;
                        query.CategoryID = x.CategoryID;
                        //query.ModifiedBy = x.ModifiedBy,
                        query.ModifiedDate = DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss");
                        query.IsDeleted = false;

                        context.SaveChanges();
                        TempData["message"] = "Success!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var checker = context.View_Subcategory.FirstOrDefault(a => a.SubCategoryName == x.SubCategoryName);

                        if (checker == null)
                        {
                            SubCategory query = new SubCategory()
                            {
                                SubCategoryName = x.SubCategoryName,
                                SubCategoryDescription = x.SubCategoryDescription,
                                CategoryID = x.CategoryID,
                                //CreatedBy = x.CreatedBy,
                                CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy" + "-" + "MM" + "-" + "dd" + " " + "HH" + ":" + "mm" + ":" + "ss")),
                                IsDeleted = false
                            };
                            context.SubCategories.Add(query);
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

        // GET: Subcategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subcategory/Edit/5
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

        // GET: Subcategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subcategory/Delete/5
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
