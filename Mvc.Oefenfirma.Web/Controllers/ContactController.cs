using Mvc.Oefenfirma.Web.ViewModels;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace Mvc.Oefenfirma.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Contact
        // Ontvang de gegevens uit de form van de View en stuur ze door per mail
        // de naam vd variabelen moeten identiek zijn aan die in de form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactFormVM model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                // Aanmaak van de email 
                string body = "<p>Beste,</br></p><p>U kreeg een nieuwe contactaanvraag vanwege {0} {1}.</br></p><p>TEL / GSM: {2} en Email: {3}.</p><p><u>Boodschap:</u></br></p><p>{4}</p><p></br>Met vriendelijke groeten,</br></p><p>Uw webmaster</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("sergepille@hotmail.com"));
                message.To.Add(new MailAddress("serge.pille@telenet.be"));
                message.To.Add(new MailAddress("docs.ivo@gmail.com"));
                message.Subject = string.Format("Nieuw contactformulier van website Ivo Bytes");
                message.Body = string.Format(body, model.RelFirstName, model.RelName, model.RelPhone, model.RelEmail, model.Message);
                message.IsBodyHtml = true;

                // Versturen van de mail via smtpClient => configuratie in web.config
                SmtpClient smtp = new SmtpClient();
                smtp.Send(message);

                // Gebruik van Tempdata om Indexpagina te voorzien met een melding wanneer mail gestuurd werd.
                TempData["Success"] = "Bedankt voor uw feedback! Deze werd succesvol doorgemaild.";

                return RedirectToAction("Index");

            }

            return View();

        }
    }
}