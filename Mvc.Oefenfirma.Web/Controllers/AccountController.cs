using Mvc.Oefenfirma.Web.Models;
using Mvc.Oefenfirma.Web.ViewModels;
using Mvc.OefenfirmaCMS.Lib.Data;
using Mvc.OefenfirmaCMS.Lib.Entities;
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
                OefenfirmaContext c = new OefenfirmaContext();
                List<User> users = c.Users.ToList();

                // geldige credentials??
                // Zoek of de UserName voorkomt in de OefenfirmaContext.Users
                // Indien ja: controleer zijn paswoord
                int index = users.FindIndex(f => f.UserName.ToUpper() == model.UserName.ToUpper());
                if ((index >= 0) && (model.Password == users[index].UserPassword))
                {
                    //inloggen door een authenticatie cookie te plaatsen op client, met de UserName
                    FormsAuthentication.SetAuthCookie(users[index].UserName, model.RememberMe);

                    //redirect naar homepage
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
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
            ContactFormVM model = new ContactFormVM();
            return View(model);
        }

        // POST: Account/Register
        // Ontvang de gegevens uit de form van de View en stuur ze door per mail
        // de naam vd variabelen moeten identiek zijn aan die in de form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ContactFormVM model)
        {
            // Modelstate not valid? See errors:
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                // Aanmaak van de email 
                string body = "<p>Beste,</br></p><p>U kreeg een nieuwe registratieaanvraag vanwege {0} {1}.</br></p><p>TEL / GSM: {2} en Email: {3}.</p><p><u>Boodschap:</u></br></p><p>{4}</p><p></br>Met vriendelijke groeten,</br></p><p>Uw webmaster</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("sergepille@hotmail.com"));
                message.To.Add(new MailAddress("serge.pille@telenet.be"));
                message.To.Add(new MailAddress("docs.ivo@gmail.com"));
                message.Subject = string.Format("Nieuwe registratie-aanvraag van website Ivo Bytes");
                message.Body = string.Format(body, model.RelFirstName, model.RelName, model.RelPhone, model.RelEmail, model.Message);
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