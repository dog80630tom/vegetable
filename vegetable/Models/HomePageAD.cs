using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class HomePageAD
    { 
        [Key]
        public int ADID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> CategoryADID { get; set; }
        public string EventPic { get; set; }

    }
}