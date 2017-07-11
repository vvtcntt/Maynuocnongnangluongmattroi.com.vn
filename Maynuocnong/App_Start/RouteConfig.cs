using Maynuocnong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Maynuocnong
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Lien-he", url: "Lien-he", defaults: new { controller = "Contact", action = "Index" });
            routes.MapRoute("liscap", "{tag}.htm", new { controller = "product", action = "ListCap", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^ListCap$" });
            routes.MapRoute("ListProduct", "{tag}.html", new { controller = "product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^ListProduct$" });
            routes.MapRoute("productdetail", "{tag}-pd", new { controller = "product", action = "productDetail", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productDetail$" });
            routes.MapRoute("Tag", "Tag/{tag}", new { controller = "Product", action = "Tag", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^Tag$" });
            routes.MapRoute("Chi-tiet-tin", "{tag}-ns", new { controller = "News", action = "NewsDetail", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^NewsDetail$" });
            routes.MapRoute("Chi-tiet-tin-1", "vn/home/tin-tuc/{tag}_{id}.html", new { controller = "News", action = "NewsDetail", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^NewsDetail$" });
            routes.MapRoute("Chi-tiet-sp-1", "vn/{tag2}/{tag1}/{tag}_{id}.html", new { controller = "product", action = "productDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^productDetail$" });
            routes.MapRoute("Chi-tiet-sp-2", "vn/{tag3}/{tag2}/{tag1}/{tag}_{id}.html", new { controller = "product", action = "productDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^productDetail$" });
            routes.MapRoute("Danh-sach-sab-pham-1", "vn/{tag1}/{tag}", new { controller = "product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("Baogia", "Bao-gia/{Tag}/{*catchall}", new { controller = "Baogia", action = "BaogiaDetail", tag = UrlParameter.Optional }, new { controller = "^B.*", action = "^BaogiaDetail$" });

            routes.MapRoute("Danh-sach-sab-pham", "vn/{tag2}/{tag1}/{tag}", new { controller = "product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("TagNew", "TagNews/{tag}", new { controller = "News", action = "TagNews", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^TagNews$" });
            routes.MapRoute(
    name: "CmsRoute",
    url: "{*tag}",
    defaults: new { controller = "news", action = "listNews" },
    constraints: new { tag = new CmsUrlConstraint() }
);

            routes.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Login", action = "LoginIndex" });
            routes.MapRoute(name: "Gio-hang", url: "Gio-hang", defaults: new { controller = "Order", action = "OrderIndex" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
