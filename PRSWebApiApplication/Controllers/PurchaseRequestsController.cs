using PRSWebApiApplication.Models;
using PRSWebApiApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRSWebApiApplication.Controllers
{

    public class PurchaseRequestsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult List()
        {
            return new JsonNetResult { Data = db.PurchaseRequests.ToList() };
        }

        public ActionResult ReviewList()
        {
            return new JsonNetResult { Data = db.PurchaseRequests.Where(PurchaseRequest => PurchaseRequest.Status=="REVIEW").ToList() };
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }

            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
            if (purchaseRequest == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = purchaseRequest };
        }
        // /PurchaseRequest/Create [POST]
        public ActionResult Create([System.Web.Http.FromBody] PurchaseRequest purchaseRequest)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
         
            purchaseRequest.Active = true;
            purchaseRequest.Status = "NEW";
            purchaseRequest.DateCreated = DateTime.Now;

            // ?? Calculate total by summing all purchase request line items?

            db.PurchaseRequests.Add(purchaseRequest);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "PurchaseRequest was created."));
        }

        // PurchaseRequest/Change [POST]
        public ActionResult Change([System.Web.Http.FromBody] PurchaseRequest purchaseRequest)
        {
            if (purchaseRequest.Description == null) return new EmptyResult();
            PurchaseRequest purchaseRequest2 = db.PurchaseRequests.Find(purchaseRequest.Id);
            purchaseRequest2.UserId = purchaseRequest.UserId;
            purchaseRequest2.Description = purchaseRequest.Description;
            purchaseRequest2.Justification = purchaseRequest.Justification;
            purchaseRequest2.DeliveryMode = purchaseRequest.DeliveryMode;
            purchaseRequest2.Status = purchaseRequest.Status; 
            purchaseRequest2.Total = purchaseRequest.Total;
            purchaseRequest2.Active = purchaseRequest.Active;
            purchaseRequest2.ReasonForRejection = purchaseRequest.ReasonForRejection;
            //purchaseRequest2.DateCreated = purchaseRequest.DateCreated;
            purchaseRequest2.DateUpdated = purchaseRequest.DateUpdated;
            purchaseRequest2.UpdatedByUser = purchaseRequest.UpdatedByUser;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "PurchaseRequest was changed."));
        }

        // PurchaseRequest/Remove [POST]
        public ActionResult Remove([System.Web.Http.FromBody] PurchaseRequest purchaseRequest)
        {
            if (purchaseRequest.Description == null) return new EmptyResult();
            PurchaseRequest purchaseRequest2 = db.PurchaseRequests.Find(purchaseRequest.Id);
            db.PurchaseRequests.Remove(purchaseRequest2);
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "PurchaseRequest was deleted."));
        }

    }
    
}