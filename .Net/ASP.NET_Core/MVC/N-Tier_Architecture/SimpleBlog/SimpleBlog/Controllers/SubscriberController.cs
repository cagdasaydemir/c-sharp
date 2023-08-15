using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.DataAccess.Repository;
using SimpleBlog.DataAccess.Repository.IRepository;
using SimpleBlog.Models.Models;
using SimpleBlog.Models.ViewModels;

namespace SimpleBlog.Web.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public SubscriberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [Authorize]
        public IActionResult Index()
        {
            var subscribers = _unitOfWork.Subscriber.GetAll();
            return View(subscribers);
        }
        public IActionResult Subscribe()
        {
            Subscriber newSubscriber = new();

            return View(newSubscriber);
        }
        [HttpPost]
        public IActionResult Subscribe(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                var isSubscriberExist = _unitOfWork.Subscriber.Get(x => x.Email == subscriber.Email);
                if (isSubscriberExist == null)
                {
                    subscriber.DateSubscribed = DateTime.Now;
                    subscriber.IsSubscribed = true;

                    _unitOfWork.Subscriber.Add(subscriber);
                    TempData["Success"] = "You have subscribed successfully.";

                    _unitOfWork.Save();
                    return RedirectToAction("Subscribed");
                }
                else
                {
                    ViewData["ErrorMessage"] = "The email address is already subscribed.";
                    return View(subscriber);
                }
            }

            return View(subscriber);
        }
        public IActionResult Subscribed()
        {
            return View();
        }

        public IActionResult Unsubscribe()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Unsubscribe(UnsubscribeVM model)
        {
            if (ModelState.IsValid)
            {
                var subscriber = _unitOfWork.Subscriber.Get(x => x.Email == model.Email);
                if (subscriber != null)
                {
                    subscriber.DateUnSubscribed = DateTime.Now;
                    subscriber.IsSubscribed = false;

                    _unitOfWork.Subscriber.Update(subscriber);
                    _unitOfWork.Save();
                    TempData["Success"] = "You have Unsubscribed successfully.";
                }
                else
                {
                    ViewData["ErrorMessage"] = "The email address is not found. Please <a href='" + Url.Action("Subscribe", "Subscriber") + "'>click here</a> to subscribe.";


                    return View(model);
                }

                return RedirectToAction("Unsubscribed");
            }

            return View(model);
        }
        public IActionResult Unsubscribed()
        {
            return View();
        }


        #region API


        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Subscriber> objSubscriberList = _unitOfWork.Subscriber.GetAll().ToList();
            return Json(new { data = objSubscriberList });
        }


        [Authorize]
        public IActionResult Delete(int? id)
        {
            var subscriberToBeDeleted = _unitOfWork.Subscriber.Get(u => u.Id == id);
            if (subscriberToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Subscriber.Remove(subscriberToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Subscriber deleted successfully." });
        }
        [Authorize]
        public IActionResult ToggleSubscription(int? id)
        {
            var subscriberToBeUpdated = _unitOfWork.Subscriber.Get(u => u.Id == id);
            if (subscriberToBeUpdated == null)
            {
                return Json(new { success = false, message = "Subscriber not found." });
            }

            subscriberToBeUpdated.IsSubscribed = !subscriberToBeUpdated.IsSubscribed;
            if (!subscriberToBeUpdated.IsSubscribed)
            {
                subscriberToBeUpdated.DateUnSubscribed = DateTime.Now;
            }

            _unitOfWork.Subscriber.Update(subscriberToBeUpdated);
            _unitOfWork.Save();

            if (subscriberToBeUpdated.IsSubscribed)
                return Json(new { success = true, message = "Activated successfully." });
            else return Json(new { success = true, message = "Deactivated successfully." });
        }


        #endregion
    }

}
