using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models.ViewModels
{
    public class NewProductDetail
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "產品類別")]
        public int CategoryId { get; set; }
        [Display(Name = "產品描述")]
        public string ProductDescription { get; set; }
        [Display(Name = "庫存")]
        public int UnitsInStock { get; set; }   
        [Display(Name = "產品價格")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "圖片路徑")]
        public string PicUrl { get; set; }

    }
}