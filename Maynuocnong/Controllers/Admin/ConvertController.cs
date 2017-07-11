using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maynuocnong.Models;
namespace Maynuocnong.Controllers.Admin
{
    public class ConvertController : Controller
    {
        MaynuocnongContext db = new MaynuocnongContext();
        // GET: Convert
        public ActionResult Index()
        {
            var listProduct = db.tblNews.ToList();
            foreach(var item in listProduct)
            {
                int id=item.id;
                tblNew tblproduct = db.tblNews.Find(id);
                tblproduct.Tag = StringClass.NameToTag(tblproduct.Name);
                db.SaveChanges();
            }
            var listProducts = db.tblGroupNews.ToList();
            foreach (var item in listProducts)
            {
                int id = item.id;
                tblGroupNew tblproduct = db.tblGroupNews.Find(id);
                tblproduct.Tag = StringClass.NameToTag(tblproduct.Name);
                db.SaveChanges();
            }
            var listProductss = db.tblAgencies.ToList();
            foreach (var item in listProductss)
            {
                int id = item.id;
                tblAgency tblproduct = db.tblAgencies.Find(id);
                tblproduct.Tag = StringClass.NameToTag(tblproduct.Name);
                db.SaveChanges();
            }
            var listProducstsss = db.tblGroupAgencies.ToList();
            foreach (var item in listProducstsss)
            {
                int id = item.id;
                tblGroupAgency tblproduct = db.tblGroupAgencies.Find(id);
                tblproduct.Tag = StringClass.NameToTag(tblproduct.Name);
                db.SaveChanges();
            }
            return View();
        }
    }
}