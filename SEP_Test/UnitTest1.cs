﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Team1.Controllers;
using SEP_Team1.Models;
using System.Web.Mvc;
using System.Web.Routing;
using SEP_Demo.Tests;
using Moq;
using System.Web;


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
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("Index", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);



        }
        [TestMethod]
        public void TestInvalidAccount_WithWrongPassWord()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            //context.SetupGet(x => x.Session["hoten"]).Returns("username");
            //context.SetupGet(x => x.Session["MaGV"]).Returns("item.data.id");
            var Username = "phamminhhuyen";
            var password = "123";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult view = controller.Login(Username, password) as ViewResult;
            Assert.AreEqual("", view.ViewName);
            Assert.AreEqual("Incorrect username or password", controller.ViewBag.mgs);

        }
        [TestMethod]
        public void TestInvalidAccount_WithWrongUsername()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            //context.SetupGet(x => x.Session["hoten"]).Returns("username");
            //context.SetupGet(x => x.Session["MaGV"]).Returns("item.data.id");
            var Username = "phamminhhuyennn";
            var password = "croprokiwi";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult view = controller.Login(Username, password) as ViewResult;
            Assert.AreEqual("", view.ViewName);
            Assert.AreEqual("Incorrect username or password", controller.ViewBag.mgs);

            //Assert.AreEqual("tai khoang khong ton tai", redirectRoute.ViewBag.mgs);
        }
        [TestMethod]
        public void TestInvalidAccount_WithWrongAccount()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            //context.SetupGet(x => x.Session["hoten"]).Returns("username");
            //context.SetupGet(x => x.Session["MaGV"]).Returns("item.data.id");
            var Username = "phamminhhuyenn";
            var password = "234567";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult view = controller.Login(Username, password) as ViewResult;
            Assert.AreEqual("",view.ViewName);
            Assert.AreEqual("Incorrect username or password", controller.ViewBag.mgs);

            //Assert.AreEqual("tai khoang khong ton tai", redirectRoute.ViewBag.mgs);
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
           

        }
        [TestMethod]
        public void Index()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();

            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("", result.ViewName);

        }
        [TestMethod]
        public void TestViewdiemDanh()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");


            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            ViewResult result = controller.diemDanh("2") as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void TestCreateAttendance()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH3");

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult result = controller.CreateAttendance("MH1") as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void TestCreateAttendanceWithNullID()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH3");

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
           RedirectToRouteResult result = controller.CreateAttendance("") as RedirectToRouteResult;

            Assert.AreEqual("", result.RouteName);


        }
        [TestMethod]
        public void TestCreateAttendanceWithWrongID()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            //context.SetupGet(x => x.Session["MaKH"]).Returns("MH3");

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            RedirectToRouteResult result = controller.CreateAttendance("ddas") as RedirectToRouteResult;

            Assert.AreEqual("", result.RouteName);


        }


        [TestMethod]
        public void TestViewAttendanceWithValidInput()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");
            context.SetupGet(x => x.Session["SessionIDs"]).Returns("1");

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            ViewResult result = controller.ViewAttendance("1") as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void TestViewAttendanceWithInvalidInput()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");
            context.SetupGet(x => x.Session["SessionIDs"]).Returns("1");

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            ViewResult result = controller.ViewAttendance("") as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void TestCheck()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            PartialViewResult result = controller.Check("BHMH1003") as PartialViewResult;
            //Assert.AreEqual("", result.ViewBag.Diemdanh);
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void TestChange()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");
            context.SetupGet(x => x.Session["SessionID"]).Returns("2");


            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var redirectToRouteResult = controller.Change("T154888") as RedirectToRouteResult;
            Assert.AreEqual("", redirectToRouteResult.RouteName);
        }
        [TestMethod]
        public void TestChangeToFalse()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");
            context.SetupGet(x => x.Session["SessionID"]).Returns("1");


            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var redirectToRouteResult = controller.Change("T154888") as RedirectToRouteResult;
            Assert.AreEqual("", redirectToRouteResult.RouteName);
        }
        [TestMethod]
        public void TestEd()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");

            //context.SetupGet(x => x.Session["SessionID"]).Returns("3");



            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var redirectToRouteResult = controller.Ed("BHMH2001") as RedirectToRouteResult;
            
            Assert.AreEqual("", redirectToRouteResult.RouteName);
        }
        [TestMethod]
        public void TestEdit()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");

            //context.SetupGet(x => x.Session["SessionID"]).Returns("3");



            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var redirectToRouteResult = controller.Ed("BHMH2001") as RedirectToRouteResult;

            Assert.AreEqual("", redirectToRouteResult.RouteName);
        }
        [TestMethod]
        public void TestViewListStudent()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");
            context.SetupGet(x => x.Session["MaKH"]).Returns("MH2");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult result = controller.ViewListStudent("MH2") as ViewResult;
            Assert.AreEqual("", result.ViewName.ToString());
        }
        [TestMethod]
        public void TestListStudent()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult result = controller.ListStudent("MH1") as ViewResult;
            Assert.AreEqual("", result.ViewName.ToString());
        }
        [TestMethod]
        public void TestLoginView()
        {
            //var helper = new MockHelper();
            //var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();

            //controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            ViewResult result = controller.Login() as ViewResult;
            Assert.AreEqual("", result.ViewName.ToString());
        }
        [TestMethod]
        public void TestStudentProfile()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new SEP_Team1.Controllers.HomeController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["MaGV"]).Returns("MH");



            ViewResult result = controller.StudentProfile("T15337") as ViewResult;
            Assert.AreEqual("", result.ViewName);

        }
        //[TestMethod]
        //public void TestExport()
        //{
        //    var helper = new MockHelper();
        //    var context = helper.MakeFakeContext();
        //    var controller = new SEP_Team1.Controllers.HomeController();
        //    controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        //    context.SetupGet(x => x.Session["SessionExcel"]).Returns("1");



        //    var a =controller.ExportToExcel("MH2") as HttpFileCollection;
        //    Assert.AreEqual("", a.);

        //}

    }
}



