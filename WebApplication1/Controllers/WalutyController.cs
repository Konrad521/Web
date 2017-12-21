using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class WalutyController : Controller
    {
        // GET: Waluty
        public ActionResult Index(string kod)
        {

            FixerAPIModel model = GetDataFixer(kod);
            return View(model);
        }

        private FixerAPIModel GetDataFixer(string kod)
        {
            using (WebClient client = new WebClient())
            {
                var url = "https://api.fixer.io/latest";

                if (kod != null)
                {
                    url = url + "?base=" + kod;
                }

                var stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);
                string s = reader.ReadToEnd();

                FixerAPIModel model = JsonConvert.DeserializeObject<FixerAPIModel>(s);

                stream.Close();

                return model;
            }


        }

        public JsonResult Przelicz(decimal kwota, string code)
        {
            FixerAPIModel model = GetDataFixer(code);
            decimal rate = model.rates["PLN"];
            decimal result = kwota*rate;

            return Json(new { kwota = result },JsonRequestBehavior.AllowGet);
        }
    }
}