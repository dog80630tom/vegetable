using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Models.ViewModels
{
    public class ReportViewModel
    {
        public DateTime OrderDate { get; set; }
        public int mouth { get; set; }
        public string CategoryName { get; set; }
        public int year { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }
        public int total { get; set; }
        
    }
}