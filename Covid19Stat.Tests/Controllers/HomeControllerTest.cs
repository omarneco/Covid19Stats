using Covid19Stat;
using Covid19Stat.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Covid19Stat.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Covid19()
        {
            
            HomeController controller = new HomeController();
            ViewResult result = controller.Covid19().Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
            Assert.AreEqual("Covid19", result.ViewName);
        }

        [TestMethod]
        public void Covid19Province(String Region)
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19Province(Region).Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
            Assert.AreEqual("Covid19Province", result.ViewName);
        }

        [TestMethod]
        public void Covid19StatToXml()
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19StatToXml().Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19StatToJson()
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19StatToJson().Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19StatCvs()
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19StatCvs().Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Covid19ProvinceStatToXml(String Region)
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19ProvinceStatToXml(Region).Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19ProvinceStatToJson(String Region)
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19ProvinceStatToJson(Region).Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19ProvinceStatCvs(String Region)
        {
            HomeController controller = new HomeController();
            var result = controller.Covid19ProvinceStatToJson(Region).Result as ViewResult;

            Assert.Fail();
            Assert.IsNotNull(result);
        }
    }
}
