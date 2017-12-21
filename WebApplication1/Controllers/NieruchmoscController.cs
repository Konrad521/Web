using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NieruchmoscController : Controller
    {
        // GET: Nieruchmosc
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //[ActionName("Dodaj")]
        public ActionResult Add(int IDOsoby)
        {
            return View(new Nieruchomosc() { IDOsoby = IDOsoby });

        }

        [HttpPost]
        public ActionResult Add(Nieruchomosc model)
        {
            using (var db = new db_context())
            {
                db.Nieruchomosci.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Osoby");
            }
            
        }

        //[HttpGet]
        //[ActionName("Pokaz")] nadpisanie nazwy
        //[NonAction] ukrycie akcji
        public JsonResult GetNieruchomosci(int IDOsoby)
        {
            using (var db = new db_context())
            {
                var nieruchomosci = db.Nieruchomosci.Where(x => x.IDOsoby == IDOsoby).ToList();

                return Json(nieruchomosci, JsonRequestBehavior.AllowGet);
            }
        }

        //[FiltrAttribute]
        public ActionResult Map(int ID)
        {
            return View(ID);
        }

        public PartialViewResult MapPartial(int ID)
        {
            using (var db = new db_context())
            {
                var nieruchomosc = db.Nieruchomosci.FirstOrDefault(x => x.ID == ID);

                return PartialView(nieruchomosc);
            }
        }

        [HttpPost]
       public ActionResult UploadFile(int ID,HttpPostedFileBase file)
       {
            //string content = new StreamReader(file.InputStream).ReadToEnd();
            var fileName = Path.GetFileName(file.FileName);
            string folder = @"c:\Nieruchomosci\" + ID;

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            file.SaveAs(Path.Combine(folder, fileName));

            WyslijMail(fileName);
               

            return RedirectToAction("Map", new { ID = ID });
       }

        private void WyslijMail(string fileName)
        {
            using (var db = new db_context())
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
                mail.Subject = "Dodano plik" + fileName;
                mail.Body = "Dodano plik" + fileName;
                client.Credentials = new NetworkCredential("ppppawlowski149@gmail.com", "Abcdefgh123");
                client.Send(mail);
            }

        }

        public PartialViewResult Pliki(int ID)
        {
            List<string> pliki = new List<string>();
            string folder = @"c:\Nieruchomosci\" + ID;
            if (Directory.Exists(folder))
            {
                pliki = Directory.GetFiles(folder).Select(x=>x.Split(new string[] { @"\" },StringSplitOptions.None).Last()).ToList();
            }
            ViewBag.IDN = ID;
            return PartialView(pliki);
        }

        public FileResult PobierzPlik(int ID, string nazwa)
        {
            string folder = @"c:\Nieruchomosci\" + ID;
            byte[] fileBytes = System.IO.File.ReadAllBytes(folder + @"\" + nazwa);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nazwa);
        }
        public ActionResult UsunPlik(int ID, string nazwa)
        {
            string folder = @"c:\Nieruchomosci\" + ID;
            System.IO.File.Delete(folder + @"\" + nazwa);

            return RedirectToAction("Map", new { ID = ID });
        }
    }
}