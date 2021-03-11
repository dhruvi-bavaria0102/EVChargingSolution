using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVCharging.Data;

namespace EVCharging.Admin.Controllers
{
    public class ChargersController : Controller
    {
        private EVModelEntities db = new EVModelEntities();

        // GET: Chargers
        public ActionResult Index()
        {
            var chargers = db.Chargers.Include(c => c.Location);
            return View(chargers.ToList());
        }

        // GET: Chargers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charger charger = db.Chargers.Find(id);
            if (charger == null)
            {
                return HttpNotFound();
            }
            return View(charger);
        }

        // GET: Chargers/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "LocationName");
            return View();
        }

        // POST: Chargers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationID,Availability,InsertDate,UpdateDate,DeleteDate,IsDeleted,ChargerName")] Charger charger)
        {
            if (ModelState.IsValid)
            {
                charger.InsertDate = DateTime.UtcNow;
                db.Chargers.Add(charger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "ID", "LocationName", charger.LocationID);
            return View(charger);
        }

        // GET: Chargers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charger charger = db.Chargers.Find(id);
            if (charger == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "LocationName", charger.LocationID);
            return View(charger);
        }

        // POST: Chargers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationID,Availability,InsertDate,UpdateDate,DeleteDate,IsDeleted,ChargerName")] Charger charger)
        {
            if (ModelState.IsValid)
            {
                charger.UpdateDate = DateTime.UtcNow;
                db.Entry(charger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "LocationName", charger.LocationID);
            return View(charger);
        }

        // GET: Chargers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charger charger = db.Chargers.Find(id);
            if (charger == null)
            {
                return HttpNotFound();
            }
            return View(charger);
        }

        // POST: Chargers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Charger charger)
        {
            using (EVModelEntities entities = new EVModelEntities())
            {
                Charger updatedCharger = (from c in entities.Chargers
                                            where c.Id == charger.Id
                                            select c).FirstOrDefault();
                updatedCharger.DeleteDate = DateTime.UtcNow;
                updatedCharger.IsDeleted = true;
                entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
