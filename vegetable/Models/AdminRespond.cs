using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class AdminRespond
    {
        [Key, Column(Order = 0)]
        public int CustomerQuestionID { get; set; }
        [Key, Column(Order = 1)]
        public int AdminID { get; set; }
        public string AdminRespond1 { get; set; }
        public System.DateTime AdminRespondTime { get; set; }
    }
}