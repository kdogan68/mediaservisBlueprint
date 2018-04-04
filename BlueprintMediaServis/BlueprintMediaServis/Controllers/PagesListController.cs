using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class PagesListController : Controller
    {
        public ActionResult Index2()
        {
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/PagesList";
            return Redirect(strTarget);
        }
        // GET: PagesList
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();            
            var model = BMSentity.Menu;

            ViewBag.Status = Session["status"];
            Session["status"] = null;

            return View(model);           
        }

        public ActionResult DeletePage(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            Menu menuEntity = entity.Menu.Find(id);

            if (menuEntity != null)
            {
                Menu menu = entity.Menu.Find(menuEntity.id);
                entity.Menu.Remove(menu);              
                entity.SaveChanges();
                Session["status"] = "deleted";
                return RedirectToAction("Index");
            }
            else
            {
                Session["status"] = "itemNotFound";
            }

            return RedirectToAction("Index");            
        }
    }
}