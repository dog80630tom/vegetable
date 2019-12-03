using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class Product
    {
     public int ProductID{get; set;}
     public int CategoryID{get; set;}
     public string ProductName{get; set;}
     public string CategoryName{get; set;}
     public string ProductDescription{get; set;}
     public int UnitsInStock{get; set;}
     public decimal ProductPrice { get; set; }

    }
}