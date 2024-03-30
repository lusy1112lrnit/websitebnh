using DACS_WebNuocHoa.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DACS_WebNuocHoa.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var listProduct = db.NuocHoa.ToList();
            return View(listProduct);
        }

        public ActionResult QLLoaiNu()
        {
            var listProductNu = db.NuocHoa.Where(p => p.MaLoai == 1).ToList();
            return View(listProductNu);
        }

        public ActionResult QLLoaiNam()
        {
            var listProductNam = db.NuocHoa.Where(p => p.MaLoai == 2).ToList();
            return View(listProductNam);
        }

        // GET: Admin/NuocHoas/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.Loai, "MaLoai", "TenLoai");
            return View();
        }

        // POST: Admin/NuocHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNH,TenNH,MoTa,Gia,Hinh,MaLoai,NgayCapNhat,DatBiet")] NuocHoa nuocHoa, HttpPostedFileBase Hinh)
        {
            if (ModelState.IsValid)
            {
                // Luu hinh vao web server
                if (Hinh != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Theme_NuocHoaStore/images/"), Path.GetFileName(Hinh.FileName));
                    Hinh.SaveAs(path);

                }

                // Luu nuoc hoa vao db
                nuocHoa.Hinh = "/Content/Theme_NuocHoaStore/images/" + Path.GetFileName(Hinh.FileName);
                db.NuocHoa.Add(nuocHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.Loai, "MaLoai", "TenLoai", nuocHoa.MaLoai);
            return View(nuocHoa);
        }

        // GET: Admin/Product/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NuocHoa nuocHoa = db.NuocHoa.Find(id);
            if (nuocHoa == null)
            {
                return HttpNotFound();
            }
            return View(nuocHoa);
        }

        // GET: Admin/Product/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NuocHoa nuocHoa = db.NuocHoa.Find(id);
            if (nuocHoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.Loai, "MaLoai", "TenLoai", nuocHoa.MaLoai);
            return View(nuocHoa);
        }

        // POST: Admin/Product/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNH,TenNH,MoTa,Gia,Hinh,MaLoai,NgayCapNhat,DatBiet")] NuocHoa nuocHoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nuocHoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.Loai, "MaLoai", "TenLoai", nuocHoa.MaLoai);
            return View(nuocHoa);
        }

        // GET: Admin/Product/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NuocHoa nuocHoa = db.NuocHoa.Find(id);
            if (nuocHoa == null)
            {
                return HttpNotFound();
            }
            return View(nuocHoa);
        }

        // POST: Admin/Product/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NuocHoa nuocHoa = db.NuocHoa.Find(id);
            db.NuocHoa.Remove(nuocHoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}