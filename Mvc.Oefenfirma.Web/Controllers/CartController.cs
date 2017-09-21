using Mvc.Oefenfirma.Web.Models;
using Mvc.Oefenfirma.Web.ViewModels;
using Mvc.OefenfirmaCMS.Lib.Data;
using Mvc.OefenfirmaCMS.Lib.Entities;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace Mvc.Oefenfirma.Web.Controllers
{
    public class CartController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        public ViewResult Index(ShoppingCart cart, string returnUrl)
        {
            // De naam van de ingelogde gebruiker: System.Web.HttpContext.Current.User.Identity.Name
            return View(new CartIndexVM
            {
                Cart = cart,
                ReturnUrl = returnUrl,
                User = db.Users.Where(u => u.UserName == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault()
            });
        }

        [HttpPost]
        public RedirectToRouteResult AddToCart(ShoppingCart cart, int productId, string returnUrl)
        {
            Product product = db.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(ShoppingCart cart, int productId, string returnUrl)
        {
            Product product = db.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(ShoppingCart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(int? userId)
        {
            User user = db.Users.FirstOrDefault(u => u.UserId == userId);
            return View(user);
        }
    }
}