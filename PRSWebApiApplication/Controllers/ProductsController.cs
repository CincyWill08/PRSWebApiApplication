using PRSWebApiApplication.Models;
using PRSWebApiApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRSWebApiApplication.Controllers
{
    
    public class ProductsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        

    public ActionResult List()
        {
            return new JsonNetResult { Data = db.Products.ToList()};
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = product };
        }
        // /Product/Create [POST]
        public ActionResult Create([System.Web.Http.FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            product.Active = true;
            product.DateCreated = DateTime.Now;
            db.Products.Add(product);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Product was created."));
        }

        // Product/Change [POST]
        public ActionResult Change([System.Web.Http.FromBody] Product product)
        {
            Product product2 = db.Products.Find(product.Id);
            product2.VendorId = product.VendorId;
            product2.PartNumber = product.PartNumber;
            product2.Name = product.Name;
            product2.Price = product.Price;
            product2.Unit = product.Unit;
            product2.PhotoPath = product.PhotoPath;
            product2.Active = product.Active;
            //product2.DateCreated = product.DateCreated;
            product2.DateUpdated = product.DateUpdated;
            product2.UpdatedByUser = product.UpdatedByUser;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Product was changed."));
        }

        // User/Remove [POST]
        public ActionResult Remove([System.Web.Http.FromBody] Product product)
        {
            Product product2 = db.Products.Find(product.Id);
            db.Products.Remove(product2);
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Product was deleted."));
        }
    }
}