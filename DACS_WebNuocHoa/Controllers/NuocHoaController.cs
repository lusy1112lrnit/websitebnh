using DACS_WebNuocHoa.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Owin.BuilderProperties;

namespace DACS_WebNuocHoa.Controllers
{
    public class NuocHoaController : Controller
    {
        // GET: NuocHoa
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
        public ActionResult Index()
        {
            var listNuocHoa = db.NuocHoa.ToList();
            return View("Index", listNuocHoa);
        }
        */
        public ActionResult Index(int? page)
        {
            const int PAGE_SIZE = 6; // Số sản phẩm hiển thị trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại (mặc định là trang 1)

            var listNuocHoa = db.NuocHoa
                                .OrderBy(n => n.MaNH)
                                .Skip((pageNumber - 1) * PAGE_SIZE)
                                .Take(PAGE_SIZE)
                                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = Math.Ceiling((double)db.NuocHoa.Count() / PAGE_SIZE);

            return View("Index", listNuocHoa);
        }

        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            var chitietNH = db.NuocHoa.FirstOrDefault(x => x.MaNH == id);
            if (chitietNH == null)
            {
                return HttpNotFound();
            }
            return View("Details", chitietNH);
        }
        public ActionResult NuocHoaNu(int? page)
        {
            const int PAGE_SIZE = 6;
            int pageNumber = (page ?? 1);

            var listNHNu = db.NuocHoa
                                .Where(n => n.MaLoai == 1)
                                .OrderBy(m => m.MaNH)
                                .Skip((pageNumber - 1) * PAGE_SIZE)
                                .Take(PAGE_SIZE)
                                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = Math.Ceiling((double)db.NuocHoa.Where(n => n.MaLoai == 1).Count() / PAGE_SIZE);

            return View("NuocHoaNu", listNHNu);
        }

        public ActionResult NuocHoaNam(int? page)
        {
            const int PAGE_SIZE = 6;
            int pageNumber = (page ?? 1);

            var listNHNam = db.NuocHoa
                                .Where(n => n.MaLoai == 2)
                                .OrderBy(m => m.MaNH)
                                .Skip((pageNumber - 1) * PAGE_SIZE)
                                .Take(PAGE_SIZE)
                                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = Math.Ceiling((double)db.NuocHoa.Where(n => n.MaLoai == 2).Count() / PAGE_SIZE);

            return View("NuocHoaNam", listNHNam);
        }

        public ActionResult SanPhamMoi(int? page)
        {

            const int PAGE_SIZE = 6;
            int pageNumber = (page ?? 1);

            var ngayCapNhatMoiNhat = DateTime.Now.AddDays(-30); // Lấy ngày cập nhật cách đây 30 ngày
            var listSPM = db.NuocHoa
                                .Where(nh => nh.NgayCapNhat >= ngayCapNhatMoiNhat)
                                .OrderByDescending(nh => nh.NgayCapNhat)
                                .Skip((pageNumber - 1) * PAGE_SIZE)
                                .Take(PAGE_SIZE)
                                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = Math.Ceiling((double)db.NuocHoa.Where(nh => nh.NgayCapNhat >= ngayCapNhatMoiNhat).Count() / PAGE_SIZE);

            return View("SanPhamMoi", listSPM);

        }

        public ActionResult SanPhamDatBiet(int? page)
        {

            const int PAGE_SIZE = 6;
            int pageNumber = (page ?? 1);

            var listSPDB = db.NuocHoa
                                .Where(nh => nh.DatBiet == true)
                                .OrderByDescending(nh => nh.MaNH)
                                .Skip((pageNumber - 1) * PAGE_SIZE)
                                .Take(PAGE_SIZE)
                                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = Math.Ceiling((double)db.NuocHoa.Where(nh => nh.DatBiet == true).Count() / PAGE_SIZE);

            return View("SanPhamDatBiet", listSPDB);

        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Search(string searchString)
        {
            var products = db.NuocHoa.Where(p => p.TenNH.Contains(searchString)).ToList();
            return View("Index", products);
        }

        public ActionResult SearchNHNu(string searchString)
        {
            var products = db.NuocHoa.Where(p => p.TenNH.Contains(searchString) && p.MaLoai == 1).ToList();
            return View("NuocHoaNu", products);
        }

        public ActionResult SearchNHNam(string searchString)
        {
            var products = db.NuocHoa.Where(p => p.TenNH.Contains(searchString) && p.MaLoai == 2).ToList();
            return View("NuocHoaNam", products);
        }

        public ActionResult SearchSPM(string searchString)
        {
            var ngayCapNhatMoiNhat = DateTime.Now.AddDays(-30);
            var products = db.NuocHoa.Where(p => p.TenNH.Contains(searchString) && p.NgayCapNhat >= ngayCapNhatMoiNhat).ToList();
            return View("SanPhamMoi", products);
        }

        public ActionResult SearchSPDB(string searchString)
        {
            var products = db.NuocHoa.Where(p => p.TenNH.Contains(searchString) && p.DatBiet == true).ToList();
            return View("SanPhamDatBiet", products);
        }

        public ActionResult FilterByPrice(decimal? minprice, decimal? maxprice)
        {
            if (!minprice.HasValue) minprice = 0;
            if (!maxprice.HasValue) maxprice = 100000000;

            var productList = db.NuocHoa.Where(p => p.Gia >= minprice && p.Gia <= maxprice).ToList();

            ViewBag.MinPrice = minprice;
            ViewBag.MaxPrice = maxprice;

            return View("Index", productList);
        }

        public ActionResult FilterByPrice_Nu(decimal? minprice, decimal? maxprice)
        {
            if (!minprice.HasValue) minprice = 0;
            if (!maxprice.HasValue) maxprice = 100000000;

            var productList = db.NuocHoa.Where(p => p.Gia >= minprice && p.Gia <= maxprice && p.MaLoai == 1).ToList();

            ViewBag.MinPrice = minprice;
            ViewBag.MaxPrice = maxprice;

            return View("NuocHoaNu", productList);
        }

        public ActionResult FilterByPrice_Nam(decimal? minprice, decimal? maxprice)
        {
            if (!minprice.HasValue) minprice = 0;
            if (!maxprice.HasValue) maxprice = 100000000;

            var productList = db.NuocHoa.Where(p => p.Gia >= minprice && p.Gia <= maxprice && p.MaLoai == 2).ToList();

            ViewBag.MinPrice = minprice;
            ViewBag.MaxPrice = maxprice;

            return View("NuocHoaNam", productList);
        }

        public ActionResult FilterByPriceSPM(decimal? minprice, decimal? maxprice)
        {
            if (!minprice.HasValue) minprice = 0;
            if (!maxprice.HasValue) maxprice = 100000000;

            var ngayCapNhatMoiNhat = DateTime.Now.AddDays(-30);
            var productList = db.NuocHoa.Where(p => p.Gia >= minprice && p.Gia <= maxprice && p.NgayCapNhat >= ngayCapNhatMoiNhat).ToList();

            ViewBag.MinPrice = minprice;
            ViewBag.MaxPrice = maxprice;

            return View("SanPhamMoi", productList);
        }

        public ActionResult FilterByPriceSPDB(decimal? minprice, decimal? maxprice)
        {
            if (!minprice.HasValue) minprice = 0;
            if (!maxprice.HasValue) maxprice = 100000000;

            var productList = db.NuocHoa.Where(p => p.Gia >= minprice && p.Gia <= maxprice && p.DatBiet == true).ToList();

            ViewBag.MinPrice = minprice;
            ViewBag.MaxPrice = maxprice;

            return View("SanPhamDatBiet", productList);
        }

        private int isExist(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].NuocHoa.MaNH.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult UpdateCart()
        {
            int productId = int.Parse(Request.Form["productId"]);
            int quantity = int.Parse(Request.Form["quantity"]);

            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = isExist(productId);
            if (index != -1)
            {
                cart[index].Quantity = quantity;
            }
            Session["cart"] = cart;

            return RedirectToAction("ViewCart");
        }

        public ActionResult RemoveCart(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = isExist(id);
            if (index != -1)
            {
                cart.RemoveAt(index);
            }
            Session["cart"] = cart;

            return RedirectToAction("ViewCart");

        }

        public ActionResult AddCart(int id)
        {
            NuocHoa nuochoa = db.NuocHoa.Find(id);
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { NuocHoa = nuochoa, Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { NuocHoa = nuochoa, Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            return View();
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

        [Authorize]
        public ActionResult Order()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            DonHang donHang = new DonHang();
            donHang.MaDH = GetNextMaDH();
            donHang.UserId = User.Identity.GetUserId();
            donHang.NgayDatHang = DateTime.Now;
            donHang.MaTT = 1;

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

            return RedirectToAction("SendMail");
        }

        public ActionResult BuyNowToCart(int id)
        {
            NuocHoa nuochoa = db.NuocHoa.Find(id);
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { NuocHoa = nuochoa, Quantity = 1 });
                Session["cart"] = cart;
            }
            return RedirectToAction("ViewCart");
        }

        [Authorize]
        public ActionResult BuyNow(int id)
        {
            NuocHoa product = db.NuocHoa.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            TempData["MaNH"] = product.MaNH;

            return RedirectToAction("BuyNowChoosePaymentMethod");
        }

        public ActionResult BuyNowChoosePaymentMethod()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuyNowChoosePaymentMethod(string paymentMethod)
        {
            int id = (int)TempData["MaNH"];
            NuocHoa nuochoa = db.NuocHoa.Find(id);
            List<CartItem> cart = new List<CartItem>();
            cart.Add(new CartItem { NuocHoa = nuochoa, Quantity = 1 });
            Session["cart"] = cart;
            if (paymentMethod == "cod")
            {
                return RedirectToAction("Order");
            }
            else if (paymentMethod == "paypal")
            {
                return RedirectToAction("PaymentWithPaypal", "ShoppingCart");
            }

            return RedirectToAction("BuyNowChoosePaymentMethod");
        }


        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ViewResult SendMail(DACS_WebNuocHoa.Models.SendMail _objModelMail)
        { 
            int maDH = (int)TempData["MaDH"];
            var ctdonhanglist = db.CTDonHang.Where(p=>p.MaDH == maDH).ToList();
            var donhang = db.DonHang.FirstOrDefault(p => p.MaDH == maDH);
            decimal tongTien = (decimal)donhang.TongTien;

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindById(donhang.UserId);

            MailMessage mail = new MailMessage();
            mail.To.Add(_objModelMail.To);
            mail.From = new MailAddress("lamluongsi@gmail.com");
            mail.Subject = "Thông báo đơn hàng từ WebNuocHoa";
            string Body = $"Xin chào, {user.Name}<br />" +
                            $"Địa chỉ: {user.Address}<br />" +
                            $"Phone: {user.Phone}<br />" +
                            $"------------------------------------------<br />" +
                            $"[ Bạn vừa đặt một đơn hàng ]<br />" +
                            $"MaDH = {maDH} và Tổng tiền = {tongTien.ToString("N0")} đ.<br />" +
                            $"------------------------------------------<br />" +
                            $"Thông tin chi tiết đơn hàng:<br />" +
                            $"------------------------------------------<br />" +
                            $"[ Mã ] [ Tên Nước Hoa ] [ SL ][ Gia ]<br />";
                            foreach (var ctdonhang in ctdonhanglist )
                            {
                                Body += $"[ { ctdonhang.MaNH } ][ { ctdonhang.TenNH } ][ { ctdonhang.SoLuong } ][ { ctdonhang.Gia.GetValueOrDefault().ToString("N0") } đ ]<br />";
                            }                      
                    Body += $"------------------------------------------<br />" +
                            $"Cảm ơn đã đặt hàng !!! <br />" +
                            $"Trân trọng,<br />WebNuocHoa";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("lamluongsi@gmail.com", "wsktnyhivepteaso"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
            ViewBag.Message = "Đã gởi thành công !!!";
            donhang.XacNhan = true;
            db.SaveChanges();
            return View("SendMail", _objModelMail);

        }
    }
}