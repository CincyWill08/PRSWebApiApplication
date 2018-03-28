using PRSWebApiApplication.Models;
using PRSWebApiApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRSWebApiApplication.Controllers
{
    public class VendorsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult List()
        {
            return new JsonNetResult { Data = db.Vendors.ToList() };
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }

            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = vendor };
        }
        // /Vendor/Create [POST]
        public ActionResult Create([System.Web.Http.FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            vendor.Active = true;
            vendor.DateCreated = DateTime.Now;
            db.Vendors.Add(vendor);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Vendor was created."));
        }

        // Vendor/Change [POST]
        public ActionResult Change([System.Web.Http.FromBody] Vendor vendor)
        {
            if (vendor.Name == null) return new EmptyResult();
            Vendor vendor2 = db.Vendors.Find(vendor.Id);
            vendor2.Code = vendor.Code;
            vendor2.Name = vendor.Name;
            vendor2.Address = vendor.Address;
            vendor2.City = vendor.City;
            vendor2.State = vendor.State;
            vendor2.Zip = vendor.Zip;
            vendor2.Phone = vendor.Phone;
            vendor2.Email = vendor.Email;
            vendor2.IsPreApproved = vendor.IsPreApproved;
            vendor2.Active = vendor.Active;
            //vendor2.DateCreated = vendor.DateCreated;
            vendor2.DateUpdated = vendor.DateUpdated;
            vendor2.UpdatedByUser = vendor.UpdatedByUser;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Vendor was changed."));
        }

        // User/Remove [POST]
        public ActionResult Remove([System.Web.Http.FromBody] Vendor vendor)
        {
            if (vendor.Name == null) return new EmptyResult();
            Vendor vendor2 = db.Vendors.Find(vendor.Id);
            db.Vendors.Remove(vendor2);
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Vendor was deleted."));
        }

    }
}