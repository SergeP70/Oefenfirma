using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.OefenfirmaCMS.Lib.Entities
{
    public class Relation
    {
        [Key]
        public int RelationId { get; set; }

        [Display(Name = "Familienaam")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 2 en 30 karakters lang zijn", MinimumLength = 2)]
        public string RelName { get; set; }

        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 2 en 30 karakters lang zijn", MinimumLength = 2)]
        public string RelFirstName { get; set; }

        [Display(Name = "Adres")]
        //[Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(100, ErrorMessage = "Het {0} mag maximum 100 karakters lang zijn")]
        public string RelAdress { get; set; }

        [Display(Name = "Postnummer")]
        [Range(1000,9999, ErrorMessage ="Postnummer ligt tussen 1000 en 9999")]
        [RegularExpression(@"^[1-9]{1}[0-9]{3}$", ErrorMessage = "Postnummer is niet geldig")]
        public int RelPost{ get; set; }

        [Display(Name = "Gemeente")]
        [StringLength(50, ErrorMessage = "De {0} mag maximum 50 karakters lang zijn")]
        public string RelGemeente { get; set; }

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

        [Display(Name = "Verwantschap")]
        public string RelationshipId { get; set; }
        [Display(Name = "Verwantschap")]
        public Relationship Relationship { get; set; }

        // Link tussen Relations en Users 
        // ALS een User == klant kunnen we desbetreffende info opvragen. 
        // public int? UserId { get; set; }
        public List<User> Users { get; set; }
        
    }
}
