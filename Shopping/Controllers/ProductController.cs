using Shopping.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
        private ShopEntities db = new ShopEntities();

        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: Admin/AdminProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = db.Product.Find(id);
            CartItem cartItem = new CartItem();
            cartItem.productId = id;
            cartItem.quantity = quantity;
            cartItem.price = product.price;
            cartItem.sessionId = int.Parse(Session["userid"].ToString());
            if (cartItem != null)
            {
                db.CartItem.Add(cartItem);
                db.SaveChanges();
                return Redirect("Index");
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
