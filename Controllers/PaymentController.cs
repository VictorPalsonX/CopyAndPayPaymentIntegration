using CopyAndPayPaymentIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopyAndPayPaymentIntegration.Controllers
{
    public class PaymentController : Controller
    {
        PaymentCheckoutModel objPaymentCheckoutModel = new PaymentCheckoutModel();
        // GET: Payment
        public ActionResult Index()
        {
            var result = objPaymentCheckoutModel.PaymentRequest();
            return PartialView("Payment");
        }

        // GET: Payment
        public bool ValidatePayment(RequestData requestdata)
        {
            var result = objPaymentCheckoutModel.Request();
            return true;
        }
    }
}