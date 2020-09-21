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

        //GET: Subcategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subcategory/Create
        [HttpPost]
        public ActionResult CreateUpdate(SubcategoryModel x)
        {
            try
            {
                using (WMSEntities context = new WMSEntities()) {
                    SubCategory query = new SubCategory() { 
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
            }
            catch
            {
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
