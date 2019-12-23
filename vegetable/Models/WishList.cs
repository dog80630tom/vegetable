using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class WishList
    {
        [Key, Column(Order = 0)]
        public int MemberID { get; set; }
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
    }
}