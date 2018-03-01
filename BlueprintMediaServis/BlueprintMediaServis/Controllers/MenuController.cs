using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueprintMediaServis.Models;

namespace BlueprintMediaServis.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Menu menuEntity)
        {

           
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();
            menuEntity.createTime = DateTime.Now;
            entities.Menu.Add(menuEntity);
            entities.SaveChanges();


            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}