using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc.OefenfirmaCMS.Lib.Data;
using Mvc.OefenfirmaCMS.Lib.Entities;
using Mvc.Oefenfirma.Web.Models;

namespace Mvc.Oefenfirma.Web.Controllers
{
    public class ProductController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();
        // Productlijst: Aantal producten per pagina:
        public int PageSize = 12;

        // GET: Product
        public ActionResult Index(string category, int page = 1)
        {
            ProductListIndexVM model = new ProductListIndexVM
            {
                // If category is not null, only those Product objects with a matching Category property are selected
                // Else: all products are selected
                ProductList = db.Products
                    .Where(p => category == null || p.Category.CatName == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                                db.Products.Count() :
                                db.Products.Where(e => e.Category.CatName == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Product product = db.Products.Include(p => p.Category).SingleOrDefault(e => e.ProductId == id);
            
            if (product == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(product);
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
