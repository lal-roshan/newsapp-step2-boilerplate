using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using News_WebApp.Controllers;
using News_WebApp.Models;
using News_WebApp.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test.ControllerTests
{
    [TestCaseOrderer("Test.PriorityOrderer", "test")]
    public class NewsControllerTest
    {
        [Fact, TestPriority(1)]
        public async Task GetShouldReturnListOfNews()
        {
            var mockRepo = new Mock<INewsRepository>();
            string userId = "Jack";
            mockRepo.Setup(repo => repo.GetAllNews(userId)).Returns(Task.FromResult(this.newsList));
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData.Add("uId", "Jack");

            var newsController = new NewsController(mockRepo.Object);
            newsController.TempData = tempData;

            var actual =await newsController.Index();

            var actionResult = Assert.IsType<ViewResult>(actual);
            Assert.True(actionResult.ViewData.Values.Count>0 );
            Assert.IsAssignableFrom<List<News>>(actionResult.ViewData["newslist"]);
        }

        [Fact, TestPriority(2)]
        public async Task PostShouldAddNews()
        {
            var mockRepo = new Mock<INewsRepository>();
            var news = new News { Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020.", PublishedAt = DateTime.Now, UrlToImage = null, UserId = "Jack" };
            mockRepo.Setup(repo => repo.AddNews(news)).Returns(Task.FromResult(new News {NewsId=102, Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020.", PublishedAt = DateTime.Now, UrlToImage = null, UserId = "Jack" }));

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData.Add("uId", "Jack");

            var newsController = new NewsController(mockRepo.Object);
            newsController.TempData = tempData;

            var actual = await newsController.Index(news);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Equal("Index",actionResult.ActionName);
        }

        [Fact, TestPriority(3)]
        public async Task DetailsShouldReturnSingleNews()
        {
            int newsId = 102;
            var mockRepo = new Mock<INewsRepository>();
            mockRepo.Setup(repo => repo.GetNewsById(newsId)).Returns(Task.FromResult(new News { NewsId = 102, Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020.", PublishedAt = DateTime.Now, UrlToImage = null, UserId = "Jack" }));

            var newsController = new NewsController(mockRepo.Object);

            var actual = await newsController.Details(newsId);

            var actionResult = Assert.IsType<ViewResult>(actual);
            Assert.NotNull(actionResult.Model);
            Assert.IsAssignableFrom<News>(actionResult.Model);
        }


        [Fact, TestPriority(4)]
        public async Task DeleteShouldReturnToIndex()
        {
            int newsId = 101;
            var mockRepo = new Mock<INewsRepository>();
            mockRepo.Setup(repo => repo.RemoveNews(newsId)).Returns(Task.FromResult(true));
            var newsController = new NewsController(mockRepo.Object);

            var actual =await newsController.Delete(newsId);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Null(actionResult.ControllerName);
            Assert.Equal("Index", actionResult.ActionName);
        }

        private readonly List<News> newsList = new List<News> {
                new News {NewsId=101, Title= "IT industry in 2020", Content= "It is expected to have positive growth in 2020.", PublishedAt= DateTime.Now, UrlToImage=null, UserId="Jack" }
        };
    }
}
