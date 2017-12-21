using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data;
using Rotativa;

namespace WebApplication1.Controllers
{
    public class OsobyController : Controller
    {
        // GET: Osoby
        //[HttpGet]
        public ActionResult Index(string pole)
        {
            using (var db = new db_context())
            {

                List<Osoba> osoby = Sortuj(db, pole);

                ViewBag.Title = "Osoby na dzień " + DateTime.Now.ToShortDateString();
                ViewData["ObecnyTytul"] = "Osoby na dzień " + DateTime.Now.ToShortDateString();

                return View(osoby);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new db_context())
            {

                Osoba o = db.Osoby.Find(id);
                //o = o == null ? new Osoba() : o;

                return View(o);
            }
        }

        [HttpPost]
        public ActionResult Edit(Osoba model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (var db = new db_context())
                {

                    Osoba o = db.Osoby.Find(model.ID);

                    if (o != null)
                    {
                        foreach (var p in o.GetType().GetProperties())
                        {
                            if(!o.HasPropertyAttribute(p.Name,typeof(KeyAttribute)))
                            {
                                p.SetValue(o, model.GetType().GetProperty(p.Name).GetValue(model));
                            }
                        }
                    }


                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }
        [HttpGet]
        public ActionResult Add()
        {
            using (var db = new db_context())
            {
                Osoba o = new Osoba();

                o = db.Osoby.Add(o);
                //o = o == null ? new Osoba() : o;

                return View(o);

            }
        }
        [HttpPost]
        public ActionResult Add(Osoba model)
        {
            if (ModelState.IsValid)
            {

                using (var db = new db_context())
                {

                    db.Osoby.Add(model);

                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }


        }

        public ActionResult Details(int IDOsoby)
        {

            using (var db = new db_context())
            {
                Osoba o = db.Osoby.Find(IDOsoby);
                return View(o);
            }
        }

        [HttpGet]
        public ActionResult Remove(Osoba model)
        {
            using (var db = new db_context())
            {
                if (model != null)
                {
                    db.Osoby.Attach(model); //podlacz model do bazy na sile
                    db.Osoby.Remove(model);

                    var nieruchmosci = db.Nieruchomosci.Where(x => x.IDOsoby == model.ID);

                    foreach (var n in nieruchmosci)
                    {
                        db.Nieruchomosci.Remove(n);
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string filename = file.FileName;
                string targetpath = Server.MapPath("~/App_Data/");
                file.SaveAs(targetpath + filename);
                string pathToExcelFile = targetpath + filename;
                string connectionString = "";
                if (file.FileName.EndsWith(".xls"))
                {
                    connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                }
                else if (file.FileName.EndsWith(".xlsx"))
                {
                    connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                }
                else
                {
                    return RedirectToAction("Index");
                }

                var adapter = new OleDbDataAdapter("Select * FROM [Arkusz1$]", connectionString);
                var ds = new DataSet();
                adapter.Fill(ds, "ExcelTable");
                DataTable dtable = ds.Tables["ExcelTable"];

                using (var db = new db_context())
                {

                    foreach (DataRow dr in dtable.Rows)
                    {
                        Osoba o = new Osoba();

                        o.Imie = dr[0].ToString();
                        o.Nazwisko = dr[1].ToString();
                        o.Wiek = Int32.Parse(dr[2].ToString());
                        o.Zdjecie = dr[3].ToString();

                        db.Osoby.Add(o);
                    }

                    db.SaveChanges();
                }

                if ((System.IO.File.Exists(pathToExcelFile)))
                {
                    System.IO.File.Delete(pathToExcelFile);
                }
            }

            return RedirectToAction("Index");
        }

        private static List<Osoba> Sortuj(db_context db, string pole)
        {
            pole = pole != null ? pole : "";

            PropertyInfo prop = typeof(Osoba).GetProperty(pole);

            if (prop != null)
            {
                return db.Osoby.ToList().OrderBy(x => prop.GetValue(x, null)).ToList();
            }
            else
            {
                return db.Osoby.ToList();
            }
        }

        public ActionResult Pdf(int IDOsoby)
        {
            return new ActionAsPdf("Details", new { IDOsoby = IDOsoby });
        }

    }
}