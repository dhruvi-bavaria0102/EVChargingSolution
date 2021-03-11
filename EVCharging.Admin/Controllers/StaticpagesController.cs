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
    public class StaticpagesController : Controller
    {
        private EVModelEntities db = new EVModelEntities();

        // GET: Staticpages
        public ActionResult Index()
        {
            return View(db.Staticpages.ToList());
        }

        // GET: Staticpages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staticpage staticpage = db.Staticpages.Find(id);
            if (staticpage == null)
            {
                return HttpNotFound();
            }
            return View(staticpage);
        }

        // GET: Staticpages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staticpages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
       
        public ActionResult Create([Bind(Include = "StaticPageId,StaticPageName,StaticPageContent")] Staticpage staticpage)
        {
            if (ModelState.IsValid)
            {
                db.Staticpages.Add(staticpage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staticpage);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staticpage staticpage = db.Staticpages.Find(id);
            if (staticpage == null)
            {
                return HttpNotFound();
            }
            return View(staticpage);
        }

        [HttpPost]
        [ValidateInput(false)]
       
        public ActionResult Edit(Staticpage staticpage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staticpage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staticpage);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staticpage staticpage = db.Staticpages.Find(id);
            if (staticpage == null)
            {
                return HttpNotFound();
            }
            return View(staticpage);
        }

        // POST: CKeditors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staticpage staticpage = db.Staticpages.Find(id);
            db.Staticpages.Remove(staticpage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
