using Microsoft.EntityFrameworkCore;
using News_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace News_WebApp.Repository
{
    /// <summary>
    /// Class containing implementations of CRUD operation on news table
    /// </summary>
    public class NewsRepository: INewsRepository
    {
        /// <summary>
        /// an object of dbcontext
        /// </summary>
        private NewsDbContext context;

        /// <summary>
        /// Constructor for injecting the dbcontext property
        /// </summary>
        /// <param name="context"></param>
        public NewsRepository(NewsDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Method for adding news
        /// </summary>
        /// <param name="news">The news object that is to be added</param>
        /// <returns>The added news if success else null</returns>
        public async Task<News> AddNews(News news)
        {
            try
            {
                await context.NewsList.AddAsync(news);
                if(await context.SaveChangesAsync() > 0)
                {
                    return news;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all news created by a user
        /// </summary>
        /// <param name="userId">the id of user whose news are to be fetched</param>
        /// <returns>List of all news created by the user</returns>
        public async Task<List<News>> GetAllNews(string userId)
        {
            try
            {
                return await context.NewsList.Where(n => string.Equals(n.UserId, userId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the details of a specific news
        /// </summary>
        /// <param name="newsId">Id of the news whose details is to be fetched</param>
        /// <returns>News object corresponding to the id provided</returns>
        public async Task<News> GetNewsById(int newsId)
        {
            try
            {
                return await context.NewsList.Where(n => n.NewsId == newsId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to delete a news
        /// </summary>
        /// <param name="newsId">id of the news to be deleted</param>
        /// <returns>true if deleted successfuly else false</returns>
        public async Task<bool> RemoveNews(int newsId)
        {
            try
            {
                News news = await context.NewsList.Where(n => n.NewsId == newsId).FirstOrDefaultAsync();
                if(news != null)
                {
                    context.NewsList.Remove(news);
                    return await context.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
