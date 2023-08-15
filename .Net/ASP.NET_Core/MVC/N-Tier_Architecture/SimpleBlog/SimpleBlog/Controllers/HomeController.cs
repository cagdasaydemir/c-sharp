using Microsoft.AspNetCore.Mvc;
using SimpleBlog.DataAccess.Repository;
using SimpleBlog.DataAccess.Repository.IRepository;
using SimpleBlog.Models;
using SimpleBlog.Models.Models;
using System.Diagnostics;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogPost> blogPostList = _unitOfWork.BlogPost.GetAll(includeProperties: "Author,Comments");
            return View(blogPostList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}