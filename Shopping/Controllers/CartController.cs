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
    public class CartController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Cart
        public ActionResult Index()
        {

            if (Session["userid"] != null)
            {
                var cart_Item = db.Cart_Item
                    .Include(c => c.Account)
                    .Include(c => c.Product).ToList();
                var my_cart = new List<Cart_Item>();
                int session = int.Parse(Session["userid"].ToString());
                foreach (var item in cart_Item)
                {

                    if (item.session_id.Equals(session))
                    {
                        my_cart.Add(item);
                    }
                }

                return View(my_cart.ToList());
            }
            else
            {
                var cart_Item = new List<Cart_Item>();
                return View(cart_Item.ToList());
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCart(FormCollection formCollection)
        {
            string[] product_ids = formCollection.GetValues("product_id");
            string[] quantitys = formCollection.GetValues("quantity");
            string[] cart_ids = formCollection.GetValues("cart_id");
            if (product_ids != null && Session["userid"] != null)
                for (int i = 0; i < product_ids.Length; i++)
                {
                    if (int.Parse(quantitys[i]) > 0)
                    {
                        var cartItem = db.Cart_Item.Find(int.Parse(cart_ids[i]));
                        cartItem.quantity = int.Parse(quantitys[i]);
                        db.Entry(cartItem).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var cartItem = db.Cart_Item.Find(int.Parse(cart_ids[i]));
                        db.Cart_Item.Remove(cartItem);
                        db.SaveChanges();

                    }
                }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult ClearCart()
        {
            //GetShoppingCart();
            //ShoppingCart.Clear();
            //Session["ShoppingCart"] = ShoppingCart;
            //return RedirectToAction("Index");
            return RedirectToAction("Index2", "Products");
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
