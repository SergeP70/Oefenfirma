using Mvc.Oefenfirma.Web.Models;
using Mvc.OefenfirmaCMS.Lib.Data;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;


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
    }
}
