using Mvc.Oefenfirma.Web.Models;
using Mvc.OefenfirmaCMS.Lib.Entities;

namespace Mvc.Oefenfirma.Web.ViewModels
{
    public class CartIndexVM
    {
        public ShoppingCart Cart { get; set; }
        public string ReturnUrl { get; set; }
        
        public User User { get; set; }
    }
}