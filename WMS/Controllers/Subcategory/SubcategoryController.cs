using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMS.Controllers.Subcategory
{
    public class SubcategoryController : Controller
    {
        // GET: Subcategory
        public ActionResult Index()
        {
            return View();
        }

        // GET: Subcategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subcategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subcategory/Create
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
