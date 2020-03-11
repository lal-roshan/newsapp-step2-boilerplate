using News_WebApp.Models;
using News_WebApp.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.RepositoryTests
{
    [TestCaseOrderer("Test.PriorityOrderer","test")]
    [Collection("NewsDb Fixture")]
    public class NewsRepositoryTest
    {
        private readonly NewsRepository newsRepository;

        public NewsRepositoryTest(DatabaseFixture fixture)
        {
             newsRepository = new NewsRepository(fixture.context);
        }

        [Fact, TestPriority(1)]
        public async Task GetNewsByIdShouldReturnANews()
        {
            int newsId = 101;
            var actual = await newsRepository.GetNewsById(newsId);
            Assert.IsAssignableFrom<News>(actual);
            Assert.Equal("IT industry in 2020",actual.Title);
        }
        [Fact, TestPriority(2)]
        public async Task AddNewsShouldSuccess()
        {
            News news = new News { Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020.", PublishedAt = DateTime.Now, UrlToImage = null, UserId="Jack" };
            var actual=await newsRepository.AddNews(news);
            Assert.Equal(102, actual.NewsId);
            Assert.NotNull(await newsRepository.GetNewsById(102));
        }

        [Fact, TestPriority(3)]
        public async Task GetAllNewsShouldReturnList()
        {
            string userId = "Jack";
            var actual = await newsRepository.GetAllNews(userId);
            Assert.Equal(2, actual.Count);
        }

        [Fact, TestPriority(4)]
        public async Task DeleteNewsShouldSuccess()
        {
            int newsId = 102;
            var actual=await newsRepository.RemoveNews(newsId);
            
            Assert.True(actual);
            Assert.Null(await newsRepository.GetNewsById(102));
        }
    }
}
