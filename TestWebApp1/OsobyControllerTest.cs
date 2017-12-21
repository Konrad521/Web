using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models;

namespace TestWebApp1
{
    [TestFixture]
    public class OsobyControllerTest
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
            var ctrl = new OsobyController();
            var index = ctrl.Index(null);

            Assert.IsInstanceOf(typeof(ViewResult), index);
        }

        [Test]
        public void EditGetTest()
        {
            using (var db = new db_context())
            {
                int id = db.Osoby.First().ID;
                var ctrl = new OsobyController();
                var edit = ctrl.Edit(id);

                Assert.IsInstanceOf(typeof(ViewResult), edit);
            }
        }

        [Test]
        public void EditPostTest()
        {
            using (var db = new db_context())
            {
                Osoba model = db.Osoby.First();
                model.Imie = "Zacheusz";
                model.Nazwisko = "Olek";
                model.Wiek = 99;
                var ctrl = new OsobyController();
                var edit = ctrl.Edit(model);

                var modelUpdated = db.Osoby.First(x => x.ID == model.ID);
                Assert.IsTrue(modelUpdated.Imie == model.Imie && modelUpdated.Nazwisko == model.Nazwisko && modelUpdated.Wiek == model.Wiek);
            }
        }

        [Test]
        public void EditPostTypeTest()
        {
            using (var db = new db_context())
            {
                Osoba model = db.Osoby.First();
                model.Imie = "Zacheusz";
                model.Nazwisko = "Olek";
                model.Wiek = 99;
                var ctrl = new OsobyController();
                var edit = ctrl.Edit(model);

                Assert.IsInstanceOf(typeof(ActionResult), edit);
            }
        }
        [Test]
        public void AddGetTest()
        {
            using (var db = new db_context())
            {
                var ctrl = new OsobyController();
                var add = ctrl.Add();

                Assert.IsInstanceOf(typeof(ViewResult), add);
            }
        }

        [Test]
        public void AddPostTest()
        {
            using (var db = new db_context())
            {
                Osoba model = new Osoba();
                model.Imie = "Zbyszek";
                model.Nazwisko = "Erbal";
                model.Wiek = 94;
                var ctrl = new OsobyController();
                var add = ctrl.Add(model);

                bool exist = db.Osoby.Any(x => x.Imie == model.Imie && x.Nazwisko == model.Nazwisko && x.Wiek == model.Wiek);
                Assert.IsTrue(exist);
            }
        }
        [Test]
        public void AddPostTypeTest()
        {
            using (var db = new db_context())
            {
                Osoba model = new Osoba();
                model.Imie = "Zbyszek";
                model.Nazwisko = "Erbal";
                model.Wiek = 94;
                var ctrl = new OsobyController();
                var add = ctrl.Add(model);

                Assert.IsInstanceOf(typeof(ActionResult), add);
            }
        }
        [Test]
        public void RemoveTypeTest()
        {
            using (var db = new db_context())
            {
                var model = db.Osoby.First();
                var ctrl = new OsobyController();
                var remove = ctrl.Remove(model);

                Assert.IsInstanceOf(typeof(ActionResult), remove);
            }
        }

        }
}
