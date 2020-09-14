using Microsoft.AspNetCore.Mvc;
using News_WebApp.Models;
using News_WebApp.Repository;

namespace News_WebApp.Controllers
{
    /// <summary>
    /// Controller class for Login purpose
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// User repository property for accessing operations on user model
        /// </summary>
        readonly IUserRepository userRepository;

        /// <summary>
        /// Parametrized constructor for injecting the user repository property
        /// </summary>
        /// <param name="userRepository">The userRepository object to be injected</param>
        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Default action for showing default view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
       
        /// <summary>
        /// The action for performing login operation
        /// </summary>
        /// <param name="user">The user properties who is trying to login</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                if (userRepository.IsAuthenticated(user.UserId, user.Password))
                {
                    TempData["uId"] = user.UserId;
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("IncorrectData", "Invalid User Id or Password");
                }
            }
            return View(user);
        }
    }
}