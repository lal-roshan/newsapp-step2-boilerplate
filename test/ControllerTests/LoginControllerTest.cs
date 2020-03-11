using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using News_WebApp.Controllers;
using News_WebApp.Models;
using News_WebApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.ControllerTests
{
    public class LoginControllerTest
    {
        [Fact]
        public void ShouldReturnLoginView()
        {
            var mockRepo = new Mock<IUserRepository>();
            var loginController = new LoginController(mockRepo.Object);

            var actual = loginController.Index();
            Assert.IsType<ViewResult>(actual);
        }

        [Fact]
        public void LoginShouldReturnToIndexAction()
        {
            string userId = "Jack";
            string password = "password@123";
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.IsAuthenticated(userId, password)).Returns(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var loginController = new LoginController(mockRepo.Object);
            loginController.TempData = tempData;

            var actual = loginController.Index(new User { UserId=userId, Password=password });

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("News", actionResult.ControllerName);
        }
    }
}
