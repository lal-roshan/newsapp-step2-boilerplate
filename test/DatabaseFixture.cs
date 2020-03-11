using System;
using Microsoft.EntityFrameworkCore;
using News_WebApp.Models;
using Xunit;

namespace Test
{
    public class DatabaseFixture : IDisposable
    {
        public NewsDbContext context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<NewsDbContext>()
                .UseInMemoryDatabase(databaseName: "NewsDB")
                .Options;

            //Initializing DbContext with InMemory
            context = new NewsDbContext(options);

            // Insert seed data into the database using one instance of the context

            context.NewsList.Add(new News {NewsId=101, Title = "IT industry in 2020", Content = "It is expected to have positive growth in 2020.", PublishedAt = DateTime.Now, UrlToImage = null, UserId="Jack" });
            context.SaveChanges();
            //context.NewsList.Add(new News { Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020.", PublishedAt = DateTime.Now, UrlToImage = null });
            //context.SaveChanges();
        }
        public void Dispose()
        {
            context = null;
        }
    }

    [CollectionDefinition("NewsDb Fixture")]
    public class DbCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
