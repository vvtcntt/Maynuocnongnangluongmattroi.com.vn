using Maynuocnong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Maynuocnong.Controllers.Display.Header
{
    public class HeaderController : Controller
    {
        private MaynuocnongContext db = new MaynuocnongContext();
        // GET: Header
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult headerPartialView()
        {
            tblConfig config = db.tblConfigs.First();
            var listCapacity = db.tblCapacities.Where(p => p.Priority == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listCapacity.Count; i++)
            {
                int idCap = listCapacity[i].id;
                result.Append("<li class=\"li1\"><a href = \"/" + listCapacity[i].Tag + ".htm\" title=\"" + listCapacity[i].Name + "\">" + listCapacity[i].Note + "</a>");
                var listid = db.tblCapacityToGroupProducts.Where(p => p.idCap == idCap).Select(p => p.idCate).ToList();
                var listGroupProduct = db.tblGroupProducts.Where(p => listid.Contains(p.id) && p.Active == true && p.ParentID==null).OrderBy(p => p.Ord).ToList();
                if (listGroupProduct.Count > 0)
                {
                    result.Append(" <div class=\"div_mn\">");
                    result.Append("<div class=\"row\">");
                    result.Append("<ul class=\"ul2\">");
                    string images = "";
                    for (int j = 0; j < listGroupProduct.Count; j++)
                    {
                        int idCate = listGroupProduct[j].id;
                        var listGroupChild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idCate && listid.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                        if (listGroupChild.Count > 0)
                            images = listGroupChild[0].Images;
                        result.Append(" <li class=\"li2\">");
                        result.Append("<a href = \"javascript:void(0)\" title=\"" + listGroupProduct[j].Name + "\"><img src = \"" + images + "\" title=\"" + listGroupProduct[j].Name + "\" /></a> ");
                        result.Append("<a href = \"javascript:void(0)\" title=\"" + listGroupProduct[j].Name + "\">" + listGroupProduct[j].Name + "</a>");                       
                        if (listGroupChild.Count > 0)
                        {
                            result.Append("<ul class=\"ul3\">");
                            for (int k = 0; k < listGroupChild.Count; k++)
                            {
                                result.Append("  <li class=\"li3\">");
                                result.Append(" <a href = \"/" + listGroupChild[k].Tag + ".html\" title=\"" + listGroupChild[k].Name + "\">" + listGroupChild[k].Name + "</a>");
                                result.Append(" </li>");
                        
                            }
                            result.Append(" </ul>");
                          
                        }
                        result.Append(" </li>   ");
                      
                    }
                    result.Append("</ul>");
                    result.Append("</div>");
                    result.Append("</div>");
                }
                result.Append("</li>");
            }
            ViewBag.result = result.ToString();
            return PartialView(config);
        }
    }
}