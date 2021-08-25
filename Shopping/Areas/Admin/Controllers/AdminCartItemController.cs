using Shopping.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Shopping.Areas.Admin.Controllers
{
    public class AdminCartItemController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Admin/AdminCartItem
        public ActionResult Index()
        {
            var cartItem = db.CartItem.Include(c => c.Account).Include(c => c.Product);
            return View(cartItem.ToList());
        }

        // GET: Admin/AdminCartItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: Admin/AdminCartItem/Create
        public ActionResult Create()
        {
            ViewBag.sessionId = new SelectList(db.Account, "id", "username");
            ViewBag.productId = new SelectList(db.Product, "id", "name");
            return View();
        }

        // POST: Admin/AdminCartItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sessionId,productId,quantity,price")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CartItem.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sessionId = new SelectList(db.Account, "id", "username", cartItem.sessionId);
            ViewBag.productId = new SelectList(db.Product, "id", "name", cartItem.productId);
            return View(cartItem);
        }

        // GET: Admin/AdminCartItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.sessionId = new SelectList(db.Account, "id", "username", cartItem.sessionId);
            ViewBag.productId = new SelectList(db.Product, "id", "name", cartItem.productId);
            return View(cartItem);
        }

        // POST: Admin/AdminCartItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sessionId,productId,quantity,price")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sessionId = new SelectList(db.Account, "id", "username", cartItem.sessionId);
            ViewBag.productId = new SelectList(db.Product, "id", "name", cartItem.productId);
            return View(cartItem);
        }

        // GET: Admin/AdminCartItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: Admin/AdminCartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItem.Find(id);
            db.CartItem.Remove(cartItem);
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
