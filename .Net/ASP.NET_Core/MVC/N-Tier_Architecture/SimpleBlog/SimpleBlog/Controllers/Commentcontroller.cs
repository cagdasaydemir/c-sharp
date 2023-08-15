using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.DataAccess.Repository.IRepository;
using SimpleBlog.Models.Models;
using SimpleBlog.Models.ViewModels;

namespace SimpleBlog.Web.Controllers
{
    public class Commentcontroller : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public Commentcontroller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DetailVM viewModel)
        {
            var newCommentObj = viewModel.NewComment;

      
                newCommentObj.DatePosted = DateTime.Now;
                _unitOfWork.Comment.Add(newCommentObj);
                _unitOfWork.Save();

                TempData["success"] = "Comment added successfully.";

                
                return RedirectToAction("Detail", "BlogPost", new { id = newCommentObj.BlogPostId });
            
        }

        #region API
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var commentToBeDeleted = _unitOfWork.Comment.Get(u => u.Id == id);
            if (commentToBeDeleted == null)
            {
                return Json(new { success = false, message = "Comment not found." });
            }

            _unitOfWork.Comment.Remove(commentToBeDeleted);
            _unitOfWork.Save();

            TempData["success"] = "Comment deleted successfully.";
            return Json(new { success = true });
        }
        #endregion
    }
}
