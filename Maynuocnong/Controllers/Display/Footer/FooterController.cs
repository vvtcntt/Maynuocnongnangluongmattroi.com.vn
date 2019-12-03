using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maynuocnong.Models;
using System.Text;
using System.Net.Mail;
 

namespace Maynuocnong.Controllers.Display.Footer
{
    public class FooterController : Controller
    {
        private MaynuocnongContext db = new MaynuocnongContext();
        // GET: Footer
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult footerPartial()
        {
            var listHotline = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for(int i=0;i<listHotline.Count;i++)
            {
             result.Append("<div class=\"Agency_Tear\">");
                result.Append("<span class=\"ts\">"+listHotline[i].Name+"</span>");
                result.Append("<span class=\"dc\">Địa chỉ: " + listHotline[i].Address + "</span>");
                result.Append("<span class=\"dt\">Điện thoại : " + listHotline[i].Mobile + "  </span>");
                result.Append(" <span class=\"dt\">Hotline: <span>"+listHotline[i].Hotline+"</span></span>");
                result.Append("</div>");

            }

            ViewBag.result = result.ToString();
            var Listchinhsach = db.tblNews.Where(p => p.Active == true && p.idCate == 4).OrderBy(p => p.Ord).ToList();
            string chuoichinhsach = "";
            foreach (var item in Listchinhsach)
            {
                chuoichinhsach += "<li><a href=\"/" + item.Tag + "-ns\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
            }
            ViewBag.chinhsach = chuoichinhsach;
            var ListBaogia = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).Take(5).ToList();
            string chuoibaogia = "";
            foreach (var item in ListBaogia)
            {
                chuoibaogia += "<h3><a href=\"/Bao-gia/" + item.Tag + "\" title=\"Báo giá " + item.Name + "\">Báo giá " + item.Name + "</a></h3>";
            }
            ViewBag.chuoibaogia = chuoibaogia;
            var ListGroup = db.tblCapacities.Where(p => p.Active == true  ).OrderBy(p => p.Ord).ToList();
            string chuoi = "";
            foreach (var item in ListGroup)
            {
                chuoi += "<a href=\"/" + item.Tag + ".htm\" title=\"" + item.Name + "\">" + item.Name + "</a>";
             
                int idcate = item.id;
    
            }
            ViewBag.chuoi = chuoi;
            var listUrl = db.tblUrls.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            string Url = "";
            foreach (var item in listUrl)
            {
                Url += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">" + item.Name + "</a>";
            }
            ViewBag.Url = Url; var Imagesadw = db.tblImages.Where(p => p.Active == true && p.idCate == 3).OrderByDescending(p => p.Ord).Take(1).ToList();
            if (Imagesadw.Count > 0)
                ViewBag.Chuoiimg = "<a href=\"" + Imagesadw[0].Url + "\" title=\"" + Imagesadw[0].Name + "\"><img src=\"" + Imagesadw[0].Images + "\" alt=\"" + Imagesadw[0].Name + "\" style=\"max-width:100%;\" /> </a>";
            return PartialView(db.tblConfigs.First());
        }
        public ActionResult CommandCall(string phone, string content)
        {
            string result = "";
            if(phone!=null && phone!="")
            {
             
                var config = db.tblConfigs.First();
                var fromAddress = config.UserEmail;
                string fromPassword = config.PassEmail;
                var toAddress = config.Email;
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
                mailMessage.Subject = "Bạn nhận yêu cầu gọi điện mua máy nước nóng năng lượng mặt trời lúc " + DateTime.Now + "";
                mailMessage.Body = "Số điện thoại " + phone + ", nội dung " + content + "";
                //try
                //{

                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;

                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = fromAddress,
                    Password = fromPassword
                };
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Send(mailMessage);
                result = "Bạn đã yêu cầu gọi điện thành công, bạn vui lòng cầm điện thoại trong khoảng 2-5 phút, chúng tôi sẽ liên hệ với bạn ngay !";
            }
            
            //}
            //catch(Exception ex)
            //{
            //    result = "Rất tiếc hiện chúng tôi không thể gọi cho bạn được, bạn có thể liên hệ qua hotline ở trên !" + ex;
            //}


            return Json(new { result = result });

        }
        public PartialViewResult callPartial()
        {
            return PartialView(db.tblConfigs.First());
        }
    }
}