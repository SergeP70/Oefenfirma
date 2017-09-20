using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.OefenfirmaCMS.Lib.Entities
{
    public class Relationship
    {
        [Key, Required]
        [Display(Name = "ID")]
        public string RelationshipId { get; set; }

        [Display(Name = "Verwantschap")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(50, ErrorMessage = "De {0} mag maximum 50 karakters lang zijn")]
        public string Description { get; set; }

        public List<Relation> Relations { get; set; }

    }
}
