using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Models.ViewModels
{
    public class ReportViewModel
    {
        public DateTime OrderDate { get; set; }
        public int Month { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }

        
    }
}