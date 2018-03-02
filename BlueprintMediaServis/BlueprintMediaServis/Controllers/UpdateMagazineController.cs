using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    public class UpdateMagazineController : Controller
    {
        // GET: UpdateMagazine
        public ActionResult Index(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            var query = entity.Magazines.ToList();
            var result = query.Where(m => m.id == id).ToList();

            ViewData["ttle"] = result[0].title;
            ViewData["imageName"] = result[0].imageName;

            ViewData["pdfName"] = result[0].pdfName;
            ViewData["id"] = result[0].id;

            return View();
        }
         
    }
}