using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMS.Controllers.GenerateReport
{
    public class GenerateReportController : Controller
    {
        // GET: GenerateReport
        public ActionResult Index()
        {
            return View();
        }

        // GET: GenerateReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
