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
using Mvc.Oefenfirma.Web.ViewModels;
using System.Threading;

namespace Mvc.Oefenfirma.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="Administrator")]
    public class UsersController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Roles).ToList() ;
            return View(users);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Include(u => u.Roles).SingleOrDefault(u => u.UserId == id);
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
            /* When we edit an User, we desire to show a ListBox with the list of available Roles 
             * where the selected are the ones associated to the concerning user. 
             * we can change the selection or other data and submit back to the controller to persist the data*/

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserRolesVM userRolesVM = new UserRolesVM
            {
                User = db.Users.Include(u => u.Roles).FirstOrDefault(u => u.UserId == id)
            };

            if (userRolesVM.User == null)
            {
                TempData["Success"] = "Nope";
                return RedirectToAction("Index");
            }
            var allUserRolesList = db.Roles.ToList();

            userRolesVM.AllUserRoles = allUserRolesList.Select(o => new SelectListItem
            {
                Text = o.RoleName,
                Value = o.RoleId.ToString()
            });

            return View(userRolesVM);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRolesVM userRolesVM)
        {
            if (ModelState.IsValid)
            {
                User userToUpdate = db.Users.FirstOrDefault(u => u.UserId == userRolesVM.User.UserId);
                
                // map the properties we **actually** want to update:
                userToUpdate.UserName = userRolesVM.User.UserName;
                userToUpdate.UserPassword = userRolesVM.User.UserPassword;
                userToUpdate.UserEmail = userRolesVM.User.UserEmail;
                userToUpdate.Birthday= userRolesVM.User.Birthday;
                userToUpdate.UserLastName= userRolesVM.User.UserLastName;
                userToUpdate.UserFirstName = userRolesVM.User.UserFirstName;
                userToUpdate.UserAddress = userRolesVM.User.UserAddress;
                userToUpdate.UserPost = userRolesVM.User.UserPost;
                userToUpdate.UserGemeente = userRolesVM.User.UserGemeente;
                userToUpdate.UserName = userRolesVM.User.UserName;
                userToUpdate.UserPhone = userRolesVM.User.UserPhone;
                userToUpdate.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(userRolesVM.User.UserPassword, "md5");


                var newRoles = db.Roles
                                .Where(r => userRolesVM.SelectedUserRoles.Contains(r.RoleId))
                                .ToList();
                var updatedRoles = new HashSet<int>(userRolesVM.SelectedUserRoles);
                foreach (Role role in db.Roles)
                {
                    if (!updatedRoles.Contains(role.RoleId))
                    {
                        userToUpdate.Roles.Remove(role);
                    }
                    else
                    {
                        userToUpdate.Roles.Add(role);
                    }
                }

                db.Entry(userToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "De gebruiker werd succesvol gewijzigd";
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(userRolesVM);
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
