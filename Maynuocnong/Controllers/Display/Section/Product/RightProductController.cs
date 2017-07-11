using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maynuocnong.Models;
using System.Text;
namespace Maynuocnong.Controllers.Display.Section.Product
{
    public class RightProductController : Controller
    {
        private MaynuocnongContext db = new MaynuocnongContext();

        //
        // GET: /RightProduct/
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
        public PartialViewResult partialLeftCap(string tag, string c)
        {
            var capacity = db.tblCapacities.FirstOrDefault(p => p.Tag == tag);
            int idCap = capacity.id;
            var listid = db.tblCapacityToGroupProducts.Where(p => p.idCap == idCap).Select(p => p.idCate).ToList();

            var ListMenu = db.tblGroupProducts.Where(p => listid.Contains(p.id) && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            StringBuilder chuoi = new StringBuilder();
             if (ListMenu.Count > 0)
            {
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    chuoi.Append("<li class=\"li_1\">");
                    chuoi.Append("<a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\" class=\"Name1\">" + ListMenu[i].Name + "</a>");
                    int idCate = ListMenu[i].id;
                    var listMenuChild = db.tblGroupProducts.Where(p =>listid.Contains(p.id) && p.ParentID == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                    if (listMenuChild.Count > 0)
                    {
                        chuoi.Append("<ul class=\"ul_2\">");
                        for (int j = 0; j < listMenuChild.Count; j++)
                        {
                            chuoi.Append("<li class=\"li_2\"><a href=\"/" + listMenuChild[j].Tag + "-html\" title=\"" + listMenuChild[j].Name + "\">› " + listMenuChild[j].Name + "</a></li>");
                        }

                        chuoi.Append("</ul>");
                    }

                    chuoi.Append("</li>");
                }
            }
            


            ViewBag.chuoi = chuoi;

            //Danh sách
   
            return PartialView();
        }
        public PartialViewResult partialLeftListProduct(string tag,string c)
        {
            var Groupproduct = db.tblGroupProducts.FirstOrDefault(p => p.Tag == tag);
            StringBuilder chuoi = new StringBuilder();
            int id=Groupproduct.id;
            var ListMenu=db.tblGroupProducts.Where(p=>p.Active==true && p.ParentID==id).OrderBy(p=>p.Ord).ToList();
            if(ListMenu.Count>0)
            {
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    chuoi.Append("<li class=\"li_1\">");
                    chuoi.Append("<a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\" class=\"Name1\">" + ListMenu[i].Name + "</a>");
                    int idCate = ListMenu[i].id;
                    var listMenuChild = db.tblGroupProducts.Where(p => p.ParentID == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                    if (listMenuChild.Count > 0)
                    {
                        chuoi.Append("<ul class=\"ul_2\">");
                        for (int j = 0; j < listMenuChild.Count; j++)
                        {
                            chuoi.Append("<li class=\"li_2\"><a href=\"/" + listMenuChild[j].Tag + ".html\" title=\"" + listMenuChild[j].Name + "\">› " + listMenuChild[j].Name + "</a></li>");
                        }

                        chuoi.Append("</ul>");
                    }

                    chuoi.Append("</li>");
                }
            }
            else
            {
                chuoi.Append("<li class=\"li_1\">");
                chuoi.Append("<a href=\"/" + Groupproduct.Tag + ".html\" title=\"" + Groupproduct.Name + "\" class=\"Name1\">" + Groupproduct.Name + "</a>"); chuoi.Append("</li>");
            }
            

            ViewBag.chuoi = chuoi;

            //Danh sách
            #region[Lọc danh mục]
            List<string> Mang = new List<string>();
            Arrayid(Groupproduct.id).Add(Groupproduct.id.ToString());
            Mang = Arrayid(Groupproduct.id);
            var ListGroupCre = db.tblGroupCriterias.Where(p => Mang.Contains(p.idCate.ToString())).ToList();
            List<int> Mangid = new List<int>();
            for (int i = 0; i < ListGroupCre.Count; i++)
            {
                Mangid.Add(int.Parse(ListGroupCre[i].idCri.ToString()));
            }
            var listCre = db.tblCriterias.Where(p => Mangid.Contains(p.id) && p.Priority == true && p.Active == true).OrderBy(p => p.Ord).ToList();
            string chuoicre = "";
            for (int i = 0; i < listCre.Count; i++)
            {
                chuoicre += "<span class=\"Name2\">" + listCre[i].Name + "</span>";
                chuoicre += "<span class=\"linemn\"></span>";
                if (listCre[i].Style == true)
                    chuoicre += "<div class=\"Content_loc1\"> ";
                else
                    chuoicre += "<div class=\"Content_loc\"> ";
                int idCri = int.Parse(listCre[i].id.ToString());
                var listInfoCri = db.tblInfoCriterias.Where(p => p.Active == true && p.idCri == idCri).ToList();
                for (int j = 0; j < listInfoCri.Count; j++)
                {
                    string root = "c=" + c + "," + listInfoCri[j].id + "";
                    int idInfocre = int.Parse(listInfoCri[j].id.ToString());
                    if (c != null)
                    {
                        string[] kiemtra = c.Split(',');

                        if (kiemtra.Contains(idInfocre.ToString()))
                        {
                            string root1 = "";

                            for (int k = 0; k < kiemtra.Length; k++)
                            {
                                if (kiemtra[k] != "")
                                {
                                    int idkt = int.Parse(kiemtra[k].ToString());
                                    if (idkt != idInfocre)
                                    {
                                        root1 += idkt + ",";
                                    }
                                }
                            }
                            chuoicre += "<a class=\"active\" rel=\"nofollow\" href=\"/" + tag + "-lp?c=" + root1 + "\"  title=\"\">" + listInfoCri[j].Name + "</a>";
                        }
                        else
                        {
                            chuoicre += "<a href=\"/" + tag + "-lp?" + root + "\"  rel=\"nofollow\" title=\"\">" + listInfoCri[j].Name + "</a>";

                        }

                    }
                    else
                    {
                        chuoicre += "<a href=\"/" + tag + "-lp?" + root + "\"  rel=\"nofollow\"  title=\"\">" + listInfoCri[j].Name + "</a>";

                    }

                }

                chuoicre += "</div>";
                chuoicre += "<div class=\"Clear\"></div>";
            }
            ViewBag.chuoici = chuoicre;
            #endregion
            return PartialView();
        }
        public PartialViewResult partialLeftCapacity(string tag, string c,string hang)
        {
            var Capacity = db.tblCapacities.FirstOrDefault(p => p.Tag == tag);
            StringBuilder chuoi = new StringBuilder();
            int id = Capacity.id;
            var listProduct = (from a in db.tblProducts where a.Capacity==id select a.idCate).ToList();
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && listProduct.Contains(p.id)).OrderBy(p => p.Ord).ToList();
            
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    chuoi.Append("<li class=\"li1\">");
                    chuoi.Append("<a href=\"/" + ListMenu[i].Tag + "-lp\" title=\"" + ListMenu[i].Name + "\" class=\"Name1\">" + ListMenu[i].Name + "</a>");
                    int idCate = ListMenu[i].id;
                    var listMenuChild = db.tblGroupProducts.Where(p => p.ParentID == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                    if (listMenuChild.Count > 0)
                    {
                        chuoi.Append("<ul class=\"ul2\">");
                        for (int j = 0; j < listMenuChild.Count; j++)
                        {
                            chuoi.Append("<li class=\"li2\"><a href=\"/" + listMenuChild[j].Tag + "-lp\" title=\"" + listMenuChild[j].Name + "\">› " + listMenuChild[j].Name + "</a></li>");
                        }

                        chuoi.Append("</ul>");
                    }

                    chuoi.Append("</li>");
                }
            ViewBag.chuoi = chuoi;
            #region[Lọc danh mục]
            //Lọc hãng sản xuất
            StringBuilder chuoihang = new StringBuilder();
            var ListMangManu = (from a in db.tblConnectManuProducts where listProduct.Contains(a.idCate) select a.idManu).ToList();

            var listManu = db.tblManufactures.Where(p => p.Active == true && ListMangManu.Contains(p.id)).OrderBy(p => p.Ord).ToList();
            if (listManu.Count > 0)
            {
                chuoihang.Append("<span class=\"Name3\">Hãng sản xuất</span>");
              chuoihang.Append("<span class=\"linemn\"></span>");
                chuoihang.Append("<div class=\"Content_Hangsanxuat\">");
                for (int i = 0; i < listManu.Count; i++)
                {
                    if (listManu[i].Tag == hang)
                    {
                        chuoihang.Append("<a class=\"active\" rel=\"nofollow\"  href=\"/" + tag + ".html/" + listManu[i].Tag + "\" title=\"" + listManu[i].Name + "\">›› " + listManu[i].Name + "</a>");
                    }
                    else
                    {
                       chuoihang.Append("<a class=\"number\" rel=\"nofollow\" href=\"/" + tag + ".html/" + listManu[i].Tag + "\" title=\"" + listManu[i].Name + "\">›› " + listManu[i].Name + "</a>");
                    }
                }
               chuoihang.Append("</div>");
            }
            ViewBag.chuoihang = chuoihang; 
            //Lọc thuộc tính
            string hangthuoctinh = "";
            if (hang != null && hang != "")
                hangthuoctinh = "/" + hang ;

            var ListGroupCre = db.tblGroupCriterias.Where(p =>listProduct.Contains(p.idCate)).ToList();
            List<int> Mangid = new List<int>();
            for (int i = 0; i < ListGroupCre.Count; i++)
            {
                Mangid.Add(int.Parse(ListGroupCre[i].idCri.ToString()));
            }
            var listCre = db.tblCriterias.Where(p => Mangid.Contains(p.id) && p.Priority == true && p.Active == true).OrderBy(p => p.Ord).ToList();
            string chuoicre = "";
            for (int i = 0; i < listCre.Count; i++)
            {
                chuoicre += "<span class=\"Name2\">" + listCre[i].Name + "</span>";
                chuoicre += "<span class=\"linemn\"></span>";
                if (listCre[i].Style == true)
                    chuoicre += "<div class=\"Content_loc1\"> ";
                else
                    chuoicre += "<div class=\"Content_loc\"> ";
                int idCri = int.Parse(listCre[i].id.ToString());
                var listInfoCri = db.tblInfoCriterias.Where(p => p.Active == true && p.idCri == idCri).ToList();
                for (int j = 0; j < listInfoCri.Count; j++)
                {
                    string root = "c=" + c + "," + listInfoCri[j].id + "";
                    int idInfocre = int.Parse(listInfoCri[j].id.ToString());
                    if (c != null)
                    {
                        string[] kiemtra = c.Split(',');

                        if (kiemtra.Contains(idInfocre.ToString()))
                        {
                            string root1 = "";

                            for (int k = 0; k < kiemtra.Length; k++)
                            {
                                if (kiemtra[k] != "")
                                {
                                    int idkt = int.Parse(kiemtra[k].ToString());
                                    if (idkt != idInfocre)
                                    {
                                        root1 += idkt + ",";
                                    }
                                }
                            }
                            chuoicre += "<a class=\"active\" rel=\"nofollow\" href=\"/" + tag + ".html"+hangthuoctinh+"?c=" + root1 + "\"  title=\"\">" + listInfoCri[j].Name + "</a>";
                        }
                        else
                        {
                            chuoicre += "<a href=\"/" + tag + ".html" + hangthuoctinh + "?" + root + "\"  rel=\"nofollow\" title=\"\">" + listInfoCri[j].Name + "</a>";

                        }

                    }
                    else
                    {
                        chuoicre += "<a href=\"/" + tag + ".html" + hangthuoctinh + "?" + root + "\"  rel=\"nofollow\"  title=\"\">" + listInfoCri[j].Name + "</a>";

                    }

                }

                chuoicre += "</div>";
                chuoicre += "<div class=\"Clear\"></div>";
            }
            ViewBag.chuoici = chuoicre;
            #endregion
              
            return PartialView();
        }
        public PartialViewResult partialLeftManufactures(string tag, string c, string hang)
        {
            var Manufactures = db.tblManufactures.FirstOrDefault(p => p.Tag == tag);
            StringBuilder chuoi = new StringBuilder();
            int id = Manufactures.id;
            var ListidMenu = (from a in db.tblConnectManuProducts where a.idManu == id select a.idCate).ToList();
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && ListidMenu.Contains(p.id)).OrderBy(p => p.Ord).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                chuoi.Append("<li class=\"li1\">");
                chuoi.Append("<a href=\"/" + ListMenu[i].Tag + "-lp\" title=\"" + ListMenu[i].Name + "\" class=\"Name1\">" + ListMenu[i].Name + "</a>");
                int idCate = ListMenu[i].id;
                var listMenuChild = db.tblGroupProducts.Where(p => p.ParentID == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                if (listMenuChild.Count > 0)
                {
                    chuoi.Append("<ul class=\"ul2\">");
                    for (int j = 0; j < listMenuChild.Count; j++)
                    {
                        chuoi.Append("<li class=\"li2\"><a href=\"/" + listMenuChild[j].Tag + "-lp\" title=\"" + listMenuChild[j].Name + "\">› " + listMenuChild[j].Name + "</a></li>");
                    }

                    chuoi.Append("</ul>");
                }

                chuoi.Append("</li>");
            }
            ViewBag.chuoi = chuoi;
            #region[Lọc danh mục]
            //Lọc hãng sản xuất
            StringBuilder chuoihang = new StringBuilder();
            var ListMangManu = (from a in db.tblConnectManuProducts where ListidMenu.Contains(a.idCate) select a.idManu).ToList();

            var listManu = db.tblManufactures.Where(p => p.Active == true && ListMangManu.Contains(p.id)).OrderBy(p => p.Ord).ToList();
            if (listManu.Count > 0)
            {
                chuoihang.Append("<span class=\"Name3\">Hãng sản xuất</span>");
                chuoihang.Append("<span class=\"linemn\"></span>");
                chuoihang.Append("<div class=\"Content_Hangsanxuat\">");
                for (int i = 0; i < listManu.Count; i++)
                {
                    if (listManu[i].Tag == hang)
                    {
                        chuoihang.Append("<a class=\"active\" rel=\"nofollow\"  href=\"/" + tag + ".html/" + listManu[i].Tag + "\" title=\"" + listManu[i].Name + "\">›› " + listManu[i].Name + "</a>");
                    }
                    else
                    {
                        chuoihang.Append("<a class=\"number\" rel=\"nofollow\" href=\"/" + tag + ".html/" + listManu[i].Tag + "\" title=\"" + listManu[i].Name + "\">›› " + listManu[i].Name + "</a>");
                    }
                }
                chuoihang.Append("</div>");
            }
            ViewBag.chuoihang = chuoihang;
            //Lọc thuộc tính
            string hangthuoctinh = "";
            if (hang != null && hang != "")
                hangthuoctinh = "/" + hang;

            var ListGroupCre = db.tblGroupCriterias.Where(p => ListidMenu.Contains(p.idCate)).ToList();
            List<int> Mangid = new List<int>();
            for (int i = 0; i < ListGroupCre.Count; i++)
            {
                Mangid.Add(int.Parse(ListGroupCre[i].idCri.ToString()));
            }
            var listCre = db.tblCriterias.Where(p => Mangid.Contains(p.id) && p.Priority == true && p.Active == true).OrderBy(p => p.Ord).ToList();
            string chuoicre = "";
            for (int i = 0; i < listCre.Count; i++)
            {
                chuoicre += "<span class=\"Name2\">" + listCre[i].Name + "</span>";
                chuoicre += "<span class=\"linemn\"></span>";
                if (listCre[i].Style == true)
                    chuoicre += "<div class=\"Content_loc1\"> ";
                else
                    chuoicre += "<div class=\"Content_loc\"> ";
                int idCri = int.Parse(listCre[i].id.ToString());
                var listInfoCri = db.tblInfoCriterias.Where(p => p.Active == true && p.idCri == idCri).ToList();
                for (int j = 0; j < listInfoCri.Count; j++)
                {
                    string root = "c=" + c + "," + listInfoCri[j].id + "";
                    int idInfocre = int.Parse(listInfoCri[j].id.ToString());
                    if (c != null)
                    {
                        string[] kiemtra = c.Split(',');

                        if (kiemtra.Contains(idInfocre.ToString()))
                        {
                            string root1 = "";

                            for (int k = 0; k < kiemtra.Length; k++)
                            {
                                if (kiemtra[k] != "")
                                {
                                    int idkt = int.Parse(kiemtra[k].ToString());
                                    if (idkt != idInfocre)
                                    {
                                        root1 += idkt + ",";
                                    }
                                }
                            }
                            chuoicre += "<a class=\"active\" rel=\"nofollow\" href=\"/" + tag + ".html" + hangthuoctinh + "?c=" + root1 + "\"  title=\"\">" + listInfoCri[j].Name + "</a>";
                        }
                        else
                        {
                            chuoicre += "<a href=\"/" + tag + ".html" + hangthuoctinh + "?" + root + "\"  rel=\"nofollow\" title=\"\">" + listInfoCri[j].Name + "</a>";

                        }

                    }
                    else
                    {
                        chuoicre += "<a href=\"/" + tag + ".html" + hangthuoctinh + "?" + root + "\"  rel=\"nofollow\"  title=\"\">" + listInfoCri[j].Name + "</a>";

                    }

                }

                chuoicre += "</div>";
                chuoicre += "<div class=\"Clear\"></div>";
            }
            ViewBag.chuoici = chuoicre;
            #endregion

            return PartialView();
        }

        public PartialViewResult RightTag()
        {
            StringBuilder chuoi = new StringBuilder();
             var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==null).OrderBy(p => p.Ord).ToList();
           
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    chuoi.Append("<li class=\"li1\">");
                    chuoi.Append("<a href=\"/" + ListMenu[i].Tag + "-lp\" title=\"" + ListMenu[i].Name + "\" class=\"Name1\">" + ListMenu[i].Name + "</a>");
                    int idCate = ListMenu[i].id;
                    var listMenuChild = db.tblGroupProducts.Where(p => p.ParentID == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                    if (listMenuChild.Count > 0)
                    {
                        chuoi.Append("<ul class=\"ul2\">");
                        for (int j = 0; j < listMenuChild.Count; j++)
                        {
                            chuoi.Append("<li class=\"li2\"><a href=\"/" + listMenuChild[j].Tag + "-lp\" title=\"" + listMenuChild[j].Name + "\">› " + listMenuChild[j].Name + "</a></li>");
                        }

                        chuoi.Append("</ul>");
                    }

                    chuoi.Append("</li>");
                }
                ViewBag.chuoi = chuoi;
            return PartialView();
        }
	}
}