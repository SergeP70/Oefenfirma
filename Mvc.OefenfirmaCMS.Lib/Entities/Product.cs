using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.OefenfirmaCMS.Lib.Entities
{
    public class Product
    {
        [Key]
        [Display(Name = "ProductId")]
        public int ProductId { get; set; }

        [Display(Name = "Productcode")]
        [Required(ErrorMessage = "Gelieve het veld {0} in te vullen")]
        public string ProductCode { get; set; }

        [Display(Name = "Productnaam")]
        [StringLength(64, ErrorMessage = "Het veld {0} kan niet langer zijn dan 64 karakters")]
        [Required(ErrorMessage = "Gelieve het veld {0} in te vullen")]
        public string ProductName { get; set; }

        [Display(Name ="Categorie")]
        [Required(ErrorMessage = "Gelieve het veld {0} in te vullen")]
        public int CatId { get; set; }

        [Display(Name ="Omschrijving")]
        [Required(ErrorMessage = "Gelieve het veld {0} in te vullen")]
        public string Description { get; set; }

        [Display(Name = "Prijs")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Het veld {0} is vereist")]
        [Range(0.01, 5000.00, ErrorMessage = "De prijs moet tussen 0.01 en 5000.00 liggen")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [Display(Name = "Afbeelding")]
        [DataType(DataType.ImageUrl)]
        [StringLength(256, ErrorMessage = "Het veld FiguurURL kan niet langer zijn dan 256 karakters")]
        public string UrlImage { get; set; }

        public bool Spotlight { get; set; }

        public Category Category { get; set; }
    }
}
