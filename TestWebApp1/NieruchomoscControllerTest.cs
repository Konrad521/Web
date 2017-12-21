using System;
using WebApplication1.Controllers;
using System.Web.Mvc;
using NUnit.Framework;
using WebApplication1.Models;
using System.Linq;

namespace TestWebApp1
{
    [TestFixture]
    public class NieruchomoscControllerTest
    {
        [SetUp]
        public void Start()
        {
            using (var db = new db_context())
            {
                db.Osoby.Add(new Osoba("Pawel", "Kajka", 25));
                db.SaveChanges();
            }
        }

        [TearDown]
        public void End()
        {
            using (var db = new db_context())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Table_1]");
            }
        }

        [Test]
        public void IndexTest()
        {
            var ctrl = new NieruchmoscController();
            var index = ctrl.Index();

            Assert.IsInstanceOf(typeof(ViewResult), index);

        }

        [Test]
        public void AddGetTest()
        {
            using (var db = new db_context())
            {
                int id = db.Osoby.First().ID;
                var ctrl = new NieruchmoscController();
                var add = ctrl.Add(id);                       //add Nie pobiera lczby argumentów

                Assert.IsInstanceOf(typeof(ViewResult), add);
            }
        } 

        [Test]
        public void AddPostTest()
        {
            using (var db = new db_context())
            {
                Nieruchomosc model = new Nieruchomosc
                {
                    IDOsoby = 1,
                    Nazwa = "Dom",
                    Adres = "Orkana 27",
                    lat = 21.00000,
                    lng = 52.06543
                };
                var ctrl = new NieruchmoscController();
                var add = ctrl.Add(model);

                bool exist = db.Nieruchomosci.Any(x => x.IDOsoby == model.IDOsoby && x.Nazwa == model.Nazwa && x.Adres == model.Adres 
                && x.lat == model.lat && x.lng == model.lng);
                Assert.IsTrue(exist);
            }
        } 
        [Test]
        public void GetJsonTest()
        {
            using (var db = new db_context())
            {
                var ctrl = new NieruchmoscController();
                int id = db.Osoby.First().ID;
                var json = ctrl.GetNieruchomosci(id);

                Assert.IsInstanceOf(typeof(JsonResult),json);
            }
        }
        /*[Test]
        public void GetJsonTest()
        {
            using (var db = new db_context())
            {
                var ctrl = new NieruchmoscController();
                JsonResult json = ctrl.Json;                //Json jest niedostęny, mimo bycia w obiekcie publicznym


                Assert.IsInstanceOf(typeof(JsonResult), json);
            }
        } */
        [Test]
        public void MapTest()
        {
            using (var db = new db_context())
            {
                var ctrl = new NieruchmoscController();
                int ID = 0;
                var nieruchomosc = db.Nieruchomosci.FirstOrDefault(x => x.ID == ID);
                var map = ctrl.Map(ID);

                Assert.IsInstanceOf(typeof(ViewResult), map);
            }
        }
        [Test]
        public void MapPartialTest()
        {
            using (var db = new db_context())
            {
                var ctrl = new NieruchmoscController();
                int ID = 0;
                var nieruchomosc = db.Nieruchomosci.FirstOrDefault(x => x.ID == ID);
                var map = ctrl.MapPartial(ID);                    

                Assert.IsInstanceOf(typeof(PartialViewResult), map);
            }
        }
    }
}