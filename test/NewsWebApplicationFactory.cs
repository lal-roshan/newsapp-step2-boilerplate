using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using News_WebApp;
using News_WebApp.Models;
using System;
using System.Linq;
using Xunit;

namespace Test
{
    public class NewsWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<NewsDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a database context (KeepNoteContext) using an in-memory database for testing.
                services.AddDbContext<NewsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("NewsDB_Test");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var newsDb = scopedServices.GetRequiredService<NewsDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<NewsWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    newsDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        newsDb.NewsList.Add(new News { NewsId = 101, Title = "IT industry in 2020", Content = "It is expected to have positive growth in 2020.", PublishedAt = DateTime.Now, UrlToImage = null, UserId = "Jack" });
                        newsDb.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    [CollectionDefinition("News Fixture")]
    public class DBCollection : ICollectionFixture<NewsWebApplicationFactory<Startup>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
