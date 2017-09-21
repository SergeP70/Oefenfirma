//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Mvc.OefenfirmaCMS.Lib.Data;
//using Mvc.OefenfirmaCMS.Lib.Entities;

//namespace Mvc.Oefenfirma.Web.Areas.Admin.Controllers
//{
//    [Authorize()]
//    public class RelationshipsController : Controller
//    {
//        private OefenfirmaContext db = new OefenfirmaContext();

//        // GET: Admin/Relationships
//        public ActionResult Index()
//        {
//            return View(db.Relationships.ToList());
//        }

//        // GET: Admin/Relationships/Details/5
//        public ActionResult Details(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Relationship relationship = db.Relationships.Find(id);
//            if (relationship == null)
//            {
//                TempData["Success"] = "Nope";
//                return RedirectToAction("Index");
//            }
//            return View(relationship);
//        }

//        // GET: Admin/Relationships/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Admin/Relationships/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "RelationshipId,Description")] Relationship relationship)
//        {
//            if (ModelState.IsValid)
//            {
//                var checkExists = db.Relationships.Find(relationship.RelationshipId);

//                if (checkExists == null)
//                {
//                db.Relationships.Add(relationship);
//                db.SaveChanges();
//                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer verwantschap aangemaakt werd.
//                TempData["Success"] = "Succes";
//                return RedirectToAction("Index");
//                }
//                else
//                {
//                    TempData["Success"] = "Allready";
//                    return RedirectToAction("Index");
//                }
//            }

//            return View(relationship);
//        }

//        // GET: Admin/Relationships/Edit/5
//        public ActionResult Edit(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Relationship relationship = db.Relationships.Find(id);
//            if (relationship == null)
//            {
//                TempData["Success"] = "Nope";
//                return RedirectToAction("Index");
//            }
//            return View(relationship);
//        }

//        // POST: Admin/Relationships/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "RelationshipId,Description")] Relationship relationship)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(relationship).State = EntityState.Modified;
//                db.SaveChanges();
//                TempData["Success"] = "Wijzig";
//                return RedirectToAction("Index");
//            }
//            return View(relationship);
//        }

//        // GET: Admin/Relationships/Delete/5
//        public ActionResult Delete(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Relationship relationship = db.Relationships.Find(id);
//            if (relationship == null)
//            {
//                TempData["Success"] = "Nope";
//                return RedirectToAction("Index");
//            }
//            return View(relationship);
//        }

//        // POST: Admin/Relationships/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(string id)
//        {
//            //Relationship relationship = db.Relationships.Find(id);
//            IEnumerable<Relation> relations = db.Relations.ToList();
//            // if (relations.Where(r => r.RelationshipId == relationship.RelationshipId).Count() > 0)
//            if (relations.Where(r => r.RelationshipId == relationship.RelationshipId).Count() > 0)
//            {
//                TempData["Success"] = "Kannie";
//            }
//            else
//            {
//            db.Relationships.Remove(relationship);
//            db.SaveChanges();
//            TempData["Success"] = "Weg";
//            }
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
