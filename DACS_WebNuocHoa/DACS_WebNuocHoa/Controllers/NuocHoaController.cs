using DACS_WebNuocHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACS_WebNuocHoa.Controllers
{
    public class NuocHoaController : Controller
    {
        // GET: NuocHoa
        private NuocHoaStoreModel db = new NuocHoaStoreModel();

        public ActionResult Index()
        {
            var listNuocHoa = db.NuocHoaes.ToList();
            return View("Index", listNuocHoa);
        }
        public ActionResult Details(int id)
        {
            var chitietNH = db.NuocHoaes.FirstOrDefault(x => x.Id == id);
            return View("Details", chitietNH);
        }
        public ActionResult Search(string searchString)
        {
            var products = db.NuocHoaes.Where(p => p.TenNH.Contains(searchString)).ToList();
            return View("Index", products);
        }
    }
}