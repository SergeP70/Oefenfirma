using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Oefenfirma.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Gelieve je gebruikersnaam in te geven")]
        [Display(Name="Gebruikersnaam")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Gelieve je wachtwoord in te geven")]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}