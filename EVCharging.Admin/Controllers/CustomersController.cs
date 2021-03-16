using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVCharging.Data;
using PagedList;

namespace EVCharging.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private EVModelEntities db = new EVModelEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customer = db.Customers.Include(c => c.BusinessUnit);
            return View(customer.ToList());


        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BusinessUnitID = new SelectList(db.BusinessUnits, "ID", "BusinessUnitName");
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName");
            return View();
        }

        // POST: Customers/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,BusinessUnitID,EmailAddress,streetAddress,PostalCode,City,Country,InvoiceStreetAddress,InvoicePostalCode,InvoiceCity,InvoiceCountry,Site,Telephone,RoleId,InsertDate,UpdateDate,DeleteDate,IsDeleted,Password,resetPasswordCode,IsEmailverify,ActivationCode,ConfirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.InsertDate = DateTime.UtcNow;
   
                db.Customers.Add(customer);
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

            ViewBag.BusinessUnitID = new SelectList(db.BusinessUnits, "ID", "BusinessUnitName", customer.BusinessUnitID);
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName", customer.RoleId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessUnitID = new SelectList(db.BusinessUnits, "ID", "BusinessUnitName", customer.BusinessUnitID);
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName", customer.RoleId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,BusinessUnitID,EmailAddress,streetAddress,PostalCode,City,Country,InvoiceStreetAddress,InvoicePostalCode,InvoiceCity,InvoiceCountry,Site,Telephone,RoleId,InsertDate,UpdateDate,DeleteDate,IsDeleted,Password,resetPasswordCode,IsEmailverify,ActivationCode,ConfirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
               
                customer.UpdateDate = DateTime.UtcNow;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            

            ViewBag.BusinessUnitID = new SelectList(db.BusinessUnits, "ID", "BusinessUnitName", customer.BusinessUnitID);
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName", customer.RoleId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
       
        public ActionResult Delete(Customer customer)
        {
            using (EVModelEntities entities = new EVModelEntities())
            {
                Customer updatedCustomer = (from c in entities.Customers
                                            where c.ID == customer.ID
                                            select c).FirstOrDefault();
                updatedCustomer.DeleteDate = DateTime.UtcNow;
                updatedCustomer.IsDeleted = true;
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
