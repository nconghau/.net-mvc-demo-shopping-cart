using Shopping.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
                var cartItem = db.CartItem
                    .Include(c => c.Account)
                    .Include(c => c.Product).ToList();
                var myCart = new List<CartItem>();
                int session = int.Parse(Session["userid"].ToString());
                foreach (var item in cartItem)
                {

                    if (item.sessionId.Equals(session))
                    {
                        myCart.Add(item);
                    }
                }
                return View(myCart.ToList());
            }
            else
            {
                var cartItem = new List<CartItem>();
                return View(cartItem.ToList());
            }
        }

        [HttpPost]
        public ActionResult UpdateCart(FormCollection formCollection)
        {
            string[] productIds = formCollection.GetValues("productId");
            string[] quantitys = formCollection.GetValues("quantity");
            string[] cartIds = formCollection.GetValues("cartId");
            if (productIds != null && Session["userid"] != null)
                for (int i = 0; i < productIds.Length; i++)
                {
                    if (int.Parse(quantitys[i]) > 0)
                    {
                        var cartItem = db.CartItem.Find(int.Parse(cartIds[i]));
                        cartItem.quantity = int.Parse(quantitys[i]);
                        db.Entry(cartItem).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var cartItem = db.CartItem.Find(int.Parse(cartIds[i]));
                        db.CartItem.Remove(cartItem);
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
            return RedirectToAction("Index", "Product");
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
