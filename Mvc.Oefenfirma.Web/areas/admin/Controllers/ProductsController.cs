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
using System.IO;

namespace Mvc.Oefenfirma.Web.Areas.Admin.Controllers
{
    [Authorize()]
    public class ProductsController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Include(p => p.Category).SingleOrDefault(e => e.ProductId == id);

            //Product product = db.Products.Find(id);
            if (product == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            //Voor de dropdownlist is er een lijst van enkel de sub-categorieën
            ViewBag.CatId = new SelectList(db.Categories
                .Where(c => c.ParentCat != null),
                "CatId", "CatName");

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductCode,ProductName,CatId,Description,Price,UrlImage,Spotlight")] Product product)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["UploadImage"];

                if ((file != null) && (file.ContentLength > 0))
                {
                    product.UrlImage = Path.Combine("~/Data/images", file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/Data/images"), file.FileName));
                }
                else
                {
                    product.UrlImage = "~/Data/images/no-image.png";
                }
                db.Products.Add(product);
                db.SaveChanges();
                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer product aangemaakt werd.
                TempData["Success"] = "Het product werd succesvol aangemaakt";
                return RedirectToAction("Index");
            }

            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }

            //Voor de dropdownlist is er een lijst van enkel de sub-categorieën
            ViewBag.CatId = new SelectList(db.Categories
                .Where(c => c.ParentCat != null),
                "CatId", "CatName", product.CatId);

            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductCode,ProductName,CatId,Description,Price,UrlImage,Spotlight")] Product product)
        {
            if (ModelState.IsValid)
            {
                // In het geval er geen afbeelding geselecteerd werd - omdat er bv. al een afbeelding stond
                // is product.UrlImage STEEDS NULL !!!  Maw. de originele UrlImage wordt overschreven door NULL
                // Daarom gebruik ik een tijdelijke variabele 'image' waar ik de UrlImage UIT DE DATABASE haal
                // en koppel aan het betreffende product.
                var image = db.Products
                    .AsNoTracking()
                    .Where(x => x.ProductId == product.ProductId)
                    .ToList();
                if (image == null)
                {
                    return new HttpNotFoundResult();
                }
                product.UrlImage = image[0].UrlImage;

                HttpPostedFileBase file = Request.Files["UploadImage"];
                // Indien wel een afbeelding geselecteerd werd, wordt de nieuwe UrlImage ingelezen:

                if ((file != null) && (file.ContentLength > 0))
                {
                    product.UrlImage = Path.Combine("~/Data/images", file.FileName);

                    file.SaveAs(Path.Combine(Server.MapPath("~/Data/images"), file.FileName));
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Het product werd succesvol gewijzigd";
                return RedirectToAction("Index");
            }
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            TempData["Success"] = "Het product werd succesvol verwijderd";
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
