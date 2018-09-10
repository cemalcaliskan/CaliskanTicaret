using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaliskanTicaret.Core.Model;
using CaliskanTicaret.Core.Model.Entity;

namespace CaliskanTicaret.UI.WEB.Areas.Admin.Controllers
{
    public class AdminOrdersController : Controller
    {
        private CaliskanDB db = new CaliskanDB();

        // GET: Admin/AdminOrders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAddress);
            return View(orders.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            ViewBag.UserAddressID = new SelectList(db.Adresses, "ID", "Title");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,UserAddressID,StatusID,TotalProductPrice,TotalTaxPrice,TotalDiscount,TotalPrice,CreateDate,CreateUserID,UpdateDate,UpdateUserID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            ViewBag.UserAddressID = new SelectList(db.Adresses, "ID", "Title", order.UserAddressID);
            return View(order);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            ViewBag.UserAddressID = new SelectList(db.Adresses, "ID", "Title", order.UserAddressID);
            return View(order);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,UserAddressID,StatusID,TotalProductPrice,TotalTaxPrice,TotalDiscount,TotalPrice,CreateDate,CreateUserID,UpdateDate,UpdateUserID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            ViewBag.UserAddressID = new SelectList(db.Adresses, "ID", "Title", order.UserAddressID);
            return View(order);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
