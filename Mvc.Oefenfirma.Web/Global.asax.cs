using Mvc.Oefenfirma.Web.Models;
using Mvc.OefenfirmaCMS.Lib.Data;
using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Mvc.Oefenfirma.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //Database.SetInitializer<OefenfirmaContext>(new OefenfirmaDropCreateAndSeed());
            Database.SetInitializer<OefenfirmaContext>(new OefenfirmaDropCreateAndSeed());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(ShoppingCart), new CartModelBinder());
        }

        /* Het Authorize attribuut zoekt naar betreffende Roles van de geauthenticeerde gebruiker. 
         * Deze Roles bevinden zich normaal gezien in het User.Identity object. 
         * Om het Authorize attribuut te kunnen benutten moeten we er dus voor zorgen dat de roles daarin terecht komen.
         * Een ideale plaats om het User.Identity object te wijzigen alvorens de request naar de controller wordt gestuurd
         * is het event Application_AuthenticateRequest.*/

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Get Forms identity from current user
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    // Get Forms Ticket from Identity Object
                    FormsAuthenticationTicket ticket = id.Ticket;
                    // Retrieve stored user-data (our roles from db)
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(';');
                    // Create a new Generic Principal Instance and assign to Current User
                    HttpContext.Current.User = new GenericPrincipal(id, roles); 
                }
            }
        }
    }
}
