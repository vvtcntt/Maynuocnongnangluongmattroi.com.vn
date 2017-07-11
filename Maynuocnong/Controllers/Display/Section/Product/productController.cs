using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maynuocnong.Models;
using System.Text;

namespace Maynuocnong.Controllers.Display.Section.Product
{
    public class productController : Controller
    {
        private MaynuocnongContext db = new MaynuocnongContext();
        // GET: product
        public ActionResult Index()
        {
            return View();
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = "<li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" title=\"" + ListMenu[i].Title + "\" href=\"/" + ListMenu[i].Tag + ".html\"> <span itemprop=\"name\">" + ListMenu[i].Name + "</span></a> <meta itemprop=\"position\" content=\"" + (ListMenu[i].Level + 2) + "\" /> </li> ›" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        public PartialViewResult productListHomes()
        {
            var listGroup = db.tblGroupProducts.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for(int i=0;i<listGroup.Count;i++)
            {
                int id = listGroup[i].id;

                var listID = db.tblGroupProducts.Where(p => p.ParentID == id && p.Active == true).ToList();
                List<string> Mang = new List<string>();
                for (int j = 0; j < listID.Count; j++)
                {
                    Mang.Add(listID[j].id.ToString());

                }
                result.Append("<div class=\"tear_Product\">");
                result.Append(" <h2><a href = \"/" +listGroup[i].Tag +".html\" title=\""+listGroup[i].Name+ "\">" + listGroup[i].Name + "</a></h2>");
                result.Append("<div class=\"product_Header\">");
                result.Append("<div class=\"slide_Product\">");


                var listIdImage = db.tblConnectImages.Where(p => Mang.Contains(p.idCate.ToString())).Select(p => p.idImg).ToList();
                var listSlide = db.tblImages.Where(p => p.idCate == 2 && listIdImage.Contains(p.id) && p.Active == true).OrderBy(p => p.Ord).ToList();
                StringBuilder result2 = new StringBuilder();
                StringBuilder result1 = new StringBuilder();
                for (int j = 0; j < listSlide.Count; j++)
                {
                    if (j == 0)
                    {
                        result2.Append("<div class=\"item active\">");
                        result2.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[j].Images + "');\"></div>");
                        result2.Append("</div>");
                        result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + j + "\" class=\"active\"></li>");
                    }
                    else
                    {
                        result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + j + "\" ></li>");
                        result2.Append("<div class=\"item\">");
                        result2.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[j].Images + "');\"></div>");
                        result2.Append("</div>");
                    }

                }

                if (listSlide.Count > 0)
                {
                    result.Append(" <div id=\"myCarousel\" class=\"carousel slide\">");
                    result.Append(" <ol class=\"carousel-indicators\">");
                    result.Append(result1);
                    result.Append(" </ol>");
                    result.Append("<div class=\"carousel-inner\">");
                    result.Append(result2);
                    result.Append(" </div>");
                    result.Append(" </div>");
                }
                //sdssdsds




                result.Append("</div>");
                result.Append("<div class=\"video_Product\"><iframe width = \"362\" height=\"307\" src=\"https://www.youtube.com/embed/" + listGroup[i].Video + "?rel=0&amp;showinfo=0\" frameborder=\"0\" allowfullscreen></iframe></div>");
                result.Append("</div>");
                result.Append("<div class=\"nvar_Product\">");
                result.Append("<ul>");
                var listChild = db.tblGroupProducts.Where(p => p.ParentID == id && p.Active == true).OrderBy(p => p.Ord).Take(2).ToList();
                for(int j=0;j<listChild.Count;j++)
                {
                    result.Append("  <li><a href = \"/"+listChild[j].Tag+".html\" title=\""+listChild[j].Name+ "\">" + listChild[j].Name + "</a></li>");

                }
                result.Append(" </ul>");
                result.Append("  </div>");
                result.Append(" <div class=\"list_TearProduct\">");
               
                var listIdManu = db.tblConnectManuProducts.Where(p => Mang.Contains(p.idCate.ToString())).Select(p => p.idManu).ToList();
                var ListManu = db.tblManufactures.Where(p => p.Active == true && listIdManu.Contains(p.id)).OrderBy(p => p.Ord).Take(1).ToList();                 
                var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString()) && p.ViewHomes == true).OrderBy(p=>p.idCate).ToList();
                for(int j=0;j<listProduct.Count;j++)
                {
                    int idp = listProduct[j].id;
                    result.Append("  <div class=\"tear_1\">");
                    result.Append(" <div class=\"img\">");
                    result.Append("<a href = \"/"+listProduct[j].Tag+"-pd\" title=\""+listProduct[j].Name+ "\"><img src = \"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                    result.Append(" </div>");
                    result.Append(" <a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                    result.Append(" <div class=\"info\">");
                    string nOng = listProduct[j].Ong.ToString();
                    if(nOng!=null && nOng!="")
                    {
                        int idOng = int.Parse(nOng);
                        tblOng tblong = db.tblOngs.FirstOrDefault(p => p.id == idOng);
                        if (tblong != null)
                            result.Append("<span>" + tblong.Name + "</span>");
                    }
                    string nBbo = listProduct[j].BBO.ToString();
                    if(nBbo!=null && nBbo!="")
                    {
                        int idBbo = int.Parse(nBbo);
                        tblBBO tblbbo = db.tblBBOes.FirstOrDefault(p => p.id == idBbo);
                        result.Append("<span>BBO: " + tblbbo.Name + "</span>");
                    }               
                    result.Append(" </div>");
                    result.Append("<div class=\"box_tear\">");
                    result.Append("<div class=\"box_price\">");
                    result.Append("<span class=\"price\">"+string.Format("{0:#,#}",listProduct[j].Price)+"đ</span>");
                    result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                    result.Append("</div>");
                  if(ListManu.Count>0)
                  {
                      result.Append("<div class=\"box_logo\">");
                      result.Append(" <a href = \"javascript:void(0)\" title=\"\"><img src = \"" + ListManu[0].Images + "\" alt=\"" + ListManu[0].Name + "\" /></a>");
                      result.Append("</div>");
                  }
                   
                    result.Append(" </div>");
                    result.Append("</div>");
                }
                result.Append("</div>");
                if (listProduct.Count > 15)
                {
                    result.Append("<div class=\"box_Xemthem\">");
                    result.Append("<a href = \"/" + listGroup[i].Tag + ".html\" title=\"" + listGroup[i].Name + "\" class=\"xemthem\"><i>&raquo; &raquo;  Xem thêm nhiều hơn</i></a>");
                    result.Append(" </div>");
                }   
                result.Append("</div>");
                Mang.Clear();
            }
            ViewBag.result = result.ToString();
            return PartialView();
        }
        public ActionResult productDetail(string tag,int id=0)
        {
            var products = db.tblProducts.FirstOrDefault(p => p.Tag == tag);
            if (products == null)
            {
                products = db.tblProducts.FirstOrDefault(p => p.id == id);
            }
            ViewBag.Title = "<title>" + products.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + products.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + products.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + products.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maynuocnongnangluongmattroi.com.vn/" + StringClass.NameToTag(tag) + "-pd\" />";
            string meta = "";
            products.Visit = products.Visit + 1;
            db.SaveChanges();
            meta += "<meta itemprop=\"name\" content=\"" + products.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + products.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://maynuocnongnangluongmattroi.com.vn" + products.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + products.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://maynuocnongnangluongmattroi.com.vn" + products.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://maynuocnongnangluongmattroi.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + products.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; int idcate = int.Parse(products.idCate.ToString());
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›" + UrlProduct(idcate) + "</ol><h1>" + products.Title + "</h1>";
            var tblManu = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b).Take(1).ToList();
            int idManu = 0;
            if (tblManu.Count > 0)
            {
                ViewBag.manu = tblManu[0].Name;
                ViewBag.urlmanu = tblManu[0].Images;
                idManu = tblManu[0].id;
                ViewBag.Baohanh = tblManu[0].Tag;

            }
            string chuoitag = "";
            if (products.Keyword != null)
            {
                string Chuoi = products.Keyword;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag += "<h2><a href=\"/Tag/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            float Price = float.Parse(products.Price.ToString());
            float PriceSale = float.Parse(products.PriceSale.ToString());
            ViewBag.tietkiem = string.Format("{0:#,#}", Convert.ToInt32(Price - PriceSale));
            //Load tính năng
            ViewBag.tendanhmuc = db.tblGroupProducts.Find(idcate).Name;
            ViewBag.video = "<iframe width = \"100%\" height=\"680\" src=\"https://www.youtube.com/embed/" + db.tblGroupProducts.Find(idcate).Video+ "?rel=0&amp;showinfo=0\" frameborder=\"0\" allowfullscreen></iframe>";
            var ListGroupCri = db.tblGroupCriterias.Where(p => p.idCate == idcate).ToList();
            List<int> Mang1 = new List<int>();
            for (int i = 0; i < ListGroupCri.Count; i++)
            {
                Mang1.Add(int.Parse(ListGroupCri[i].idCri.ToString()));
            }

            int idp = int.Parse(products.id.ToString());
            var ListCri = db.tblCriterias.Where(p => Mang1.Contains(p.id) && p.Active == true).OrderBy(p=>p.Ord).ToList();
            string chuoi = "";
            #region[Lọc thuộc tính]
            for (int i = 0; i < ListCri.Count; i++)
            {
                int idCre = int.Parse(ListCri[i].id.ToString());
                var ListCr = (from a in db.tblConnectCriterias
                              join b in db.tblInfoCriterias on a.idCre equals b.id
                              where a.idpd == idp && b.idCri == idCre && b.Active == true
                              select new
                              {
                                  b.Name,
                                  b.Url,
                                  b.Ord
                              }).OrderBy(p => p.Ord).ToList();
                if (ListCr.Count > 0)
                {
                    if(ListCri[i].Style==true)
                        chuoi += "<tr class=\"bold\">";
                    else
                        chuoi += "<tr>";
               chuoi += "<td>" + ListCri[i].Name + "</td>";
                    chuoi += "<td>";
                    int dem = 0;
                    string num = "";
                    if (ListCr.Count > 1)
                        num = "⊹ ";
                    foreach (var item in ListCr)
                        if (item.Url != null && item.Url != "")
                        {
                            chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">";
                            if (dem == 1)
                                chuoi += num + item.Name;
                            else
                                chuoi += num + item.Name;
                            dem = 1;
                            chuoi += "</a>";
                        }
                        else
                        {
                            if (dem == 1)
                                chuoi += num + item.Name + "</br> ";
                            else
                                chuoi += num + item.Name + "</br> "; ;
                            dem = 1;
                        }
                    chuoi += "</td>";
                    chuoi += " </tr>";
                }
            }
            #endregion
            ViewBag.chuoi = chuoi;
            var listimage = db.tblImageProducts.Where(p => p.idProduct == idp).ToList();
            if (listimage.Count > 0)
            {
                StringBuilder chuoiimages = new StringBuilder();

                chuoiimages.Append(" <a title=\"" + products.Name + "\" onclick=\"javascript:return SetIMG('" + products.ImageLinkDetail + "','an0" + products.id + "');\"><img class=\"an0" + products.id + "\" src=\"" + products.ImageLinkThumb + "\" /></a> ");

                for (int i = 0; i < listimage.Count; i++)
                {

                    chuoiimages.Append(" <a title=\"" + listimage[i].Name + "\" onclick=\"javascript:return SetIMG('" + listimage[i].Images + "','an" + listimage[i].idProduct + "');\"><img class=\"an" + listimage[i].idProduct + "\" src=\"" + listimage[i].Images + "\" /></a> ");

                }

                ViewBag.chuoiimage = chuoiimages;
            }



            ViewBag.hotline = db.tblConfigs.First().HotlineIN;
            var listImages = db.tblImages.Where(p => p.Active == true && p.idCate == 11).OrderByDescending(p => p.Ord).ToList();
            StringBuilder chuoiimage = new StringBuilder();
            for (int i = 0; i < listImages.Count; i++)
            {
                chuoiimage.Append("<a href=\"" + listImages[i].Url + "\" title=\"" + listImages[i].Name + "\"><img src=\"" + listImages[i].Images + "\" alt=\"" + listImages[i].Name + "\" /></a>");
            }
            ViewBag.chuoiimages = chuoiimage;
            string nOng=products.Ong.ToString();
            string nBbo=products.BBO.ToString();
            if(nOng!=null && nOng!="")
            {
                int idOng=int.Parse(nOng);
                ViewBag.ong=db.tblOngs.FirstOrDefault(p=>p.id==idOng).Name;
            }
            if (nBbo != null && nBbo != "")
            {
                int idBbo = int.Parse(nBbo);
                ViewBag.bbo = db.tblBBOes.FirstOrDefault(p => p.id == idBbo).Name;
            }
             return View(products);
        }
        public ActionResult listProduct(string tag, string c)
        {

            var Groupproduct = db.tblGroupProducts.First(p => p.Tag == tag);
            int id = Groupproduct.id;
            ViewBag.h1 = "<h2>" + Groupproduct.Name + "</h2>";
            ViewBag.content = Groupproduct.Content;
            ViewBag.Headshort = Groupproduct.Content;
            ViewBag.Title = "<title>" + Groupproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Groupproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + Groupproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Groupproduct.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Maynuocnongnangluongmattroi.com.vn/" + StringClass.NameToTag(tag) + ".html\" />";
            meta += "<meta itemprop=\"name\" content=\"" + Groupproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + Groupproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + Groupproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://http://Maynuocnongnangluongmattroi.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + Groupproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›" + UrlProduct(id) + "</ol><h1>" + Groupproduct.Title + "</h1>";

            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id).OrderBy(p => p.Ord).ToList();

            //Kiểm tra
            #region[Lọc phần tử]
            List<string> Mangloc = new List<string>();
            List<int> Mangidpp = new List<int>();
            List<int> Mangidp = new List<int>();
            if (c != null && c != "")
            {
                string[] arrayloc = c.Split(',');
                for (int i = 0; i < arrayloc.Length; i++)
                {
                    if (arrayloc[i] != null && arrayloc[i] != "")
                    {
                        Mangidpp.Clear();
                        Mangloc.Add(arrayloc[i]);
                        int idcri = int.Parse(arrayloc[i].ToString());
                        var ListIdProduct = db.tblConnectCriterias.Where(p => p.idCre == idcri).ToList();
                        for (int j = 0; j < ListIdProduct.Count; j++)
                        {
                            int idp = int.Parse(ListIdProduct[j].idpd.ToString());
                            Mangidpp.Add(idp);

                        }
                        if (Mangidp.Count > 0)
                        {
                            for (int j = 0; j < Mangidp.Count; j++)
                            {
                                int idss = Mangidp[j];
                                var kiemtra = Mangidpp.Where(p => Mangidpp.Contains(idss)).ToList();
                                if (kiemtra.Count == 0)
                                {
                                    Mangidp.Remove(idss);
                                }

                            }
                        }
                        else
                        {
                            Mangidp = Mangidpp;

                        }

                    }

                }
            }
            #endregion
            StringBuilder chuoi = new StringBuilder();
            if (ListMenu.Count > 0)
            {
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    int ids = ListMenu[i].id;
                    var listID = db.tblGroupProducts.Where(p => p.ParentID == ids && p.Active == true).ToList();
                    List<string> Mang1 = new List<string>();
                    for (int j = 0; j < listID.Count; j++)
                    {
                        Mang1.Add(listID[j].id.ToString());

                    }
                    Mang1.Add(ids.ToString());
                    var listIdManu = db.tblConnectManuProducts.Where(p => Mang1.Contains(p.idCate.ToString())).Select(p => p.idManu).ToList();
                    var ListManu = db.tblManufactures.Where(p => p.Active == true && listIdManu.Contains(p.id)).OrderBy(p => p.Ord).Take(1).ToList();
                 
                    chuoi.Append(" <div class=\"tear_Product\">");
                    List<int> MangImage = new List<int>();
                    int idCate = ListMenu[i].id;
                    var ListconnectImage = db.tblConnectImages.Where(p => p.idCate == idCate).ToList();
                    for (int j = 0; j < ListconnectImage.Count; j++)
                    {
                        int idm = int.Parse(ListconnectImage[j].idImg.ToString());
                        MangImage.Add(idm);
                    }

                    var listSlide = db.tblImages.Where(p => MangImage.Contains(p.id) && p.Active == true && p.idCate == 2).OrderBy(p => p.Ord).ToList();
                     StringBuilder result = new StringBuilder();
                     StringBuilder result1 = new StringBuilder();
                     for (int j = 0; j < listSlide.Count; j++)
                     {
                         if (j == 0)
                         {
                             result.Append("<div class=\"item active\">");
                             result.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[j].Images + "');\"></div>");
                             result.Append("</div>");
                             result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + j + "\" class=\"active\"></li>");
                         }
                         else
                         {
                             result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + j + "\" ></li>");
                             result.Append("<div class=\"item\">");
                             result.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[j].Images + "');\"></div>");
                             result.Append("</div>");
                         }

                     }
                    if(listSlide.Count>0)
                    {
                        chuoi.Append(" <div class=\"slide_ProductList\">");
                        chuoi.Append(" <div id=\"myCarousel\" class=\"carousel slide\">");
                        chuoi.Append(" <ol class=\"carousel-indicators\">");
                        chuoi.Append(result1);
                        chuoi.Append(" </ol>");
                        chuoi.Append("<div class=\"carousel-inner\">");
                        chuoi.Append(result);
                        chuoi.Append(" </div>");
                        chuoi.Append(" </div>");
                        chuoi.Append(" </div>");
                    }
                    

                    //sdsdsd
                    chuoi.Append(" <div class=\"Clear\"></div>");
                    chuoi.Append(" <h2><a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\">" + ListMenu[i].Name + "</a></h2>");
                    chuoi.Append(" <div class=\"list_TearProduct\">");
                    List<string> Mang = new List<string>();
                    Mang = Arrayid(idCate);
                    Mang.Add(idCate.ToString());
                    var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).Take(10).ToList();
                    if (Mangloc.Count > 0)
                    {
                        if (Mangidp.Count > 0)
                        {
                            listProduct = listProduct.Where(p => Mangidp.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                        }
                        else
                        {
                            listProduct = listProduct.Take(0).ToList();
                        }
                    }


                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        int idp = listProduct[j].id;
                        chuoi.Append("  <div class=\"tear_1\">");
                        chuoi.Append(" <div class=\"img\">");
                        chuoi.Append("<a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\"><img src = \"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                        chuoi.Append(" </div>");
                        chuoi.Append(" <a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                        chuoi.Append(" <div class=\"info\">");
                        string nOng = listProduct[j].Ong.ToString();
                        if (nOng != null && nOng != "")
                        {
                            int idOng = int.Parse(nOng);
                            tblOng tblong = db.tblOngs.FirstOrDefault(p => p.id == idOng);
                            if (tblong != null)
                                chuoi.Append("<span>" + tblong.Name + "</span>");
                        }
                        string nBbo = listProduct[j].BBO.ToString();
                        if (nBbo != null && nBbo != "")
                        {
                            int idBbo = int.Parse(nBbo);
                            tblBBO tblbbo = db.tblBBOes.FirstOrDefault(p => p.id == idBbo);
                            chuoi.Append("<span>BBO: " + tblbbo.Name + "</span>");
                        }
                        chuoi.Append(" </div>");
                        chuoi.Append("<div class=\"box_tear\">");
                        chuoi.Append("<div class=\"box_price\">");
                        chuoi.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                        chuoi.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                        chuoi.Append("</div>");
                        if (ListManu.Count>0)
                        {
                            chuoi.Append("<div class=\"box_logo\">");
                            chuoi.Append(" <a href = \"javascript:void(0)\" title=\"\"><img src = \"" + ListManu[0].Images + "\" alt=\"" + ListManu[0].Name + "\" /></a>");
                            chuoi.Append("</div>");
                        }                      
                        chuoi.Append(" </div>");
                        chuoi.Append("</div>");
                    }    

                     chuoi.Append("</div>");
                     if (listProduct.Count>10)
                     {
                         chuoi.Append("<div class=\"box_Xemthem\">");
                         chuoi.Append("<a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\" class=\"xemthem\"><i>&raquo; &raquo;  Xem thêm nhiều hơn </i></a>");
                         chuoi.Append("</div>");
                     }
                     chuoi.Append("</div>"); Mangphantu.Clear();
                }
            }
            else
            {
                chuoi.Append(" <div class=\"tear_Product\">");
                List<int> MangImage = new List<int>();
                int idCate = Groupproduct.id;
                var ListconnectImage = db.tblConnectImages.Where(p => p.idCate == idCate).ToList();
                for (int j = 0; j < ListconnectImage.Count; j++)
                {
                    int idm = int.Parse(ListconnectImage[j].idImg.ToString());
                    MangImage.Add(idm);
                }
                var ListImage = db.tblImages.Where(p => p.Active == true && MangImage.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                var listSlide = db.tblImages.Where(p => MangImage.Contains(p.id) && p.Active == true && p.idCate==2).OrderBy(p => p.Ord).ToList();
                StringBuilder result = new StringBuilder();
                StringBuilder result1 = new StringBuilder();
                for (int j = 0; j < listSlide.Count; j++)
                {
                    if (j == 0)
                    {
                        result.Append("<div class=\"item active\">");
                        result.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[j].Images + "');\"></div>");
                        result.Append("</div>");
                        result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + j + "\" class=\"active\"></li>");
                    }
                    else
                    {
                        result1.Append(" <li data-target=\"#myCarousel\" data-slide-to=\"" + j + "\" ></li>");
                        result.Append("<div class=\"item\">");
                        result.Append("<div class=\"fill\" style=\"background-image:url('" + listSlide[j].Images + "');\"></div>");
                        result.Append("</div>");
                    }

                }

                if(listSlide.Count>0)
                {
                    chuoi.Append(" <div class=\"slide_ProductList\">");
                    chuoi.Append(" <div id=\"myCarousel\" class=\"carousel slide\">");
                    chuoi.Append(" <ol class=\"carousel-indicators\">");
                    chuoi.Append(result1);
                    chuoi.Append(" </ol>");
                    chuoi.Append("<div class=\"carousel-inner\">");
                    chuoi.Append(result);
                    chuoi.Append(" </div>");
                    chuoi.Append(" </div>");
                    chuoi.Append(" </div>");
                }
                chuoi.Append(" <div class=\"Clear\"></div>");
                chuoi.Append(" <h2><a href=\"/" + Groupproduct.Tag + ".html\" title=\"" + Groupproduct.Name + "\">" + Groupproduct.Name + "</a></h2>");
                chuoi.Append(" <div class=\"list_TearProduct\">");
                List<string> Mang = new List<string>();
                Mang = Arrayid(idCate);
                Mang.Add(idCate.ToString());
                var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).Take(10).ToList();
                if (Mangloc.Count > 0)
                {
                    if (Mangidp.Count > 0)
                    {
                        listProduct = listProduct.Where(p => Mangidp.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                    }
                    else
                    {
                        listProduct = listProduct.Take(0).ToList();
                    }
                }

                var listIdManu = db.tblConnectManuProducts.Where(p =>p.idCate==idCate).Select(p => p.idManu).ToList();
                var ListManu = db.tblManufactures.Where(p => p.Active == true && listIdManu.Contains(p.id)).OrderBy(p => p.Ord).Take(1).ToList();
                for (int j = 0; j < listProduct.Count; j++)
                {
                    int idp = listProduct[j].id;
                    chuoi.Append("  <div class=\"tear_1\">");
                    chuoi.Append(" <div class=\"img\">");
                    chuoi.Append("<a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\"><img src = \"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                    chuoi.Append(" </div>");
                    chuoi.Append(" <a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                    chuoi.Append(" <div class=\"info\">");
                    string nOng = listProduct[j].Ong.ToString();
                    if (nOng != null && nOng != "")
                    {
                        int idOng = int.Parse(nOng);
                        tblOng tblong = db.tblOngs.FirstOrDefault(p => p.id == idOng);
                        if (tblong != null)
                            chuoi.Append("<span>" + tblong.Name + "</span>");
                    }
                    string nBbo = listProduct[j].BBO.ToString();
                    if (nBbo != null && nBbo != "")
                    {
                        int idBbo = int.Parse(nBbo);
                        tblBBO tblbbo = db.tblBBOes.FirstOrDefault(p => p.id == idBbo);
                        chuoi.Append("<span>BBO: " + tblbbo.Name + "</span>");
                    }
                    chuoi.Append(" </div>");
                    chuoi.Append("<div class=\"box_tear\">");
                    chuoi.Append("<div class=\"box_price\">");
                    chuoi.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                    chuoi.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                    chuoi.Append("</div>");
                    if (ListManu.Count>0)
                    {
                        chuoi.Append("<div class=\"box_logo\">");
                        chuoi.Append(" <a href = \"javascript:void(0)\" title=\"\"><img src = \"" + ListManu[0].Images + "\" alt=\"" + ListManu[0].Name + "\" /></a>");
                        chuoi.Append("</div>");
                    }
                  
                    chuoi.Append(" </div>");
                    chuoi.Append("</div>");
                }
                chuoi.Append("</div>");
                chuoi.Append("</div>");     
            }

            ViewBag.chuoi = chuoi;
            List<string> Mangid = new List<string>();
            Mangid = Arrayid(id);
            Mangid.Add(id.ToString());
            List<int> MangImg = new List<int>();
            StringBuilder chuoislide = new StringBuilder();
            var Imageconnectimage = db.tblConnectImages.Where(p => Mangid.Contains(p.idCate.ToString())).ToList();
            for (int j = 0; j < Imageconnectimage.Count; j++)
            {
                int idm = int.Parse(Imageconnectimage[j].idImg.ToString());
                MangImg.Add(idm);
            }
            var ListImages = db.tblImages.Where(p => p.Active == true && MangImg.Contains(p.id) && p.idCate == 10).OrderBy(p => p.Ord).ToList();
            if (ListImages.Count > 0)
            {
                chuoislide.Append("<div id=\"SlideInPoduct\"><div id=\"captioned-gallery\"><figure class=\"slider\">");
                for (int j = 0; j < ListImages.Count; j++)
                {
                    chuoislide.Append("<figure><a href=\"" + ListImages[j].Url + "\" title=\"" + ListImages[j].Name + "\"><img src=\"" + ListImages[j].Images + "\" alt=\"" + ListImages[j].Name + "\" title=\"" + ListImages[j].Name + "\"></a> </figure>");
                }
                chuoislide.Append("  </figure> </div>  </div>");
                ViewBag.chuoislide = chuoislide;
            }

            return View();
        }
        public ActionResult Tag(string tag)
        {
            
            var tbltags = db.tblProductTags.Where(p => p.Tag == tag).ToList();
         
                ViewBag.Title = "<title>" + tbltags[0].Name + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tbltags[0].Name + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"" + tbltags[0].Name + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tbltags[0].Name + "\" /> ";
                string meta = "";
                meta += "<meta itemprop=\"name\" content=\"" + tbltags[0].Name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + tbltags[0].Name + "\" />";
                meta += "<meta itemprop=\"image\" content=\"\" />";
                meta += "<meta property=\"og:title\" content=\"" + tbltags[0].Name + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://Maynuocnongnangluongmattroi.com.vn\" />";
                meta += "<meta property=\"og:description\" content=\"" + tbltags[0].Name + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />";




                var listId = db.tblProductTags.Where(p => p.Tag == tag).Select(p => p.idp).ToList();
            StringBuilder result = new StringBuilder();
            var listProduct = db.tblProducts.Where(p => p.Active == true && listId.Contains(p.id)).OrderBy(p => p.PriceSale).ToList();
            ViewBag.h1 = tbltags[0].Name;
            result.Append("<div class=\"tear_Product\">");
            result.Append(" <h1>" + tbltags[0].Name + "</h1>");
           
           
            result.Append(" <div class=\"list_TearProduct\">");

       

            for (int j = 0; j < listProduct.Count; j++)
            {
                int idp = listProduct[j].id;
                int idcate = int.Parse(listProduct[j].idCate.ToString());
                var listIdManu = db.tblConnectManuProducts.Where(p => p.idCate==idcate).Select(p => p.idManu).ToList();
                var ListManu = db.tblManufactures.Where(p => p.Active == true && listIdManu.Contains(p.id)).OrderBy(p => p.Ord).Take(1).ToList();
                result.Append("  <div class=\"tear_1\">");
                result.Append(" <div class=\"img\">");
                result.Append("<a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\"><img src = \"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                result.Append(" </div>");
                result.Append(" <a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                result.Append(" <div class=\"info\">");
                string nOng = listProduct[j].Ong.ToString();
                if (nOng != null && nOng != "")
                {
                    int idOng = int.Parse(nOng);
                    tblOng tblong = db.tblOngs.FirstOrDefault(p => p.id == idOng);
                    if (tblong != null)
                        result.Append("<span>" + tblong.Name + "</span>");
                }
                string nBbo = listProduct[j].BBO.ToString();
                if (nBbo != null && nBbo != "")
                {
                    int idBbo = int.Parse(nBbo);
                    tblBBO tblbbo = db.tblBBOes.FirstOrDefault(p => p.id == idBbo);
                    result.Append("<span>BBO: " + tblbbo.Name + "</span>");
                }
                result.Append(" </div>");
                result.Append("<div class=\"box_tear\">");
                result.Append("<div class=\"box_price\">");
                result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                result.Append("</div>");

                result.Append("<div class=\"box_logo\">");
                result.Append(" <a href = \"javascript:void(0)\" title=\"\"><img src = \"" + ListManu[0].Images + "\" alt=\"" + ListManu[0].Name + "\" /></a>");
                result.Append("</div>");
                result.Append(" </div>");
                result.Append("</div>");
            }

            //dfdfdf
            result.Append("</div>");
            

            result.Append("</div>");



            ViewBag.chuoi = result.ToString();
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›<li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"" + Request.Url + "\"> <span itemprop=\"name\">" + tbltags[0].Name + "</span></a> <meta itemprop=\"position\" content=\"2\" /> </li> </ol><h1>" + tbltags[0].Name + "</h1";

            return View();
        }
        public ActionResult ListCap(string tag, string c)
        {

            var capacitys = db.tblCapacities.FirstOrDefault(p => p.Tag == tag);
            int idCap = capacitys.id;
            var listIdCap = db.tblCapacityToGroupProducts.Where(p => p.idCap == idCap).Select(p => p.idCate).ToList();

            //var Groupproduct = db.tblGroupProducts.First(p => p.Tag == tag);
            //int id = Groupproduct.id;
            //ViewBag.h1 = "<h2>" + Groupproduct.Name + "</h2>";
            ViewBag.content = capacitys.Content;
            ViewBag.Headshort = capacitys.Content;
            ViewBag.Title = "<title>" + capacitys.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + capacitys.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + capacitys.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + capacitys.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Maynuocnongnangluongmattroi.com.vn/" + StringClass.NameToTag(tag) + ".htm\" />";
            meta += "<meta itemprop=\"name\" content=\"" + capacitys.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + capacitys.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + capacitys.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://maynuocnongnangluongmattroi.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + capacitys.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›<h1>" + capacitys.Title + "</h1>";

            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && listIdCap.Contains(p.id) && p.ParentID==null).OrderBy(p => p.Ord).ToList();

        
            
            StringBuilder chuoi = new StringBuilder();
            if (ListMenu.Count > 0)
            {
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    int ids = ListMenu[i].id;
                    var listID = db.tblGroupProducts.Where(p => p.ParentID == ids && p.Active == true).ToList();
                    List<string> Mang1 = new List<string>();
                    for (int j = 0; j < listID.Count; j++)
                    {
                        Mang1.Add(listID[j].id.ToString());

                    }
                    Mang1.Add(ids.ToString());
                    var listIdManu = db.tblConnectManuProducts.Where(p => Mang1.Contains(p.idCate.ToString())).Select(p => p.idManu).ToList();
                    var ListManu = db.tblManufactures.Where(p => p.Active == true && listIdManu.Contains(p.id)).OrderBy(p => p.Ord).Take(1).ToList();

                    chuoi.Append(" <div class=\"tear_Product\">");     
                    chuoi.Append(" <div class=\"Clear\"></div>");
                    chuoi.Append(" <h2><a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\">" + ListMenu[i].Name + "</a></h2>");
                    chuoi.Append(" <div class=\"list_TearProduct\">");
                    var listIDCate = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == ids && listIdCap.Contains(p.id)).Select(p=>p.id).ToList();
                    List<string> Mang = new List<string>();
                    for(int j=0;j<listIDCate.Count;j++)
                    {
                        Mang.Add(listIDCate[j].ToString());

                    }
                    var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).Take(10).ToList();                 


                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        int idp = listProduct[j].id;
                        chuoi.Append("  <div class=\"tear_1\">");
                        chuoi.Append(" <div class=\"img\">");
                        chuoi.Append("<a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\"><img src = \"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                        chuoi.Append(" </div>");
                        chuoi.Append(" <a href = \"/" + listProduct[j].Tag + "-pd\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                        chuoi.Append(" <div class=\"info\">");
                        string nOng = listProduct[j].Ong.ToString();
                        if (nOng != null && nOng != "")
                        {
                            int idOng = int.Parse(nOng);
                            tblOng tblong = db.tblOngs.FirstOrDefault(p => p.id == idOng);
                            if (tblong != null)
                                chuoi.Append("<span>" + tblong.Name + "</span>");
                        }
                        string nBbo = listProduct[j].BBO.ToString();
                        if (nBbo != null && nBbo != "")
                        {
                            int idBbo = int.Parse(nBbo);
                            tblBBO tblbbo = db.tblBBOes.FirstOrDefault(p => p.id == idBbo);
                            chuoi.Append("<span>BBO: " + tblbbo.Name + "</span>");
                        }
                        chuoi.Append(" </div>");
                        chuoi.Append("<div class=\"box_tear\">");
                        chuoi.Append("<div class=\"box_price\">");
                        chuoi.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                        chuoi.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                        chuoi.Append("</div>");
                        if (ListManu.Count > 0)
                        {
                            chuoi.Append("<div class=\"box_logo\">");
                            chuoi.Append(" <a href = \"javascript:void(0)\" title=\"\"><img src = \"" + ListManu[0].Images + "\" alt=\"" + ListManu[0].Name + "\" /></a>");
                            chuoi.Append("</div>");
                        }
                        chuoi.Append(" </div>");
                        chuoi.Append("</div>");
                    }



                    chuoi.Append("</div>");
                    if (listProduct.Count > 10)
                    {
                        chuoi.Append("<div class=\"box_Xemthem\">");
                        chuoi.Append("<a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\" class=\"xemthem\"><i>&raquo; &raquo;  Xem thêm nhiều hơn </i></a>");
                        chuoi.Append("</div>");
                    }
                    chuoi.Append("</div>"); Mangphantu.Clear();
                }
            }
           
            ViewBag.chuoi = chuoi;
          
            return View();
        }

    }
}