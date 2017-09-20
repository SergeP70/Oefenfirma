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

namespace Mvc.Oefenfirma.Web.Areas.Admin.Controllers
{
    [Authorize()]
    public class CategoriesController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            // Toevoegen van de titels van de categorieên, gesorteerd op categorie
            var results = View(db.Categories
                 .Include(e => e.ParentCatObj)
                 .OrderBy(m => m.ParentCat)
                 .ToList());

            return results;

        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var results = View(db.Categories
            //    .Include(e => e.ParentCatObj)
            //    .ToList());

            //Category category = db.Categories.Find(id);
            Category category= db.Categories
                .Include(c => c.ParentCatObj)
                .SingleOrDefault(e => e.CatId == id);

            if (category == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            //Voor de dropdownlist is er een lijst van enkel de hoofdcategorieën
            ViewBag.ParentCat = new SelectList(db.Categories
                .Where(c => c.ParentCat == null),
                "CatId", "CatName");

            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatId,CatName,Description,ParentCat")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer gebruiker aangemaakt werd.
                TempData["Success"] = "Succes";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Include(c => c.ParentCatObj).SingleOrDefault(e => e.CatId ==  id);
            if (category == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            //ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "Description", relation.RelationshipId);
            //Voor de dropdownlist is er een lijst van enkel de hoofdcategorieën
            ViewBag.ParentCat = new SelectList(db.Categories
                .Where(c => c.ParentCat == null),
                "CatId", "CatName");

            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatId,CatName,Description,ParentCat")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Wijzig";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories
                .Include(c => c.ParentCatObj)
                .SingleOrDefault(e => e.CatId == id);

            if (category == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);

            IEnumerable<Product> products = db.Products.ToList();
            if (products.Where(p => p.CatId == category.CatId).Count() > 0)
            {
                TempData["Success"] = "Kannie";
            }
            else
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                TempData["Success"] = "Weg";
            }
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
