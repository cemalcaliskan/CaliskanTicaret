using CaliskanTicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaliskanTicaret.UI.WEB.Controllers
{
    public class ProductController : CaliskanControllerBase
    {
        CaliskanDB db = new CaliskanDB();

        // GET: Product
        [Route("urun/{title}/{id}")]
        public ActionResult Detail(string title, int id)
        {
            var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return View(product);
        }
    }
}