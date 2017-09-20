using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.OefenfirmaCMS.Lib.Entities
{
    public class Category
    {
        [Key]
        [Display(Name ="Categorie")]
        public int CatId { get; set; }

        [Display(Name = "Categorie")]
        [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [StringLength(30, ErrorMessage = "De {0} moet tussen 4 en 30 karakters lang zijn", MinimumLength = 4)]
        public string CatName { get; set; }

        [Display(Name = "Omschrijving")]
        [StringLength(120, ErrorMessage = "De {0} moet tussen 5 en 120 karakters lang zijn", MinimumLength = 5)]
        public string Description { get; set; }

        // We maken ParentCat nullable
        [Display(Name = "Hoofd-Categorie")]
        // [Required(ErrorMessage = "Gelieve de {0} in te vullen")]
        [Range(0,50, ErrorMessage ="De categorie moet tussen 0 en 50 liggen")]
        [ForeignKey("ParentCatObj")]
        public int? ParentCat { get; set; }

        public Category ParentCatObj { get; set; }
        
        public List<Product> Products { get; set; }
    }
}
