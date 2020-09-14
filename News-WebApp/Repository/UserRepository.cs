using News_WebApp.Models;
using System;
using System.Linq;
namespace News_WebApp.Repository
{
    /// <summary>
    /// Class for implementing CRUD operations on User table
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// an object of dbcontext
        /// </summary>
        private NewsDbContext context;

        /// <summary>
        /// Constructor for injecting dbcontext property
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(NewsDbContext context)
        {
            this.context = context;
        }
        
        /// <summary>
        /// Method to check whether a user exists with the given set of user id and password
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="password">User password</param>
        /// <returns>True if a user exists with the given combination of userId and password else false</returns>
        public bool IsAuthenticated(string userId, string password)
        {
            try
            {
                User user = context.Users
                    .Where(u => string.Equals(userId, u.UserId) && string.Equals(password, u.Password))
                    .FirstOrDefault();
                return user != null? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
