using PRSWebApiApplication.Models;
using PRSWebApiApplication.Utility;
using Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRSWebApiApplication.Controllers
{
    public class UsersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                return Json(new JsonMessage("Failure", "1.Username or password is incorrect.  Try again."), JsonRequestBehavior.AllowGet);
            }
            var loginVerification = db.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            if (loginVerification == null) {
                return Json(new JsonMessage("Failure", "2. Username or password is incorrect.  Try again."), JsonRequestBehavior.AllowGet);
            }
            //return Json(new JsonMessage("Success", "Login is successful"), Data = loginVerification);
            //return new JsonNetResult { Data = new JsonMessage ("Success", "Login is successful"), Data = loginVerification };
            return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Login Successful", Data = loginVerification } };
        }

        public ActionResult List()
        {
            return new JsonNetResult { Data = db.Users.ToList() };
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            //return Json(user, JsonRequestBehavior.AllowGet);
            return new JsonNetResult { Data = user };
        }
        // /User/Create [POST]
        public ActionResult Create([System.Web.Http.FromBody] User user)
        {
            if (user.UserName == null) return new EmptyResult();
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            user.Active = true;
            user.DateCreated = DateTime.Now;
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "User was created."));
        }

        // User/Change [POST]
        public ActionResult Change([System.Web.Http.FromBody] User user)
        {
            if (user.UserName == null) return new EmptyResult();
            User user2 = db.Users.Find(user.Id);
            user2.UserName = user.UserName;
            user2.Password = user.Password;
            user2.FirstName = user.FirstName;
            user2.LastName = user.LastName;
            user2.Phone = user.Phone;
            user2.Email = user.Email;
            user2.IsReviewer = user.IsReviewer;
            user2.IsAdmin = user.IsAdmin;
            user2.Active = user.Active;
            //user2.DateCreated = user.DateCreated;
            user2.DateUpdated = user.DateUpdated;
            user2.UpdatedByUser = user.UpdatedByUser;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "User was changed."));
        }

        // User/Remove [POST]
        public ActionResult Remove([System.Web.Http.FromBody] User user)
        {
            if (user.UserName == null) return new EmptyResult();
            User user2 = db.Users.Find(user.Id);
            db.Users.Remove(user2);
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "User was deleted."));
        }
    }
}