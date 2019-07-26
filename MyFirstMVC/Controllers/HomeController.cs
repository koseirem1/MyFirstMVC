using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Projects()
        {
           using(var db =new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);
            }
           
        }

        public ActionResult Contact() //sayfa ilk görüntülendiğinde bu method çalışır
        {
            ViewBag.Message = "Aşağıdaki formu kullanarak bizimle iletişime geçebilirsiniz.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel model)//formu gönderince bu method çalışır. validasyonlar gerçekleşir.
        {
            if (ModelState.IsValid)
            {
                //TODO:mail gönder
                {
                    try
                    {
                        System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.From = new System.Net.Mail.MailAddress("koseirem1@gmail.com", "İrem Köse");
                        mailMessage.Subject = "İletişim Formu: " + model.FirstName;

                        mailMessage.To.Add("koseirem1@gmail.com,koseirem1@gmail.com");

                        string body;
                        body = "Ad: " + model.FirstName + "<br />";
                        body += "Soyad: " + model.LastName + "<br />";
                        body += "Telefon: " + model.Telephone + "<br />";
                        body += "E-posta: " + model.Email + "<br />";

                        body += "Mesaj: " + model.Message + "<br />";
                        body += "Tarih: " + DateTime.Now.ToString("dd MMMM yyyy") + "<br />";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = body;

                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                        smtp.Credentials = new System.Net.NetworkCredential("koseirem1@gmail.com", "sifre");
                        smtp.EnableSsl = true;
                        smtp.Send(mailMessage);
                        ViewBag.Message = "Mesajınız gönderildi. Teşekkür ederiz.";
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Error = "Form gönderilirken bir hata oluştu. Tekrar deneyiniz.";
                    }
                }

            }
            return View(model);
        }

        public ActionResult Kvkk()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}