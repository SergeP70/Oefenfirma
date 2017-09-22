using Mvc.Oefenfirma.Web.Models;
using Mvc.Oefenfirma.Web.ViewModels;
using Mvc.OefenfirmaCMS.Lib.Data;
using Mvc.OefenfirmaCMS.Lib.Entities;
using Mvc.OefenfirmaCMS.Lib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc.Oefenfirma.Web.Controllers
{
    public class AccountController : Controller
    {
        UsersRepository usersRepository;
        OefenfirmaContext db;

        public AccountController()
        {
            db = new OefenfirmaContext();
            usersRepository = new UsersRepository(db); 
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // Get: Account/Login
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        // POST Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User loggedInUser = usersRepository.GetUserByUsernameAndPassword(model.UserName, model.Password);

                // geldige credentials??
                if (loggedInUser != null)
                {
                    // Geldige credentials, inloggen maar !!
                    // alle roles voor de huidige gebruiker ophalen
                    IEnumerable<string> roleNames = loggedInUser.Roles.Select<Role, string>(r => r.RoleName);
                    string rolesString = string.Join(";", roleNames.ToArray());

                    //We maken onze eigen ticket aan zodat ook de roles bewaard kunnen worden!
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        loggedInUser.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                        model.RememberMe,
                        rolesString);

                    //ticket encrypteren en in cookie zetten
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    //redirect naar homepage
                    if (roleNames.Contains("Administrator"))
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    //@Html.ActionLink("Admin Area", "Index", "Admin", new { area = "Admin" }, htmlAttributes: new { title = "Manage" })
                }
                else
                {
                    //ongeldige gebruikersnaam of wachtwoord
                    //voeg een foutboodschap toe (wanneer key == "", dan hoort deze niet bij een bepaalde property)
                    ModelState.AddModelError("", "De ingevoerde gebruikersnaam of wachtwoord is ongeldig");
                    //aanmeldingscherm opnieuw tonen
                    return View(model);
                }
            }
            else
            {
                //onvolledige gegevens, toon formulier opnieuw
                ModelState.AddModelError("", "Onvolledige gegevens, probeer opnieuw");
                return View(model);
            }
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        // Ontvang de gegevens uit de form van de View en stuur ze door per mail
        // de naam vd variabelen moeten identiek zijn aan die in de form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,UserName,UserPassword,ConfirmPassword,UserFirstName,UserLastName,UserAddress,UserPost,UserGemeente,UserEmail,UserPhone,Birthday")] User user)
        {
            // Modelstate not valid? See errors:
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                // Aanmaak Hash-paswoord en Role als klant:
                user.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "md5");
                Role userRole = db.Roles.FirstOrDefault(r => r.RoleName == "Klant");
                user.Roles.Add(userRole);
                db.Users.Add(user);
                db.SaveChanges();


                // Aanmaak van de email 
                string body = "<p>Beste {0},</br></p><p>Bedankt voor uw registratieaanvraag! U kan meteen aan de slag.</p><p><u>Gegevens:</u></br></p><p>Naam: {0} {1}.</p><p>TEL / GSM: {2} </p><p>Email: {3}.</p><p>Adres: {4}</p><p></br>Met vriendelijke groeten,</br></p><p>Uw webmaster</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("sergepille@hotmail.com"));
                message.To.Add(new MailAddress("serge.pille@telenet.be"));
                message.To.Add(new MailAddress("docs.ivo@gmail.com"));
                message.Subject = string.Format("Nieuwe registratie-aanvraag van website Ivo Bytes");
                message.Body = string.Format(body, user.UserFirstName, user.UserLastName, user.UserPhone, user.UserEmail, user.UserAddress);
                message.IsBodyHtml = true;

                // Versturen van de mail via smtpClient => configuratie in web.config
                SmtpClient smtp = new SmtpClient();
                smtp.Send(message);

                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer mail gestuurd werd.
                TempData["Success"] = "Bedankt voor uw registratie! We nemen snel contact met u op.";

                return RedirectToAction("Register");
            }

            return View();
        }




        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}