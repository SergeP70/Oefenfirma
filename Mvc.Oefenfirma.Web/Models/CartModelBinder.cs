using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Oefenfirma.Web.Models
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        // ControllerContext provides access to all the information that the controller class has, 
        //                   which includes details of the request from the client.
        // ModelBindingContext gives you information about the model object you are being asked to build 
        //                   and some tools for making the binding process easier
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            ShoppingCart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (ShoppingCart)controllerContext.HttpContext.Session[sessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new ShoppingCart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            return cart;
        }
    }
}