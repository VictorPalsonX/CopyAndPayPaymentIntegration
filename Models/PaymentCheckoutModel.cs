using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace CopyAndPayPaymentIntegration.Models
{
    public class PaymentCheckoutModel
    {
        public Dictionary<string, dynamic> PaymentRequest()
        {
            Dictionary<string, dynamic> responseData;
            string data = "authentication.userId=8a8294174b7ecb28014b9699220015cc" +
                "&authentication.password=sy6KJsT8" +
                "&authentication.entityId=8a8294174b7ecb28014b9699220015ca" +
                "&amount=10.00" +
                "&currency=EUR" +                
                "&paymentBrand=VISA" +
                "&paymentType=CD" +
                "&card.number=42000000000000001111111111111" +
                "&card.expiryMonth=12" +
                "&card.expiryYear=2018" +
                "&card.holder=Jane Jones";
            string url = "https://test.globalonepayapm.io/v1/checkouts";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Stream PostData = request.GetRequestStream();
            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var s = new JavaScriptSerializer();
                responseData = s.Deserialize<Dictionary<string, dynamic>>(reader.ReadToEnd());
                reader.Close();
                dataStream.Close();
            }
            if (responseData != null)
            {
                System.Web.HttpContext.Current.Session["PaymentCheckoutId"] = responseData["id"];
            }
            return responseData;
        }

        //responseData = Request()["result"]
        //["description"];
        public Dictionary<string, dynamic> Request()
        {
            Dictionary<string, dynamic> responseData;
            string data = "authentication.userId=8a8294174b7ecb28014b9699220015cc" +
                "&authentication.password=sy6KJsT8" +
                "&authentication.entityId=8a8294174b7ecb28014b9699220015ca";
            string url = "https://test.globalonepayapm.io/v1/checkouts/" + System.Web.HttpContext.Current.Session["PaymentCheckoutId"] + "/payment?" + data;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var s = new JavaScriptSerializer();
                responseData = s.Deserialize<Dictionary<string, dynamic>>(reader.ReadToEnd());
                reader.Close();
                dataStream.Close();
            }
            return responseData;
        }
    }


    public class RequestData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}