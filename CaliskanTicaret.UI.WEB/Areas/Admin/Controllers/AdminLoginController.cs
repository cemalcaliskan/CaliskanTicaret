using CaliskanTicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaliskanTicaret.UI.WEB.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        CaliskanDB db = new CaliskanDB();

        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {
            var data = db.Users.Where(x => x.Email == Email 
            && x.Password == Password
            && x.IsActice == true
            && x.IsAdmin == true).ToList();

            if (data.Count == 1)
            {
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return Redirect("/admin");
            }
            else
            {
                return View();
            }
        }
    }
}