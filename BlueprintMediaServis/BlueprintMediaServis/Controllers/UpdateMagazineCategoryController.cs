using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class UpdateMagazineCategoryController : Controller
    {

        public ActionResult Display(int id)
        {
            Session["id"] = id;

            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/mobileapp/UpdateMagazineCategory";
            return Redirect(strTarget);
        }

        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var query = BMSentity.MagazineCategory.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();

            ViewBag.Status = Session["Status"];
            Session["Status"] = null;

            return View(Tuple.Create<MagazineCategory, IEnumerable<MagazineCategory>>(new MagazineCategory(),result));
        }

        [HttpPost]
        public ActionResult Update(MagazineCategory magazineCategory)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            var categoryNamesThatEqualNew = entity.MagazineCategory.Where(w => w.text == magazineCategory.text);
            var magazineCategoryTheUpdate = entity.MagazineCategory.Where(w => w.id == magazineCategory.id).FirstOrDefault();

            if (categoryNamesThatEqualNew.Count() == 0)
            {
                
                magazineCategoryTheUpdate.text = magazineCategory.text;

                entity.SaveChanges();
                Session["Status"] = "success";
                return RedirectToAction("Index");
            }
            else if (categoryNamesThatEqualNew.Count() == 1 && categoryNamesThatEqualNew.First().text == magazineCategoryTheUpdate.text) {
                Session["Status"] = "noChange";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Status"] = "failed";
                return RedirectToAction("Index");
            }
        }      
    }
}
