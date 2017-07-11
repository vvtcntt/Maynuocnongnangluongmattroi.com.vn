using Maynuocnong.Models;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Maynuocnong.Controllers.Display
{
    public class DefaultController : Controller
    {
        private MaynuocnongContext db = new MaynuocnongContext();

        // GET: Default
        public ActionResult Index()
        {
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";
            ViewBag.h1 = "<h1 class=\"h1\">" + config.Title + "</h1>";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maynuocnongnangluongmattroi.com.vn\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + config.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + config.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://maynuocnongnangluongmattroi.com.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + config.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://maynuocnongnangluongmattroi.com.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://maynuocnongnangluongmattroi.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + config.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            return View(config);
        }
        public PartialViewResult partialdefault()
        {
            
            return PartialView(db.tblConfigs.First());
        }

        public PartialViewResult partialTransport()
        {
            return PartialView();
        }
        public PartialViewResult partialSlide()
        {
            var listSlide = db.tblImages.Where(p => p.idCate == 1 && p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            StringBuilder result1 = new StringBuilder();
            for (int i = 0; i < listSlide.Count;i++ )
            {
                if(i==0)
                {
                    result.Append("<div class=\"item active\">");
                    result.Append("<div class=\"fill\" style=\"background-image:url('"+listSlide[i].Images+"');\"></div>");
                    result.Append("</div>");
                    result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\""+i+"\" class=\"active\"></li>");
                }
                else
                {
                    result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + i + "\" ></li>");
                    result.Append("<div class=\"item\">");
                    result.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[i].Images + "');\"></div>");
                    result.Append("</div>");
                }
                  
            }
            ViewBag.result = result.ToString();
            ViewBag.result1 = result1.ToString();

                return PartialView();
        }
        public ActionResult Action()
        {
            var listProducts = db.tblProducts.ToList();
            for(int i=0;i<listProducts.Count;i++)
            {
                string keyword = listProducts[i].Keyword;
                int id = listProducts[i].id;
                StringClass.CreateTag(id, keyword);
                clsSitemap.CreateSitemap("/" + StringClass.NameToTag(listProducts[i].Name) + "-pd", id.ToString(), "Product");
            }
            var listGroupProduct = db.tblGroupProducts.ToList();
            for (int i = 0; i < listGroupProduct.Count; i++)
            {
                int id = listGroupProduct[i].id;
                clsSitemap.CreateSitemap("/" + StringClass.NameToTag(listGroupProduct[i].Name) + ".html", id.ToString(), "GroupProduct");
            }
            var listNews = db.tblNews.ToList();
            for (int i = 0; i < listNews.Count; i++)
            {
                int id = listNews[i].id;
                clsSitemap.CreateSitemap("/" + StringClass.NameToTag(listNews[i].Name) + "-ns", id.ToString(), "News");
            }
            var listCap = db.tblCapacities.ToList();
            for (int i = 0; i < listCap.Count; i++)
            {
                int id = listCap[i].id;
                clsSitemap.CreateSitemap("/" + StringClass.NameToTag(listCap[i].Name) + ".htm", id.ToString(), "Capacity");
            }
            return View();
        }
    }
}