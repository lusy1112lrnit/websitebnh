using DACS_WebNuocHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACS_WebNuocHoa.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ThongKe
        public ActionResult ThongKeSanPham()
        {
            // Lấy dữ liệu từ bảng CTDonHang
            var data = db.CTDonHang.GroupBy(x => x.TenNH)
                                   .Select(x => new { TenNH = x.Key, SoLuong = x.Sum(y => y.SoLuong) })
                                   .OrderByDescending(x => x.SoLuong)
                                   .Take(20)
                                   .ToList();

            // Chuyển đổi dữ liệu sang dạng mảng hai chiều
            var chartData = data.Select(x => new[] { x.TenNH, x.SoLuong.ToString() }).ToArray();

            // Truyền dữ liệu sang View
            ViewBag.ChartData = chartData;

            return View();
        }
    }
}