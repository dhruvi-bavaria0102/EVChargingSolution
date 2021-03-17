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
    public class ServicesController : Controller
    {
        private EVModelEntities db = new EVModelEntities();

        // GET: Services
        public ActionResult Index()
        {
           
            return View(db.Services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceID,ServiceName,ServiceDescription,Accepted,InsertDate,UpdateDate,DeleteDate,isDeleted")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.ServiceStatus = "Pending";
                service.InsertDate = DateTime.UtcNow;
                db.Services.Add(service);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                }
                return RedirectToAction("Index");
            }
           
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceID,ServiceName,ServiceDescription,Accepted,InsertDate,UpdateDate,DeleteDate,isDeleted,ServiceStatus")] Service service)
        {
            if (ModelState.IsValid)
            {
            
                service.UpdateDate = DateTime.UtcNow;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Service service)
        //{
        //    using (EVModelEntities entities = new EVModelEntities())
        //    {
        //        Service updatedService = (from c in entities.Services
        //                                  where c.ServiceID == service.ServiceID
        //                                  select c).FirstOrDefault();
        //        //  updatedService.DeleteDate = DateTime.UtcNow;
        //        // updatedService.isDeleted = true;
        //        entities.SaveChanges();
        //    }

        //    return RedirectToAction("Index");
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service staticpage = db.Services.Find(id);
            db.Services.Remove(staticpage);
            db.SaveChanges();
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
