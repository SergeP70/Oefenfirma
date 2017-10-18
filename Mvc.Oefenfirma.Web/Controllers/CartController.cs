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

        //public ViewResult Checkout(int? userId)
        //{
        //    User user = db.Users.FirstOrDefault(u => u.UserId == userId);
        //    return View(user);
        //}

        // GET: Cart/Checkout
        public ViewResult Checkout(int? userId, ShoppingCart cart)
        {
            User user = db.Users.FirstOrDefault(u => u.UserId == userId);

            CartIndexVM cartIndexVM = new CartIndexVM
            {
                Cart = cart,
                ReturnUrl = null,
                User = db.Users.Where(u => u.UserId == userId).FirstOrDefault()
            };

            return View(cartIndexVM);
        }

        // POST: Cart/Checkout
        // Ontvang de gegevens uit de form van de View en 
        // sla ze op in de database Orders & OrderDetails
        // en stuur ze door per mail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CartIndexVM model)
        {
            User user = new User();

            if (ModelState.IsValid)
            {
                // Mapping ViewModel => entity
                //user.UserName = model.UserName;
                //user.UserPassword = model.UserPassword;
                //user.UserName = model.UserName;
                //user.UserEmail = model.UserEmail;
                //user.Birthday = model.Birthday;
                //user.UserLastName = model.UserLastName;
                //user.UserFirstName = model.UserFirstName;
                //user.UserAddress = model.UserAddress;
                //user.UserPost = model.UserPost;
                //user.UserGemeente = model.UserGemeente;
                //user.UserPhone = model.UserPhone;
                // Aanmaak Hash-paswoord en Role als klant:
                //user.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "md5");
                //Role userRole = db.Roles.FirstOrDefault(r => r.RoleName == "Klant");
                //user.Roles.Add(userRole);
                //db.Users.Add(user);
                //db.SaveChanges();

                // Aanmaak van de email 
                //string body = "<p>Beste {0},</br></p><p>Bedankt voor uw registratieaanvraag! U kan meteen aan de slag.</p><p><u>Gegevens:</u></br></p><p>Naam: {0} {1}.</p><p>TEL / GSM: {2} </p><p>Email: {3}.</p><p>Adres: {4}</p><p></br>Met vriendelijke groeten,</br></p><p>Uw webmaster</p>";
                //var message = new MailMessage();
                //message.To.Add(new MailAddress("sergepille@hotmail.com"));
                //message.To.Add(new MailAddress("serge.pille@telenet.be"));
                //message.To.Add(new MailAddress("docs.ivo@gmail.com"));
                //message.Subject = string.Format("Nieuwe registratie-aanvraag van website Ivo Bytes");
                //message.Body = string.Format(body, user.UserFirstName, user.UserLastName, user.UserPhone, user.UserEmail, user.UserAddress);
                //message.IsBodyHtml = true;

                // Versturen van de mail via smtpClient => configuratie in web.config
                //SmtpClient smtp = new SmtpClient();
                //smtp.Send(message);

                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer mail gestuurd werd.
                //TempData["Success"] = "Bedankt voor uw registratie! We nemen snel contact met u op.";

                return RedirectToAction("Index", "Home");
            }
            // Modelstate not valid? See errors:
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View();
        }


    }
}