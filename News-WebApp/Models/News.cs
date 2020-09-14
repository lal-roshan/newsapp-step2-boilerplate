using System;

namespace News_WebApp.Models
{
    /// <summary>
    /// Model class for news table
    /// </summary>
    public class News
    {
        /// <summary>
        /// Id of the news
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// Title of the news
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Content of the news
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Date and time of publish of the news
        /// </summary>
        public DateTime PublishedAt { get; set; }

        /// <summary>
        /// The url to the image of the news
        /// </summary>
        public string UrlToImage { get; set; }

        /// <summary>
        /// The id of the user who published the news
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User model property added for creating foreign key constraint
        /// </summary>
        public User User { get; set; }
    }
}
