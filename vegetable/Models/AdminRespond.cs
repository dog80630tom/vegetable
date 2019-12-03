using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class AdminRespond
    {
        public int CustomerQuestionID { get; set; }
        public int AdminID { get; set; }
        public string AdminRespond1 { get; set; }
        public System.DateTime AdminRespondTime { get; set; }
    
        //public virtual Admin Admin { get; set; }
        //public virtual Customer_Question Customer_Question { get; set; }
    }
}