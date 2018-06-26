using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Team1.Controllers;
using SEP_Team1.Models;
using System.Web.Mvc;
using System.Web.Routing;
using SEP_Demo.Tests;

namespace UnitTestSep
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            //var Controller = new HomeController();
            //String ID = "phamminhhuyen";
            //String Pass = "123456";
            //var result = Controller.Login() as ViewResult;
            //var result1 = Controller.Login(ID, Pass) as RedirectToRouteResult ;
            //Assert.AreEqual("Index", result1.RouteValues["action"]);
            //Assert.AreEqual("Home", result1.RouteValues["controller"]);
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            var Username = "phamminhhuyen";
            var password = "croprokiwi";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            //
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("Index", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
        }
        [TestMethod]
        public void TestInvalidAccount()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            var Username = "phamminhhuyenmm";
            var password = "12345678";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var redirectRoute = controller.Login(Username, password) as ViewResult;
            Assert.AreEqual("tai khoang khong ton tai", redirectRoute.ViewBag.mgs);
        }
        [TestMethod]
        public void TestViewCourses()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();

            string id = "SP";
            context.SetupGet(x => x.Session["MaGV"]).Returns("GV123");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult result = controller.Course(id) as ViewResult;
            Assert.AreEqual("", result.ViewName.ToString());
        }
        [TestMethod]
        public void Logout()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            var Username = "phamminhhuyen";
            var password = "croprokiwi";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            var redirectToRouteResult = controller.Logout() as RedirectToRouteResult;
            Assert.AreEqual("Login", redirectToRouteResult.RouteValues["action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);

        }
        [TestMethod]
        public void Index()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();

            context.SetupGet(x => x.Session["MaGV"]).Returns("GV123");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("", result.ViewName.ToString());

        }
        [TestMethod]
        public void ViewCheck()
        {
            //var helper = new MockHelper();
            //var context = helper.MakeFakeContext();
            //var controller = new SEP_Team1.Controllers.HomeController();
            //var Username = "phamminhhuyen";
            //var password = "123456";
            //var buoihoc = "MH1";
            //controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            //var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            //ViewResult checkresult = controller.Check(buoihoc) as ViewResult;
            //Assert.AreEqual("", checkresult.ViewData);

        }
        [TestMethod]
        public void viewStudentProfile()
        {
            //var helper = new MockHelper();
            //var context = helper.MakeFakeContext();
            //var controller = new SEP_Team1.Controllers.HomeController();

            //context.SetupGet(x => x.Session["MaGV"]).Returns("GV123");
            //controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            //ViewResult stuProfile= controller. 
        }
        [TestMethod]
        public void ExportFile()
        {
            //var helper = new MockHelper();
            //var context = helper.MakeFakeContext();
            //var controller = new SEP_Team1.Controllers.HomeController();

            //context.SetupGet(x => x.Session["MaGV"]).Returns("GV123");
            //controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            //ViewResult result = controller.Enrollment() as ViewResult;
            //Assert.AreEqual("", result.ViewName);
        }
    }
}
