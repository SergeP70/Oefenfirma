using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.OefenfirmaCMS.Lib.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name ="Gebruikersnaam")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 6 en 30 karakters lang zijn", MinimumLength = 6)]
        public string UserName { get; set; }

        [Display(Name ="Paswoord")]
        [Required(ErrorMessage = "Gelieve het {0} in te vullen")]
        [StringLength(12, ErrorMessage = "Het {0} moet tussen 5 en 30 karakters lang zijn", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public string PasswordHash { get; set; }

        [Display(Name = "Bevestig paswoord")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "De bevestiging komt niet overeen met het paswoord")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Gelieve het {0} in te vullen")]
        [StringLength(128, ErrorMessage = "Het {0} mag maximum 30 karakters lang zijn", MinimumLength = 5)]
        public string UserEmail { get; set; }

        [Display(Name ="TEL / GSM")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+[0-9@\s]{2,3})?[0-9]{3,4}([\/\-@\s])?[0-9]{6}$", ErrorMessage = "Ingegeven nummer is niet geldig")]
        [StringLength(32)]
        public string Phone { get; set; }

        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public ICollection<Role> Roles { get; set; }
        
        public int? RelationId { get; set; }
        public Relation Relation { get; set; }

    }
}
