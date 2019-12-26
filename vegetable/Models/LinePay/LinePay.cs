using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using vegetable.Cors;

namespace vegetable.Models.LinePay
{
    public class Lineconfirm {
        public int amount { get; set; }
        public string currency { get; set; }
    }
    public class PaySuccess {
        public string returnCode { get; set; }
    }
    public class Linedata {

       public string productName { get; set; }
        public string productImageUrl { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string orderId { get; set; }
        public string confirmUrl { get; set; }
    }

    [AllowCrossSite]
    public class LinePay
    {
        public string passdata(int amount) {
            var post = new WebClient();
           
            post.Encoding = System.Text.Encoding.UTF8;
            var data = new System.Net.WebHeaderCollection();
            
            post.Headers.Add("X-LINE-ChannelId", "1653696494") ;
            post.Headers.Add("X-LINE-ChannelSecret", "86ba3a2c73531c34810b6617047ef6a8");
            post.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            Linedata linedata = new Linedata() { productName = "test product", productImageUrl = "http://testst.com", amount = amount, confirmUrl = "https://localhost:44394/FrontEnd/checkpay", currency = "TWD", orderId = "20140101123456789" };
            string json = JsonConvert.SerializeObject(linedata);
            
            var bResult = post.UploadString("https://api-pay.line.me/v2/payments/request", json);


            
       
            return bResult;
        }
        public PaySuccess confirm(int amount,string info) {

            var post = new WebClient();

            post.Encoding = System.Text.Encoding.UTF8;
            var data = new System.Net.WebHeaderCollection();

            post.Headers.Add("X-LINE-ChannelId", "1653696494");
            post.Headers.Add("X-LINE-ChannelSecret", "86ba3a2c73531c34810b6617047ef6a8");
            post.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            Lineconfirm lineconfirm = new Lineconfirm() { amount = amount, currency = "TWD" };
            string json = JsonConvert.SerializeObject(lineconfirm);
            var bResult = post.UploadString("https://api-pay.line.me/v2/payments/" + ""+info+""+"/confirm", json);
            var GetTokenFromCodeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PaySuccess>(bResult);
            int i = 0;
            return GetTokenFromCodeResult;
        }
    }
}