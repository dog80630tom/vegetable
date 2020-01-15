using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderDetailsID
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

        public int OrderID
        {
            get; set;
        }

        public DateTime OrderDate
        {
            get;set;
        }

        public string DeliverAddress
        {
            get;set;
        }

        public string DeliverName
        {
            get; set;
        }

        public string DeliverPhone
        {
            get; set;
        }

        public string CategoryName
        {
            get; set;
        }

        public string PicUrl
        {
            get; set;
        }
    }
}