using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Oefenfirma.Web.ViewModels
{
    public class AccountRegisterVM
    {
        [Display(Name = "Gebruikersnaam")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 6 en 30 karakters lang zijn", MinimumLength = 6)]
        public string UserName { get; set; }

        [Display(Name = "Paswoord")]
        [Required(ErrorMessage = "Gelieve het {0} in te vullen")]
        [StringLength(12, ErrorMessage = "Het {0} moet tussen 5 en 30 karakters lang zijn", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "Bevestig paswoord")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "De bevestiging komt niet overeen met het paswoord")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Gelieve het {0} in te vullen")]
        [StringLength(128, ErrorMessage = "Het {0} mag maximum 30 karakters lang zijn", MinimumLength = 5)]
        public string UserEmail { get; set; }

        [Display(Name = "Familienaam")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 2 en 30 karakters lang zijn", MinimumLength = 2)]
        public string UserLastName { get; set; }

        [Display(Name = "Voornaam")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 2 en 30 karakters lang zijn", MinimumLength = 2)]
        public string UserFirstName { get; set; }

        [Display(Name = "Adres")]
        [StringLength(100, ErrorMessage = "Het {0} mag maximum 100 karakters lang zijn")]
        public string UserAddress { get; set; }

        [Display(Name = "Postnummer")]
        [RegularExpression(@"^[1-9]{1}[0-9]{3}$", ErrorMessage = "Postnummer is niet geldig")]
        [StringLength(4)]
        public string UserPost { get; set; }

        [Display(Name = "Gemeente")]
        [StringLength(50, ErrorMessage = "De {0} mag maximum 50 karakters lang zijn")]
        public string UserGemeente { get; set; }

        [Display(Name = "TEL/GSM")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+[0-9@\s]{2,3})?[0-9]{3,4}([\/\-@\s])?[0-9]{6}$", ErrorMessage = "Ingegeven nummer is niet geldig")]
        [StringLength(32)]
        public string UserPhone { get; set; }

        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

    }
}