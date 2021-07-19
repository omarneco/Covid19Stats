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
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19() ;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19Province(String Region)
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19Province(Region);

            // Assert
            Assert.AreEqual("Covid19 Statistic by Province", result.Result.ToString());
        }

        [TestMethod]
        public void Covid19StatToXml()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19StatToXml();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19StatToJson()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19StatToJson();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19StatCvs()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19StatCvs();

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Covid19ProvinceStatToXml(String Region)
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19ProvinceStatToXml(Region);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19ProvinceStatToJson(String Region)
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19ProvinceStatToJson(Region);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Covid19ProvinceStatCvs(String Region)
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Covid19ProvinceStatToJson(Region);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
