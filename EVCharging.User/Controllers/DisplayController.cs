using EVCharging.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVCharging.User.Controllers
{
    public class DisplayController : Controller
    {
        // GET: View
        EVModelEntities db = new EVModelEntities();


        public ActionResult list()
        {
                return View();
            
           
        }

        public ActionResult Index()
        {
            
                
                return View();
            
        }
        [HttpPost]
        public ActionResult Index(string Password, string newPassword, string ConfirmPassword)
        {
            Customer objlogin = new Customer();

            string EmailAddress =Session["UserEmail"].ToString();
            var login = db.Customers.Where(u => u.EmailAddress.Equals(EmailAddress)).FirstOrDefault();
            Console.WriteLine(login);

            if (login.Password == Password)
            {
               
                    if (ConfirmPassword == newPassword)
                    {

                        login.ConfirmPassword = ConfirmPassword;
                        login.Password = newPassword;
                        db.Entry(login).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch(DbEntityValidationException e)
                    {
                        Console.WriteLine(e);
                    }
                        TempData["msg"] = "<script>alert('Password has been changed successfully !!!');</script>";
                    return RedirectToAction("Login", "Home");

                }

                    else
                    {
                        TempData["msg"] = "<script>alert('New password not match !!! Please check');</script>";
                    }
                
               
            }

            else
            {
                TempData["msg"] = "<script>alert('Old password not match !!! Please check entered old password');</script>";
            }

            return View();
        }


        public ActionResult UpdateProfile()
        {
            string EmailAddress = Session["UserEmail"].ToString();
            var pro = db.Customers.Where(u => u.EmailAddress.Equals(EmailAddress)).FirstOrDefault();
            return View(pro);

        }

        [HttpPost]


        public ActionResult UpdateProfile(Customer c, string ConfirmPassword)

        {
            string EmailAddress = Session["UserEmail"].ToString();



            if (ModelState.IsValid)
            {

                var pro = db.Customers.Where(u => u.EmailAddress.Equals(EmailAddress)).FirstOrDefault();






                
                    db.Customers.Remove(pro);
                    db.Customers.Add(c);
                    db.SaveChanges();

                
                
                return RedirectToAction("list", "Display");

            }
            else
            {
                TempData["msg"] = "<script>alert('not update');</script>";
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Login", "Home");
        }
        public ActionResult userLocation()
        {
            return View();
           
        }
        public JsonResult GetMapMarker()
        {
          var ListOfAddress = db.Select_Location().ToList();
          return Json(ListOfAddress, JsonRequestBehavior.AllowGet);
        }

    }

}
