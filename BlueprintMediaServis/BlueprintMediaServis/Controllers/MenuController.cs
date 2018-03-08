using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueprintMediaServis.Models;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class MenuController : Controller
    {
        public ActionResult Index2()
        {
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/Menu";
            return Redirect(strTarget);
        }
        public ActionResult Index()
        {

            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
               
            return View(Tuple.Create<Menu, IEnumerable<Menu>>(new Menu(), BMSentity.Menu.ToList()));
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

        public ActionResult PagesList()
        {
            



            return View();
        }

        public ActionResult MenuPage(int id)
        {
            var entity = new BlueprintMediaServisEntity();
            var query = entity.Menu.ToList();
            var result = query.Where(m => m.id == id).ToList();
            //string str1 = result[0].content.Replace("\r\n", "<br/>");
            

            ViewData["name"] = result[0].name;
            ViewData["content"] = result[0].content;



            return View();
        }
    }
}