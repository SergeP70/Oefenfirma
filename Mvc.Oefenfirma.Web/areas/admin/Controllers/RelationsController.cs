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
    public class RelationsController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        // GET: Admin/Relations
        public ActionResult Index()
        {
            //var relations = db.Relations.Include(r => r.Relationship);
            return View(db.Relations.ToList());
        }

        // GET: Admin/Relations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Relation relation = db.Relations.Find(id);
            // Relation relation = db.Relations.Include(r => r.Relationship).SingleOrDefault(e => e.RelationId == id);
            Relation relation = db.Relations.SingleOrDefault(e => e.RelationId == id);
            if (relation == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(relation);
        }

        // GET: Admin/Relations/Create
        public ActionResult Create()
        {
            //ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "Description");
            return View();
        }

        // POST: Admin/Relations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RelId,RelName,RelFirstName,RelAdress,RelPost,RelGemeente,RelPhone,RelEmail,RelationshipId")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                db.Relations.Add(relation);
                db.SaveChanges();
                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer relatie aangemaakt werd.
                TempData["Success"] = "De relatie werd succesvol aangemaakt";
                return RedirectToAction("Index");
            }

            //ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "Description", relation.RelationshipId);
            return View(relation);
        }

        // GET: Admin/Relations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            //ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "Description", relation.RelationshipId);
            return View(relation);
        }

        // POST: Admin/Relations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RelId,RelName,RelFirstName,RelAdress,RelPost,RelGemeente,RelPhone,RelEmail,RelationshipId")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relation).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "De relatie werd succesvol gewijzigd";
                return RedirectToAction("Index");
            }
            //ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "Description", relation.RelationshipId);
            return View(relation);
        }

        // GET: Admin/Relations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Relation relation = db.Relations.Include(r => r.Relationship).SingleOrDefault(e => e.RelationId == id);
            Relation relation = db.Relations.SingleOrDefault(e => e.RelationId == id);
            if (relation == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(relation);
        }

        // POST: Admin/Relations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relation relation = db.Relations.Find(id);
            db.Relations.Remove(relation);
            db.SaveChanges();
            TempData["Success"] = "De relatie werd succesvol verwijderd";
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
