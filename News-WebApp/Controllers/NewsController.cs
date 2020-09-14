using Microsoft.AspNetCore.Mvc;
using News_WebApp.Models;
using News_WebApp.Repository;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace News_WebApp.Controllers
{
    /// <summary>
    /// Controller for handling news related operations
    /// </summary>
    public class NewsController : Controller
    {
        /// <summary>
        /// news repository property for accessing operations on news model
        /// </summary>
        readonly INewsRepository newsRepository;
        
        /// <summary>
        /// Paramterised constructor for injecting news repository property
        /// </summary>
        /// <param name="newsRepository">The news repository object to be injected</param>
        public NewsController(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        /// <summary>
        /// Default action for news controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["newsList"] = await newsRepository.GetAllNews(TempData["uId"]?.ToString());
            return View();
        }

        /// <summary>
        /// Action for adding news
        /// </summary>
        /// <param name="news">The object containing the new news details</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(News news)
        {
            string prevUserId = news.UserId;
            string filepath = string.Empty;
            if (ModelState.IsValid)
            {
                if (Request?.Form?.Files?.Any() ?? false)
                {
                    filepath = Path.GetFullPath("wwwroot/images/");
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    filepath = Path.Combine(filepath, Request.Form.Files.First().FileName);
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                    using (var stream = System.IO.File.Create(filepath))
                    {
                        await Request.Form.Files.First().CopyToAsync(stream);
                    }
                    news.UrlToImage = Request.Form.Files.First().FileName;
                }
                news = await newsRepository.AddNews(news);
            }
            if (news == null)
            {
                if (!string.IsNullOrEmpty(filepath))
                {
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
                ModelState.AddModelError("OperationFailed", "Something went wrong while adding the news!!!");
            }
            TempData["uId"] = prevUserId;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action method which accepts a newsId and shows the details of the specified news
        /// </summary>
        /// <param name="newsId">News id of the news details to be shown</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int newsId)
        {
            News news = await newsRepository.GetNewsById(newsId);
            return View(news);
        }

        /// <summary>
        /// Action method for deleting a news
        /// </summary>
        /// <param name="newsId">Id of the news to be deleted</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int newsId)
        {
            if (ModelState.IsValid)
            {
                await newsRepository.RemoveNews(newsId);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action method for going back to the main page from details page
        /// </summary>
        /// <returns></returns>
        public IActionResult GoBack()
        {
            return RedirectToAction("Index");
        }
    }
}
