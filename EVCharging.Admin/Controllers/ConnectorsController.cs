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
    public class ConnectorsController : Controller
    {
        private EVModelEntities db = new EVModelEntities();

        // GET: Connectors
        public ActionResult Index()
        {
            var connectors = db.Connectors.Include(c => c.Charger).Include(c => c.Connector_Types).Include(c => c.Location);
            return View(connectors.ToList());
        }

        // GET: Connectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connector connector = db.Connectors.Find(id);
            if (connector == null)
            {
                return HttpNotFound();
            }
            return View(connector);
        }

        // GET: Connectors/Create
        public ActionResult Create()
        {
            ViewBag.ChargerId = new SelectList(db.Chargers, "Id", "ChargerName");
            ViewBag.ConnectorTypeId = new SelectList(db.Connector_Types, "Id", "ConnectorName");
            ViewBag.LocationId = new SelectList(db.Locations, "ID", "LocationName");
            return View();
        }

        // POST: Connectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ChargerId,LocationId,ConnectorTypeId,ConnectorStatus,SMSCode,ConnectorUnId,Tariff,InsertDate,UpdateDate,DeleteDate,isDeleted")] Connector connector)
        {
            if (ModelState.IsValid)
            {
                connector.InsertDate = DateTime.UtcNow;
                db.Connectors.Add(connector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChargerId = new SelectList(db.Chargers, "Id", "ChargerName", connector.ChargerId);
            ViewBag.ConnectorTypeId = new SelectList(db.Connector_Types, "Id", "ConnectorName", connector.ConnectorTypeId);
            ViewBag.LocationId = new SelectList(db.Locations, "ID", "LocationName", connector.LocationId);
            return View(connector);
        }

        // GET: Connectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connector connector = db.Connectors.Find(id);
            if (connector == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChargerId = new SelectList(db.Chargers, "Id", "ChargerName", connector.ChargerId);
            ViewBag.ConnectorTypeId = new SelectList(db.Connector_Types, "Id", "ConnectorName", connector.ConnectorTypeId);
            ViewBag.LocationId = new SelectList(db.Locations, "ID", "LocationName", connector.LocationId);
            return View(connector);
        }

        // POST: Connectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ChargerId,LocationId,ConnectorTypeId,ConnectorStatus,SMSCode,ConnectorUnId,Tariff,InsertDate,UpdateDate,DeleteDate,isDeleted")] Connector connector)
        {
            if (ModelState.IsValid)
            {
                connector.UpdateDate = DateTime.UtcNow;
                db.Entry(connector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChargerId = new SelectList(db.Chargers, "Id", "ChargerName", connector.ChargerId);
            ViewBag.ConnectorTypeId = new SelectList(db.Connector_Types, "Id", "ConnectorName", connector.ConnectorTypeId);
            ViewBag.LocationId = new SelectList(db.Locations, "ID", "LocationName", connector.LocationId);
            return View(connector);
        }

        // GET: Connectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connector connector = db.Connectors.Find(id);
            if (connector == null)
            {
                return HttpNotFound();
            }
            return View(connector);
        }

        // POST: Connectors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Connector connector)
        {
            using (EVModelEntities entities = new EVModelEntities())
            {
                Connector updatedConnector = (from c in entities.Connectors
                                            where c.Id == connector.Id
                                            select c).FirstOrDefault();
                updatedConnector.DeleteDate = DateTime.UtcNow;
                updatedConnector.isDeleted = true;
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
