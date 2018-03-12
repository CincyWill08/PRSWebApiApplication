﻿using PRSWebApiApplication.Models;
using PRSWebApiApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRSWebApiApplication.Controllers
{
    public class PurchaseRequestLineItemsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult List()
        {
            return new JsonNetResult { Data = db.PurchaseRequestLineItems.ToList() };
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }

            PurchaseRequestLineItem purchaseRequestLineItem = db.PurchaseRequestLineItems.Find(id);
            if (purchaseRequestLineItem == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = purchaseRequestLineItem };
        }
        // /PurchaseRequestLineItem/Create [POST]
        public ActionResult Create([System.Web.Http.FromBody] PurchaseRequestLineItem purchaseRequestLineItem)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            purchaseRequestLineItem.Active = true;
            purchaseRequestLineItem.DateCreated = DateTime.Now;           
            db.PurchaseRequestLineItems.Add(purchaseRequestLineItem);         
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            CalculatePurchaseRequestTotal(purchaseRequestLineItem.PurchaseRequestId);
            return Json(new JsonMessage("Success", "PurchaseRequestLineItem was created."));           
        }

        // PurchaseRequestLineItem/Change [POST]
        public ActionResult Change([System.Web.Http.FromBody] PurchaseRequestLineItem purchaseRequestLineItem)
        {
            PurchaseRequestLineItem purchaseRequestLineItem2 = db.PurchaseRequestLineItems.Find(purchaseRequestLineItem.Id);
            purchaseRequestLineItem2.PurchaseRequestId = purchaseRequestLineItem.PurchaseRequestId;
            purchaseRequestLineItem2.ProductId = purchaseRequestLineItem.ProductId;
            purchaseRequestLineItem2.Quantity = purchaseRequestLineItem.Quantity;
            purchaseRequestLineItem2.Active = purchaseRequestLineItem.Active;
            //purchaseRequestLineItem2.DateCreated = purchaseRequestLineItem.DateCreated;
            purchaseRequestLineItem2.DateUpdated = purchaseRequestLineItem.DateUpdated;
            purchaseRequestLineItem2.UpdatedByUser = purchaseRequestLineItem.UpdatedByUser;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "PurchaseRequestLineItem was changed."));
        }

        // PurchaseRequestLineItem/Remove [POST]
        public ActionResult Remove([System.Web.Http.FromBody] PurchaseRequestLineItem purchaseRequest)
        {
            PurchaseRequestLineItem purchaseRequestLineItem2 = db.PurchaseRequestLineItems.Find(purchaseRequest.Id);
            db.PurchaseRequestLineItems.Remove(purchaseRequestLineItem2);
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "PurchaseRequestLineItem was deleted."));
        }

        public ActionResult CalculatePurchaseRequestTotal(int purchaseRequestId)
        {
            decimal total = 0;

            var purchaseRequestLineItems = db.PurchaseRequestLineItems.Where(p => p.PurchaseRequestId == purchaseRequestId);

            foreach (var purchaseRequestLineItem in purchaseRequestLineItems)
            {
                total += purchaseRequestLineItem.Quantity * purchaseRequestLineItem.Product.Price;
            }

            var purchaseRequest = db.PurchaseRequests.Find(purchaseRequestId);
            purchaseRequest.Total = total;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure in CalculatePurchaseRequestTotal", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success in CalculatePurchaseRequestTotal", "PurchaseRequest.Total was updated."));
        }
    }
}