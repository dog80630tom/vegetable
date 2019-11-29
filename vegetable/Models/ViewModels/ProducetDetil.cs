using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models.ViewModels
{
    public class ProducetDetil
    {
        [Key]
        public int ProductID { get; set; }
    
        [Display(Name="產品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "類別")]
        public string CategoryName { get; set; }
        [Display(Name = "產品描述")]
        public string ProductDescription { get; set; }
        [Display(Name = "庫存")]
        public int UnitsInStock { get; set; }
        [Display(Name = "圖片路徑")]
        public string PicUrl { get; set; }
        [Display(Name = "類別圖片路徑")]
        public string CategoryPic { get; set; }
        [Display(Name = "類別描述")]
        public string CategoryDescription { get; set; }
        [Display(Name = "產品價格")]
        public decimal ProductPrice { get; set; }
    }
}