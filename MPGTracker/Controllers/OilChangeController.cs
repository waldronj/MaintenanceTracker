using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPGTracker.Models;

namespace MPGTracker.Controllers
{
    [Authorize(Roles = "Admin, Family")]
    public class OilChangeController : Controller
    {
        private CarTrackerEntities db = new CarTrackerEntities();

        //
        // GET: /OilChange/

        public ActionResult Index()
        {
            var oilchanges = db.OilChanges.Include(o => o.Car);
            return View(oilchanges.ToList());
        }

        //
        // GET: /OilChange/Details/5

        public ActionResult Details(int id = 0)
        {
            OilChange oilchange = db.OilChanges.Find(id);
            if (oilchange == null)
            {
                return HttpNotFound();
            }
            return View(oilchange);
        }

        //
        // GET: /OilChange/Create

        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner");
            return View();
        }

        //
        // POST: /OilChange/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OilChange oilchange)
        {
            if (ModelState.IsValid)
            {
                db.OilChanges.Add(oilchange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner", oilchange.CarId);
            return View(oilchange);
        }

        //
        // GET: /OilChange/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OilChange oilchange = db.OilChanges.Find(id);
            if (oilchange == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner", oilchange.CarId);
            return View(oilchange);
        }

        //
        // POST: /OilChange/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OilChange oilchange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oilchange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner", oilchange.CarId);
            return View(oilchange);
        }

        //
        // GET: /OilChange/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OilChange oilchange = db.OilChanges.Find(id);
            if (oilchange == null)
            {
                return HttpNotFound();
            }
            return View(oilchange);
        }

        //
        // POST: /OilChange/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OilChange oilchange = db.OilChanges.Find(id);
            db.OilChanges.Remove(oilchange);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}