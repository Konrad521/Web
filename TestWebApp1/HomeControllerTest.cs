using System;
using WebApplication1.Controllers;
using System.Web.Mvc;
using NUnit.Framework;

namespace TestWebApp1
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void IndexTest()
        {
            var ctrl = new NieruchomoscController();
            var index = ctrl.Map();

            Assert.IsInstanceOf(typeof(ViewResult),index);

        }
        [Test]
        public void AboutTest()
        {
            var ctrl = new NieruchomoscController();
            var about = ctrl.About();

            Assert.IsInstanceOf(typeof(ViewResult), about);

        }
        [Test]
        public void ContactTest()
        {
            var ctrl = new NieruchomoscController();
            var contact = ctrl.Contact();

            Assert.IsInstanceOf(typeof(ViewResult), contact);

        }
    }
}
