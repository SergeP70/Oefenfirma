using Mvc.Oefenfirma.Web.Models;
using Mvc.OefenfirmaCMS.Lib.Data;
using Mvc.OefenfirmaCMS.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc.Oefenfirma.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            OefenfirmaContext c = new OefenfirmaContext();
            //List<Product> vmProducts = c.Products.ToList();
            List<Product> vmProducts = c.Products.Where(p => p.Spotlight == true).ToList();
            //Product product = db.Products.Include(p => p.Category).SingleOrDefault(e => e.ProductId == id);

            //Creating Hash Password for Users
            //List<User> users = c.Users.ToList();
            //foreach (var u in users)
            //{
            //    u.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(u.UserPassword, "md5");

            //}
            //c.SaveChanges();

            return View(vmProducts);
        }
    }
}