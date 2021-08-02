using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class AccountsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var account = db.Accounts.FirstOrDefault(x => x.username == username);
            if (account != null)
            {
                if (account.password.Equals(password))
                {
                    Session["userid"] = account.id;
                    return RedirectToAction("Index2", "Products");
                }
            }
            return Redirect("Login");
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
