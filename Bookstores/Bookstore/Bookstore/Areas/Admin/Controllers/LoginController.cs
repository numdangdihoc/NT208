using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bookstore.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string user, string password)
        {

            //check input
            if (user.ToLower() == "admin12345" && password == "12345")
            {
                Session["user"] = "admin12345";
                return RedirectToAction("Dashboard","Login"); ;
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut(string user, string password)
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}