using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class MagazineCategoryController : Controller
    {
        
        // GET: MagazineCategory
        public ActionResult Index()
        {

            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            ViewBag.Status = Session["status"];
            Session["status"]=null;


            return View(Tuple.Create<MagazineCategory, IEnumerable<MagazineCategory>>(new MagazineCategory(), BMSentity.MagazineCategory.ToList()));

        }
       
        // POST: MagazineCategory/Create
        [HttpPost]
        public ActionResult Create(MagazineCategory magazineCategoryEntity)
        {           
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();
            var categoryNamesThatEqualNew = entities.MagazineCategory.Where(w => w.text == magazineCategoryEntity.text);


            if (magazineCategoryEntity.text == null)
            {
                Session["status"] = "error";
                return RedirectToAction("Index");
            }

            if (magazineCategoryEntity.text.Length < 3)
            {
                Session["status"] = "error";
                return RedirectToAction("Index");
            }

            if(categoryNamesThatEqualNew.Count() != 0)
            {
                Session["status"] = "foundEqual";
            }
            else
            {
                entities.MagazineCategory.Add(magazineCategoryEntity);
                entities.SaveChanges();

                Session["status"] = "success";
            }

            return RedirectToAction("Index"); 
        }

        // GET: MagazineCategory/Delete/5
        public ActionResult Delete(int id)
        {            
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            MagazineCategory magazineCategoryEntity = entity.MagazineCategory.Find(id);
            if (magazineCategoryEntity != null)
            {
                if (magazineCategoryEntity.MagazinesContent.Count == 0)
                {
                    entity.MagazineCategory.Remove(magazineCategoryEntity);
                    entity.SaveChanges();
                    Session["status"] = "deleted";
                    return RedirectToAction("Index");
                }else
                {
                    Session["status"] = "relatedEntity";
                    return RedirectToAction("Index");

                }
            }
            Session["status"] = "itemNotFound";
            return RedirectToAction("Index");
        }
       
    }
}
