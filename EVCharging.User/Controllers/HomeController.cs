
using EVCharging.Data;
using EVCharging.User.Models;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.SqlClient;

namespace EVCharging.User.Controllers
{
        public class HomeController : Controller
        {
           

            EVModelEntities db = new EVModelEntities();


            // GET: User
            public ActionResult Index()
            {
                return View();

            }

            public ActionResult Login()
            {
             
                return View();

            }


        [HttpPost]
        public ActionResult Login(Data.Customer s)
        {
            var User = db.Customers.Where(model => model.EmailAddress == s.EmailAddress && model.Password == s.Password ).FirstOrDefault();
        
            if (User != null)
            {
                if (User.RoleId == "User" || User.RoleId== null)
                {


                    
                    Session["UserEmail"] = s.EmailAddress.ToString();
                 










                    TempData["loginstatus"] = "<script>alert('login succefully !!')</script>";

                    return RedirectToAction("list", "Display");

                }

                else
                {
                    Session["UserEmail"] = s.EmailAddress.ToString();

                    return RedirectToAction("Admin", "Home");

                }
            }
            return View();
               
            }
        public ActionResult Admin()
        {
            return View();

        }


        // Get Register
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register([Bind(Exclude = "IsEmailVerify,ActivationCode,IsEmailverify,resetPasswordCode,ResetCode")] Customer c)
        {
            if (ModelState.IsValid == true)
            {







                var isEmailAlreadyExists = db.Customers.Any(x => x.EmailAddress == c.EmailAddress);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("EmailAddress", "User with this email already exists");
                    return View(c);
                }
                else
                {
                    c.RoleId = "User";
                    db.Customers.Add(c);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {

                        TempData["register"] = "<script>alert('register succefully !!')</script>";
                        ModelState.Clear();

                        return RedirectToAction("Login");
                    }

                    else
                    {

                        TempData["Noregister"] = "<script>alert('not register succefully !!')</script>";
                    }

                }
            }
            return View();

        }

        public ActionResult Roles()
        {
            var role = db.Roles.ToList();
            ViewBag.Role = role;

            return View();
        }
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult About(Staticpage s)
        {
           return View(db.Staticpages.ToList());
        }

        public ActionResult Contact()
        {
            var cont = db.Staticpages.ToList();


            return View(cont);
        }
         public ActionResult FAQ()
        {
            var cont = db.Staticpages.ToList();


            return View(cont);
        }





    }
}


        

