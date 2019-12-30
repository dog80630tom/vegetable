using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        public int OrderID
        {
            get; set;
        }
        [Key, Column(Order = 1)]
        public int ProductID
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
        public int Discount
        {
            get; set;
        }
        public bool IsWish
        {
            get; set;
        }

        public int MemberID
        {
            get; set;
        }

        //public virtual Order Order { get; set; }
        //public virtual Product Product { get; set; }
    }
}