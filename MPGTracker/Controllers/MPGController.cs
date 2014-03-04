﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPGTracker.Models;

namespace MPGTracker.Controllers
{
    
    public class MPGController : Controller
    {
        private CarTrackerEntities db = new CarTrackerEntities();

        //
        // GET: /MPG/

        public ActionResult Index()
        {
            var mpgs = db.MPGs.Include(m => m.Car);
            return View(mpgs.OrderByDescending(m=> m.Id).ToList());
        }

        public ActionResult Car(string Owner)
        {
            var dbResults = db.MPGs.Include(m => m.Car);
            var mpgs = from x in dbResults
                       where x.Car.Owner == Owner
                       select x;
            return View(mpgs.OrderByDescending(m => m.Id).ToList());
        }

        //
        // GET: /MPG/Details/5

        public ActionResult Details(int id = 0)
        {
            MPG mpg = db.MPGs.Find(id);
            if (mpg == null)
            {
                return HttpNotFound();
            }
            return View(mpg);
        }

        //
        // GET: /MPG/Create

        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner");
            return View();
        }

        //
        // POST: /MPG/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MPG mpg, string password)
        {
            if (ModelState.IsValid && password == System.Configuration.ConfigurationManager.AppSettings["passphrase"].ToString())
            {
                db.MPGs.Add(mpg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner", mpg.CarId);
            return View(mpg);
        }

        //
        // GET: /MPG/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MPG mpg = db.MPGs.Find(id);
            if (mpg == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner", mpg.CarId);
            return View(mpg);
        }

        //
        // POST: /MPG/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MPG mpg, string password)
        {
            if (ModelState.IsValid && password == "rdwalw")
            {
                db.Entry(mpg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Owner", mpg.CarId);
            return View(mpg);
        }

        //
        // GET: /MPG/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MPG mpg = db.MPGs.Find(id);
            if (mpg == null)
            {
                return HttpNotFound();
            }
            return View(mpg);
        }

        //
        // POST: /MPG/Delete/5
        [Authorize(Roles = "Admin, Family")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MPG mpg = db.MPGs.Find(id);
            db.MPGs.Remove(mpg);
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