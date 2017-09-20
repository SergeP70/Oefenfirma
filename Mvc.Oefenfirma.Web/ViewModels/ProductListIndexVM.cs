using Mvc.OefenfirmaCMS.Lib.Entities;
using System.Collections.Generic;

namespace Mvc.Oefenfirma.Web.Models
{
    public class ProductListIndexVM
    {
        public IEnumerable<Product> ProductList { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}