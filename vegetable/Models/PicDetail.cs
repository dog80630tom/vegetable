using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class PicDetail
    {
        [Key]
        public string PicUrl { get; set; }
        public int ProductID { get; set; }
    }
}