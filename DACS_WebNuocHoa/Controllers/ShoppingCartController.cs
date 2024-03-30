using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using DACS_WebNuocHoa.Models;
using Microsoft.AspNet.Identity;
using PayPal.Api;

namespace DACS_WebNuocHoa.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult FailureView()
        {
            return View();
        }

        public ActionResult SuccessView() 
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/shoppingcart/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                
                return View("FailureView");
            }
            //on successful payment, show success page to user.
            return RedirectToAction("SendMail", "NuocHoa");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private int GetNextMaDH()
        {
            int? maxMaDH = db.DonHang.Max(dh => (int?)dh.MaDH);
            int nextMaDH;

            if (maxMaDH.HasValue)
            {
                nextMaDH = maxMaDH.Value + 1;
            }
            else
            {
                nextMaDH = 1;
            }

            return nextMaDH;
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            DonHang donHang = new DonHang();
            donHang.MaDH = GetNextMaDH();
            donHang.UserId = User.Identity.GetUserId();
            donHang.NgayDatHang = DateTime.Now;
            donHang.MaTT = 2;

            decimal? total = cart.Sum(item => item.NuocHoa.Gia * item.Quantity);
            donHang.TongTien = total;

            foreach (CartItem item in cart)
            {
                CTDonHang ctDonHang = new CTDonHang();
                ctDonHang.MaNH = item.NuocHoa.MaNH;
                ctDonHang.MaDH = donHang.MaDH;
                ctDonHang.TenNH = item.NuocHoa.TenNH;
                ctDonHang.SoLuong = item.Quantity;
                ctDonHang.Gia = item.NuocHoa.Gia;

                db.CTDonHang.Add(ctDonHang);
            }

            db.DonHang.Add(donHang);
            db.SaveChanges();

            TempData["MaDH"] = donHang.MaDH;

            Session["cart"] = null;

            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            foreach (CartItem item in cart)
            {
                var product = item.NuocHoa;
                var nuocHoaItem = new Item()
                {
                    name = product.TenNH,
                    currency = "USD",
                    price = product.Gia.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = product.MaNH.ToString()
                };

                itemList.items.Add(nuocHoaItem);
            }

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = total.ToString()
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = total.ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();

            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(),
                amount = amount,
                item_list = itemList
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }


    }
}
