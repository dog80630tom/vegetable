using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace vegetable.Models
{
    public class ProductList
    {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int UnitsInStock { get; set; }
        public decimal ProductPrice { get; set; }
        public string Url { get; set; }
    }


}