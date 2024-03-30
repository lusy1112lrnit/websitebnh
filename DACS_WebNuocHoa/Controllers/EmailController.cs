using DACS_WebNuocHoa.Migrations;
using DACS_WebNuocHoa.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DACS_WebNuocHoa.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<IdentityUser> UserManager { get; }
        // GET: Email

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Index(DACS_WebNuocHoa.Models.SendMail _objModelMail)
        {
            var donHang = db.DonHang.FirstOrDefault();
            int maDH = donHang.MaDH;
            decimal tongTien = (decimal)donHang.TongTien;

            MailMessage mail = new MailMessage();
            mail.To.Add(_objModelMail.To);
            mail.From = new MailAddress("lamluongsi@gmail.com");
            mail.Subject = "Xác nhận đơn hàng từ WebNuocHoa";
            string Body = $"Xin chào, {User.Identity.Name}<br /><br />" +
                          $"Bạn đã đặt một đơn hàng với mã đơn hàng {maDH} và tổng tiền là {tongTien.ToString("N0")} VNĐ.<br /><br />" +
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
            return View("Index", _objModelMail);

        }

    }
}