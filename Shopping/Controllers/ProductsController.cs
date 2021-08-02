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
    public class ProductsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        public ActionResult Index2()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/AdminProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = db.Products.Find(id);
            Cart_Item cart_Item = new Cart_Item();
            cart_Item.product_id = id;
            cart_Item.quantity = quantity;
            cart_Item.price = product.price;
            cart_Item.session_id = int.Parse(Session["userid"].ToString());
            if(cart_Item != null)
            {
                db.Cart_Item.Add(cart_Item);
                db.SaveChanges();
                return Redirect("Index2");
            }
            return View();

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
