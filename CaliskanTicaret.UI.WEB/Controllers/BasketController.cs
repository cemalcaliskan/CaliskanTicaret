using CaliskanTicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaliskanTicaret.UI.WEB.Controllers
{
    public class BasketController : CaliskanControllerBase
    {
        CaliskanDB db = new CaliskanDB();

        // GET: Basket
        [HttpPost]
        public JsonResult AddProduct(int productID, int quantity)
        {
            db.Baskets.Add(new Core.Model.Entity.Basket
            {
                CreateDate = DateTime.Now,
                CreateUserID = LoginUserID,
                ProductID = productID,
                Quantity = quantity,
                UserID = LoginUserID
            });
            var basket = db.SaveChanges();
            return Json(basket, JsonRequestBehavior.AllowGet);
        }

        [Route("sepetim")]
        public ActionResult Index()
        {
            var sepet = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            return View(sepet);
        }

        public ActionResult Delete(int id)
        {
            var deleteItem = db.Baskets.Where(x => x.ID == id).FirstOrDefault();
            db.Baskets.Remove(deleteItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}