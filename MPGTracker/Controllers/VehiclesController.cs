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
    public class VehiclesController : Controller
    {
        private CarTrackerEntities db = new CarTrackerEntities();

        //
        // GET: /Vehicles/

        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        //
        // GET: /Vehicles/Details/5

        public ActionResult Details(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // GET: /Vehicles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vehicles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car, string password)
        {
            if (ModelState.IsValid && password == System.Configuration.ConfigurationManager.AppSettings["passphrase"])
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        //
        // GET: /Vehicles/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // POST: /Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        //
        // GET: /Vehicles/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // POST: /Vehicles/Delete/5
        [Authorize(Roles = "Admin, Family")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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