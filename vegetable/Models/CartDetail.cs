using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class CartDetail
    {
        [Key]
        public int CartID { get; set; }
        public int MemberID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public bool IsWish { get; set; }
    
        //public virtual Member Member { get; set; }
        //public virtual Product Product { get; set; }
    }
}