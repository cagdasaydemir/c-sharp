using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBlog.DataAccess.Repository.IRepository;
using SimpleBlog.Models.Models;
using SimpleBlog.Models.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleBlog.Web.Controllers
{
    
    public class BlogPostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;
        public BlogPostController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            List<BlogPost> blogPostList = _unitOfWork.BlogPost.GetAll(includeProperties: "Author,Comments").ToList();

            return View(blogPostList);
        }
    
        public IActionResult Upsert(int? id)
        {
           

            if (id == null || id == 0)
            {
                BlogPost newPost = new();
                // create
                return View(newPost);
            }
            else
            {
                // update
                BlogPost post = _unitOfWork.BlogPost.Get(u => u.Id == id, includeProperties: "Author");
                return View(post);
            }

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upsert(BlogPost obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    obj.DateCreated = DateTime.Now;
                    var currentUser = await _userManager.GetUserAsync(User);
                    obj.Author = currentUser;
                    _unitOfWork.BlogPost.Add(obj);
                    TempData["success"] = "Blog Post created successfully.";
                }
                else
                {
                    _unitOfWork.BlogPost.Update(obj);
                    TempData["success"] = "Blog Post updated successfully.";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Detail(int? id)
        {

            BlogPost post = _unitOfWork.BlogPost.Get(u => u.Id == id, includeProperties:"Author,Comments");
            DetailVM detailVM = new()
            {
                BlogPost = post,
                NewComment = new Comment() 
            };
            return View(detailVM);
        }

        public IActionResult UserPosts(string id)
        {
            List<BlogPost> blogPostList = _unitOfWork.BlogPost.GetAll(x => x.AuthorId == id, includeProperties: "Author,Comments").ToList();
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            UserBlogPostsViewModel viewModel;

            if (blogPostList.Count == 0)
            {
                viewModel = new UserBlogPostsViewModel
                {
                    BlogPosts = new List<BlogPost>(),
                    User = user
                };
            }
            else
            {
                viewModel = new UserBlogPostsViewModel
                {
                    BlogPosts = blogPostList,
                    User = user
                };
            }

            return View(viewModel);
        }


        #region API

        [Authorize]
        public IActionResult Delete(int? id)
        {
            var postToBeDeleted = _unitOfWork.BlogPost.Get(u => u.Id == id);
            if (postToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.BlogPost.Remove(postToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Blog post deleted successfully." });
        }

        #endregion
    }
}
