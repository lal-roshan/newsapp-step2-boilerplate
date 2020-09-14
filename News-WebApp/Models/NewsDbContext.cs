using Microsoft.EntityFrameworkCore;
namespace News_WebApp.Models
{
    /// <summary>
    /// The class for communicating with database
    /// </summary>
    public class NewsDbContext:DbContext
    {
        /// <summary>
        /// Represents the NewsList table
        /// </summary>
        public DbSet<News> NewsList { get; set; }

        /// <summary>
        /// Represents the Users table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Parametrized constructor to be set from startup
        /// </summary>
        /// <param name="options"></param>
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NewsDbContext() { }

        /// <summary>
        /// Override method for confugration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        /// <summary>
        /// Method to set properties to models when they are being created
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId).ValueGeneratedNever();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "Jack",
                    Password = "password@123"
                },
                new User
                {
                    UserId = "John",
                    Password = "password@123"
                }
            );

            modelBuilder.Entity<News>().HasKey(n => n.NewsId);
            modelBuilder.Entity<News>().Property(n => n.Title).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.Content).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.PublishedAt).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.UserId).IsRequired();
        }

    }
}
