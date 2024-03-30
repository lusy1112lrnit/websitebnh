using DACS_WebNuocHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DACS_WebNuocHoa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /*List<Product> products = new List<Product>()
            {
                new Product(){MaNH = 1, TenNH="LV Nuoc Hoa 1", Mota="Mo ta", Gia= 100000, Hinh="/Content/Theme_WebNuocHoa/category1.jpg"},
                new Product(){MaNH = 1, TenNH="LV Nuoc Hoa 2", Mota="Mo ta", Gia= 60000, Hinh="/Content/Theme_WebNuocHoa/category2.jpg"},
                new Product(){MaNH = 1, TenNH="LV Nuoc Hoa 3", Mota="Mo ta", Gia= 200000, Hinh="/Content/Theme_WebNuocHoa/category3.jpg"},
                new Product(){MaNH = 1, TenNH="LV Nuoc Hoa 4", Mota="Mo ta", Gia= 400000, Hinh="/Content/Theme_WebNuocHoa/category1.jpg"}
            };
            Application["listProduct"] = products;*/
        }
    }
}
