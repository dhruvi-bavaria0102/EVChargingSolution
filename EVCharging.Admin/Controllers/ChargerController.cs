using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EVCharging.Data;

namespace EVCharging.Admin.Controllers
{
    public class ChargerController : Controller
    {
        EVModelEntities ev = new EVModelEntities();
        // GET: Charger
        public ActionResult Index()
        {
            return View(ev.Select_Charger().ToList());
        }

        // GET: Charger/Details/5
        public ActionResult Details(int id)
        { 
            return View();
        }

        // GET: Charger/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Charger/Create
        [HttpPost]
        public ActionResult Create(Charger charger)
        {
            ev.Insert_Charger(charger.LocationID, charger.Availability,charger.InsertDate,charger.UpdateDate,charger.DeleteDate,charger.IsDeleted);
            return RedirectToAction("Index");
        }

        // GET: Charger/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Edit");
        }

        // POST: Charger/Edit/5
        [HttpPost]
        public ActionResult Edit(Charger charger)
        {
            ev.Update_Charger(charger.LocationID, charger.Availability, charger.InsertDate, charger.UpdateDate, charger.DeleteDate, charger.IsDeleted);
           return RedirectToAction("Index");
        }

        // GET: Charger/Delete/5
        public ActionResult Delete(Charger charger)
        {
            ev.Delete_Charger(charger.LocationID, charger.Availability, charger.InsertDate, charger.UpdateDate, charger.DeleteDate, charger.IsDeleted);
            return RedirectToAction("Index");
        }

        
    }
}
