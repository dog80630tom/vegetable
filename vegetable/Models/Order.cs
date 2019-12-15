using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int MemberID { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        //Html.EditorFor(model => model.Ondate)
        public DateTime OrderDate { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string DeliverName { get; set; }
        [Required]
        [Display(Name = "地址")]
        public string DeliverNameAddress { get; set; }
        [Required]
        [Phone]
        [Display(Name = "電話")]
        public string DeliverPhone { get; set; }
    }
}