using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class Customer_Review
    {
        [Key]
        public int CR_ID { get; set; }
        public int ProductID { get; set; }
        public int CR_Rate { get; set; }
        public string CR_ReviewTitle { get; set; }
        public string CR_ReviewContent { get; set; }
        public System.TimeSpan CR_Time { get; set; }
    
        //public virtual Product Product { get; set; }
    }
}