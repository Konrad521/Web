using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MailAdressController : Controller
    {
        // GET: MailAdress
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Mails()
        {
            using (var db = new db_context())
            {
                var maile = db.Adresy.ToList();
                return PartialView(maile);
            }
        }

        [HttpPost]
        public PartialViewResult Mails(MailAdress model)
        {

            using (var db = new db_context())
            {
                if (model != null)
                {
                    try
                    {
                        System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(model.Adres);
                        db.Adresy.Add(model);
                        db.SaveChanges();
                    }
                    catch (FormatException)
                    {

                    }
                }

                var maile = db.Adresy.ToList();
                return PartialView(maile);
            }
        }
        [HttpGet]
        public ActionResult Remove(MailAdress model)
        {
            using (var db = new db_context())
            {
                    db.Adresy.Attach(model);
                    db.Adresy.Remove(model);

                    db.SaveChanges();

                    return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public ActionResult WyslijMail_Click(Email model)
        {
            using (var db = new db_context())
            {
                string Sub = model.Subject;
                string Bod = model.Body;
                
                if (Sub != null && Bod != null)
                {
                    

                        MailMessage mail = new MailMessage();
                        mail.From = new System.Net.Mail.MailAddress("ppppawlowski149@gmail.com");

                        foreach (var adres in db.Adresy)
                        {
                            mail.To.Add(new System.Net.Mail.MailAddress(adres.Adres));
                        }

                        SmtpClient client = new SmtpClient();
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "smtp.gmail.com";
                        mail.Subject = Sub;
                        mail.Body = Bod;
                        mail.IsBodyHtml = true;
                        client.Credentials = new NetworkCredential("ppppawlowski149@gmail.com", "Abcdefgh123");
                        
                        client.Send(mail);
                    
                }
                return RedirectToAction("Index");
            }
           
        }
    }
}