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
using System.Web.Security;

namespace Mvc.Oefenfirma.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="Administrator")]
    public class UsersController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserPassword,ConfirmPassword,UserFirstName,UserLastName,UserAddress,UserPost,UserGemeente,UserEmail,UserPhone,Birthday")] User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "md5");
                db.Users.Add(user);
                db.SaveChanges();
                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer gebruiker aangemaakt werd.
                TempData["Success"] = "De gebruiker werd succesvol aangemaakt";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,UserPassword,UserEmail,UserLastName,UserFirstName,UserAddress,UserPost,UserGemeente,UserPhone,Birthday")] User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "md5");
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "De gebruiker werd succesvol gewijzigd";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            TempData["Success"] = "De gebruiker werd succesvol verwijderd";
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
