using CaliskanTicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaliskanTicaret.UI.WEB.Controllers
{
    public class CategoryController : CaliskanControllerBase
    {
        CaliskanDB db = new CaliskanDB();

        // GET: Category
        [Route("kategori/{isim}/{id}")]
        public ActionResult Index(string isim, int id)
        {
            var data = db.Products.Where(x => x.IsActive == true && x.CategoryID == id).ToList();
            ViewBag.category = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
    }
}