using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shopping.Models;

namespace Shopping.Areas.Admin.Controllers
{
    public class AdminCartItemController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Admin/AdminCartItem
        public ActionResult Index()
        {
            var cart_Item = db.Cart_Item.Include(c => c.Account).Include(c => c.Product);
            return View(cart_Item.ToList());
        }

        // GET: Admin/AdminCartItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            if (cart_Item == null)
            {
                return HttpNotFound();
            }
            return View(cart_Item);
        }

        // GET: Admin/AdminCartItem/Create
        public ActionResult Create()
        {
            ViewBag.session_id = new SelectList(db.Accounts, "id", "username");
            ViewBag.product_id = new SelectList(db.Products, "id", "name");
            return View();
        }

        // POST: Admin/AdminCartItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,session_id,product_id,quantity,price")] Cart_Item cart_Item)
        {
            if (ModelState.IsValid)
            {
                db.Cart_Item.Add(cart_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.session_id = new SelectList(db.Accounts, "id", "username", cart_Item.session_id);
            ViewBag.product_id = new SelectList(db.Products, "id", "name", cart_Item.product_id);
            return View(cart_Item);
        }

        // GET: Admin/AdminCartItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            if (cart_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.session_id = new SelectList(db.Accounts, "id", "username", cart_Item.session_id);
            ViewBag.product_id = new SelectList(db.Products, "id", "name", cart_Item.product_id);
            return View(cart_Item);
        }

        // POST: Admin/AdminCartItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,session_id,product_id,quantity,price")] Cart_Item cart_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.session_id = new SelectList(db.Accounts, "id", "username", cart_Item.session_id);
            ViewBag.product_id = new SelectList(db.Products, "id", "name", cart_Item.product_id);
            return View(cart_Item);
        }

        // GET: Admin/AdminCartItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            if (cart_Item == null)
            {
                return HttpNotFound();
            }
            return View(cart_Item);
        }

        // POST: Admin/AdminCartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            db.Cart_Item.Remove(cart_Item);
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
