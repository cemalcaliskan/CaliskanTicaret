using CaliskanTicaret.Core.Model;
using CaliskanTicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaliskanTicaret.UI.WEB.Controllers
{
    public class HomeController : CaliskanControllerBase
    {
        CaliskanDB db = new CaliskanDB();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            var data = db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(data);
        }

        public PartialViewResult GetMenu()
        {
            var menus = db.Categories.Where(x => x.ParentID == 1).ToList();
            return PartialView(menus);
        }

        [Route("uye-giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("uye-giris")]
        public ActionResult Login(string Email, string Password)
        {
            var users = db.Users.Where(x => x.Email == Email
            && x.Password == Password
            && x.IsActice == true
            && x.IsAdmin == false).ToList();

            if (users.Count == 1)
            {
                //Üye giriş yapmışsa
                Session["LoginUserID"] = users.FirstOrDefault().ID;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else
            {
                //Üye giriş yanlış ise
                ViewBag.Error = "Hatalı Kullanıcı Adı veya Şifre!!!";
                return View();
            }
        }

        [Route("uye-kayit")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Route("uye-kayit")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUserID = 1;
                entity.IsActice = true;
                entity.IsAdmin = false;

                db.Users.Add(entity);
                db.SaveChanges();

                return Redirect("/");
            }
            catch (Exception ex)
            {
                return View();
            }
            

            
        }
    }
}