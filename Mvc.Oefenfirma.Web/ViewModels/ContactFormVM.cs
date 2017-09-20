using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Oefenfirma.Web.ViewModels
{
    public class ContactFormVM
    {
        [Display(Name = "Familienaam")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 2 en 30 karakters lang zijn", MinimumLength = 2)]
        public string RelName { get; set; }

        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 2 en 30 karakters lang zijn", MinimumLength = 2)]
        public string RelFirstName { get; set; }

        [Display(Name = "TEL/GSM")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+[0-9@\s]{2,3})?[0-9]{3,4}([\/\-@\s])?[0-9]{6}$", ErrorMessage = "Ingegeven nummer is niet geldig")]
        [StringLength(32)]
        public string RelPhone { get; set; }

        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Gelieve het {0} in te vullen")]
        [StringLength(128, ErrorMessage = "Het {0} mag maximum 30 karakters lang zijn", MinimumLength = 5)]
        public string RelEmail { get; set; }

        [Display(Name = "Uw bericht")]
        [Required(ErrorMessage = "Gelieve {0} in te vullen")]
        [StringLength(500, ErrorMessage = "{0} kan maximum 500 karakters lang zijn", MinimumLength = 5)]
        public string Message { get; set; }
    }
}