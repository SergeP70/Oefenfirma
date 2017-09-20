using Mvc.OefenfirmaCMS.Lib.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mvc.Oefenfirma.Web.Controllers
{
    public class CatController : Controller
    {
        private OefenfirmaContext db = new OefenfirmaContext();

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = db.Categories
                                    .Where(c => c.ParentCat != null)
                                    .Select(c => c.CatName)
                                    .Distinct()
                                    .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}