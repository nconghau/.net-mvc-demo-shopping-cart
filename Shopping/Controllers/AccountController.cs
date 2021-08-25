using Shopping.Models;
using System.Linq;
using System.Web.Mvc;

namespace Shopping.Controllers
{
    public class AccountController : Controller
    {
        private ShopEntities db = new ShopEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var account = db.Account.FirstOrDefault(x => x.username == username);
            if (account != null)
            {
                if (account.password.Equals(password))
                {
                    Session["userid"] = account.id;
                    return RedirectToAction("Index", "Product");
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
