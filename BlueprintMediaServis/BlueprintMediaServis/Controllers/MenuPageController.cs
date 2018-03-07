using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    public class MenuPageController : Controller
    {
        public ActionResult Display(int id)
        {
            Session["id"] = id;
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/MenuPage";

            return Redirect(strTarget);
        }
        // GET: MenuPage
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var query = BMSentity.Menu.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();

            return View(Tuple.Create<Menu, IEnumerable<Menu>>(new Menu(), result));
            
        }

        [HttpPost]
        public ActionResult Update(Menu menuEntity)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();

            var menuTheUpdate = entities.Menu.Where(w => w.id == menuEntity.id).FirstOrDefault();

            menuTheUpdate.name = menuEntity.name;
            menuTheUpdate.content = menuEntity.content;
            menuTheUpdate.language = menuEntity.language;            

            entities.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }


    }
}