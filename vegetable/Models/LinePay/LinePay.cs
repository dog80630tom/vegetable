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
        public string passdata (int memberId)
        {
            var post = new WebClient();
            post.Encoding = System.Text.Encoding.UTF8;
            //var data = new System.Net.WebHeaderCollection();
            LinePay linePay = new LinePay();
            object [] paymentInfo = linePay.PaymentInfo(memberId);
            OrderDetailViewModel order = (OrderDetailViewModel)paymentInfo [1];
            post.Headers.Add("X-LINE-ChannelId", "1653696494");
            post.Headers.Add("X-LINE-ChannelSecret", "86ba3a2c73531c34810b6617047ef6a8");
            post.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            Linedata linedata = new Linedata() { productName = order.ProductName, productImageUrl = (string)paymentInfo[2], amount = (int)paymentInfo[0], confirmUrl = "https://localhost:44394/Checkout", currency = "TWD", orderId = order.OrderID.ToString() };
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
            var bResult = post.UploadString("https://api-pay.line.me/v2/payments/" + "" + info + "" + "/confirm", json);
            var GetTokenFromCodeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PaySuccess>(bResult);
            int i = 0;
            return GetTokenFromCodeResult;
        }

        public object [] PaymentInfo (int memberId)
        {
            OrderDetailRepository orderDetail = new OrderDetailRepository();
            var productInfo = orderDetail.GetAllCart(memberId).FirstOrDefault();
            CartServices cartServices = new CartServices();
            int amount = cartServices.GetCartAmount(memberId).CountAmount;
            JavaScriptSerializer js = new JavaScriptSerializer();
            JsonURL url = js.Deserialize<JsonURL>(productInfo.PicUrl);
            object [] info = {amount, productInfo, url.Url1};
            return info;
        }
    }
}