using Microsoft.EntityFrameworkCore;
namespace News_WebApp.Models
{
    //Inherit DbContext class and use Entity Framework Code First Approach
    public class NewsDbContext:DbContext
    {
        /*
        This class should be used as DbContext to speak to database and should make the use of 
        Code First Approach. It should autogenerate the database based upon the model class in 
        your application
        */

        //Create a Dbset for News class and User class

        /*Override OnModelCreating function to configure relationship between entities and initialize 
         * with seed data 
         *  UserId:Jack Password:password@123
         *  UserId:John Password:password@123
         * So that user can login by using the above credentials
        */

    }
}
