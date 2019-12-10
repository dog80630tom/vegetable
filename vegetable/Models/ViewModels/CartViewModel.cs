using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Models.ViewModels
{
    public class CartViewModel
    {
        public int CartId
        {
            get; set;
        }
        public string ProductName
        {
            get; set;
        }

        public decimal ProductPrice
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }
    }
}