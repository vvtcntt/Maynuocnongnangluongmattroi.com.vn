using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maynuocnong.Models;
using PagedList;
using PagedList.Mvc;
namespace Maynuocnong.Controllers.Display.Section.News
{
    public class NewsController : Controller
    {
        private MaynuocnongContext db = new MaynuocnongContext();
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        string nUrl = "";
        public string UrlNews(int idCate)
        {
            var ListMenu = db.tblGroupNews.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/" + ListMenu[i].Tag + "\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlNews(id);
                }

            }
            return nUrl;
        }
        public PartialViewResult newsHomesPartial()
        {
            var listTinTuc = db.tblNews.Where(p => p.Active == true && p.idCate == 1).OrderByDescending(p => p.DateCreate).Take(8).ToList();
            StringBuilder resultNews = new StringBuilder();
            StringBuilder resultListNews = new StringBuilder();
            for (int i = 0; i < listTinTuc.Count; i++)
            {
                if(i==0)
                {
                    resultNews.Append("<a href = \"/"+listTinTuc[i].Tag+"-ns\" title = \""+listTinTuc[i].Name+ "\" ><img src = \"" + listTinTuc[i].Images + "\" alt = \"" + listTinTuc[i].Name + "\" /></ a >");
                    resultNews.Append("<a href = \"/" + listTinTuc[i].Tag + "-ns\" title = \"" + listTinTuc[i].Name + "\" class=\"Name\">" + listTinTuc[i].Name + "</a>");
                    resultNews.Append("<span>"+listTinTuc[i].Description+"</span>");
                }
                else
                {
                    resultListNews.Append("<a href=\"/" + listTinTuc[i].Tag + "-ns\" title=\"" + listTinTuc[i].Name + "\">" + listTinTuc[i].Name + "</a>");
                }
                
            }
            ViewBag.resultNews = resultNews.ToString();
            ViewBag.resultListNews = resultListNews.ToString();
            //Load tư vấn kỹ thuẩt
            var listTuVan = db.tblNews.Where(p => p.Active == true && p.idCate == 2).OrderByDescending(p => p.DateCreate).Take(8).ToList();
            StringBuilder resultTuVan = new StringBuilder();
            StringBuilder resultListTuVan = new StringBuilder();
            for (int i = 0; i < listTuVan.Count; i++)
            {
                if (i == 0)
                {
                    resultTuVan.Append("<a href = \"/" + listTuVan[i].Tag + "-ns\" title = \"" + listTuVan[i].Name + "\" ><img src = \"" + listTuVan[i].Images + "\" alt = \"" + listTuVan[i].Name + "\" /></ a >");
                    resultTuVan.Append("<a href = \"/" + listTuVan[i].Tag + "-ns\" title = \"" + listTuVan[i].Name + "\" class=\"Name\">" + listTuVan[i].Name + "</a>");
                    resultTuVan.Append("<span>"+listTuVan[i].Description+"</span>");
                }
                else
                {
                    resultListTuVan.Append("<a href=\"/" + listTuVan[i].Tag + "-ns\" title=\"" + listTuVan[i].Name + "\">" + listTuVan[i].Name + "</a>");
                }

            }
            ViewBag.resultTuVan = resultTuVan.ToString();
            ViewBag.resultListTuVan = resultListTuVan.ToString();
            return PartialView();
        }
        public ActionResult newsDetail(string tag , int id=0)
        {
            var tblnews = db.tblNews.FirstOrDefault(p => p.Tag == tag);
             if(tblnews==null)
                 tblnews = db.tblNews.FirstOrDefault(p => p.id == id);
             int idUser = int.Parse(tblnews.idUser.ToString());
            ViewBag.Username = db.tblUsers.Find(idUser).UserName;
            int idCate = int.Parse(tblnews.idCate.ToString());
            var groupnews = db.tblGroupNews.First(p => p.id == idCate);
            ViewBag.NameMenu = groupnews.Name;
            ViewBag.Title = "<title>" + tblnews.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblnews.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblnews.Keyword + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblnews.Title + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblnews.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Maynuocnongnangluongmattroi.com.vn/" + StringClass.NameToTag(tag) + "-ns\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblnews.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblnews.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Maynuocnongnangluongmattroi.com.vn" + tblnews.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblnews.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Maynuocnongnangluongmattroi.com.vn" + tblnews.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Maynuocnongnangluongmattroi.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblnews.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Descriptionss = tblnews.Description;
            ViewBag.Meta = meta;
            int ids = int.Parse(tblnews.id.ToString());
            if (tblnews.Keyword != null)
            {
                string Chuoi = tblnews.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tabs = Mang[i].ToString();
                    var listnew = db.tblNews.Where(p => p.Keyword.Contains(tabs) && p.id != ids && p.Active == true).ToList();
                    for (int j = 0; j < listnew.Count; j++)
                    {
                        araylist.Add(listnew[j].id);
                    }
                }
                var Lienquan = db.tblNews.Where(p => araylist.Contains(p.id) && p.Active == true && p.id != ids).OrderByDescending(p => p.Ord).Take(3).ToList();
                string chuoinew = "";
                if (Lienquan.Count > 0)
                {

                    chuoinew += " <div class=\"Lienquan\">";
                    for (int i = 0; i > Lienquan.Count; i++)
                    {
                        chuoinew += "<a href=\"/" + Lienquan[i].Tag + "-ns\" title=\"" + Lienquan[i].Name + "\"> " + Lienquan[i].Name + "</a>";
                    }
                    chuoinew += "</div>";
                }
                ViewBag.chuoinew = chuoinew;


                //Load tin mới

            }

            string chuoinewnew = "";
            var NewsNew = db.tblNews.Where(p => p.Active == true && p.id != ids).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/" + NewsNew[i].Tag + "-ns\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;

            //load tag
            string chuoitag = "";
            if (tblnews.Keyword != null)
            {
                string Chuoi = tblnews.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/TagNews/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;

            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlNews(idCate);
            return View(tblnews);
        }
        public PartialViewResult leftNewsPartial()
        {
            return PartialView();
        }
        public ActionResult ListNews(string tag, int? page)
        {
            var groupnew = db.tblGroupNews.First(p => p.Tag == tag);
            int idcate = groupnew.id;
            var listnews = db.tblNews.Where(p => p.idCate == idcate && p.Active == true).OrderByDescending(p => p.Ord).ToList();
            string chuoinewnew = "";
            var NewsNew = db.tblNews.Where(p => p.Active == true && p.idCate != idcate).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/" + NewsNew[i].Tag + "-ns\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;
            string chuoichinhsach = "";
            var Chinhsach = db.tblNews.Where(p => p.Active == true && p.idCate == 3).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < Chinhsach.Count; i++)
            {
                chuoichinhsach += "<li><a href=\"/" + Chinhsach[i].Tag + "-ns\" title=\"" + Chinhsach[i].Name + "\" rel=\"nofollow\"> " + Chinhsach[i].Name + " <span>" + Chinhsach[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoichinhsach = chuoichinhsach;
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = groupnew.Name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlNews(groupnew.id);
            ViewBag.Title = "<title>" + groupnew.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + groupnew.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + groupnew.Keyword + "\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TagNews(string tag, int? page)
        {
            string[] Mang1 = StringClass.COnvertToUnSign1(tag).Split('-');
            string chuoitag = "";
            for (int i = 0; i < Mang1.Length; i++)
            {
                if (i == 0)
                    chuoitag += Mang1[i];
                else
                    chuoitag += " " + Mang1[i];
            }
            int dem = 1;
            string name = "";
            List<tblNew> ListNew = (from c in db.tblNews where c.Active == true select c).ToList();
            List<tblNew> listnews = ListNew.FindAll(delegate(tblNew math)
            {
                if (StringClass.COnvertToUnSign1(math.Keyword.ToUpper()).Contains(chuoitag.ToUpper()))
                {

                    string[] Manghienthi = math.Keyword.Split(',');
                    foreach (var item in Manghienthi)
                    {
                        if (dem == 1)
                        {
                            var kiemtra = StringClass.COnvertToUnSign1(item.ToUpper()).Contains(chuoitag.ToUpper());
                            if (kiemtra == true)
                            {
                                name = item;
                                dem = 0;
                            }
                        }
                    }

                    return true;
                }

                else
                    return false;
            }
            );
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> " + name + "";
            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }

    }
}