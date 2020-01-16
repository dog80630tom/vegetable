using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using vegetable.Cors;
using vegetable.Controllers;
using vegetable.Services;
using vegetable.Respository;
using vegetable.Models.ViewModels;
using System.Web.Script.Serialization;

namespace vegetable.Models.LinePay
{
    public class Lineconfirm
    {
        public int amount
        {
            get; set;
        }
        public string currency
        {
            get; set;
        }
    }
    public class PaySuccess
    {
        public string returnCode
        {
            get; set;
        }
    }
    public class Linedata
    {

        public string productName
        {
            get; set;
        }
        public string productImageUrl
        {
            get; set;
        }
        public int amount
        {
            get; set;
        }
        public string currency
        {
            get; set;
        }
        public string orderId
        {
            get; set;
        }
        public string confirmUrl
        {
            get; set;
        }
    }

    

    [AllowCrossSite]
    public class LinePay
    {
        static string Url = "https://lobeda.azurewebsites.net/";
        public string passdata (int memberId, int price)
        {
            var post = new WebClient();
            post.Encoding = System.Text.Encoding.UTF8;
            //var data = new System.Net.WebHeaderCollection();
            LinePay linePay = new LinePay();
            Linedata linedata = linePay.PaymentInfo(memberId, price);
            post.Headers.Add("X-LINE-ChannelId", "1653696494");
            post.Headers.Add("X-LINE-ChannelSecret", "86ba3a2c73531c34810b6617047ef6a8");
            post.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string json = JsonConvert.SerializeObject(linedata);

            //var bResult = post.UploadString("https://api-pay.line.me/v2/payments/request", json);
            var bResult = post.UploadString("https://sandbox-api-pay.line.me/v2/payments/request", json);
            return bResult;
        }
        public PaySuccess confirm (int amount, string info)
        {

            var post = new WebClient();

            post.Encoding = System.Text.Encoding.UTF8;
            var data = new System.Net.WebHeaderCollection();

            post.Headers.Add("X-LINE-ChannelId", "1653696494");
            post.Headers.Add("X-LINE-ChannelSecret", "86ba3a2c73531c34810b6617047ef6a8");
            post.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            Lineconfirm lineconfirm = new Lineconfirm() { amount = amount, currency = "TWD" };
            string json = JsonConvert.SerializeObject(lineconfirm);
            var bResult = post.UploadString("https://sandbox-api-pay.line.me/v2/payments/" + "" + info + "" + "/confirm", json);
            var GetTokenFromCodeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PaySuccess>(bResult);
            int i = 0;
            return GetTokenFromCodeResult;
        }

        public Linedata PaymentInfo (int memberId, int price)
        {
            OrderDetailRepository orderDetail = new OrderDetailRepository();
            var productInfo = orderDetail.GetAllCart(memberId).FirstOrDefault();
            CartServices cartServices = new CartServices();
            //數量
            int quantity = cartServices.GetCarQuantity(memberId).CountAmount;
            JavaScriptSerializer js = new JavaScriptSerializer();
            JsonURL url = js.Deserialize<JsonURL>(productInfo.PicUrl);

            LinePay line = new LinePay();
            Linedata linedata = new Linedata()
            {
                productName = line.ProductName(quantity, productInfo.ProductName),
                currency = "TWD",
                orderId = productInfo.OrderID.ToString(),
                productImageUrl = url.Url1,
                amount = price,
                confirmUrl = Url + "Checkout"

            };
            return linedata;
        }

        public string ProductName (int quantity, string productName)
        {
            productName = productName + $"，總共{quantity}項商品";
            return productName;
        }
    }
}