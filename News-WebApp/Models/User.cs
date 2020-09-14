using System.ComponentModel.DataAnnotations;

namespace News_WebApp.Models
{
    /// <summary>
    /// User model class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
