using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class UpdateContactController : Controller
    {
        public ActionResult Display(int id)
        {
            Session["id"] = id;
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/mobileapp/UpdateContact";

            return Redirect(strTarget);
        }
        // GET: UpdateContact
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var query = BMSentity.Contact.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();
            ViewBag.Status = Session["status"];
            Session["status"] = null;

            return View(Tuple.Create<Contact, IEnumerable<Contact>>(new Contact(), result));
           
        }

        [HttpPost]
        public ActionResult Update(Contact contactEntity)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();

            var contactTheUpdate = entities.Contact.Where(w => w.id == contactEntity.id).FirstOrDefault();            

            if (contactTheUpdate != null)
            {
                contactTheUpdate.slug = contactEntity.slug;

                contactTheUpdate.name_tr = contactEntity.name_tr;
                contactTheUpdate.name_en = contactEntity.name_en;
                contactTheUpdate.name_ru = contactEntity.name_ru;

                contactTheUpdate.statu_tr = contactEntity.statu_tr;
                contactTheUpdate.statu_en = contactEntity.statu_en;
                contactTheUpdate.statu_ru = contactEntity.statu_ru;

                contactTheUpdate.phone_tr = contactEntity.phone_tr;
                contactTheUpdate.phone_en = contactEntity.phone_en;
                contactTheUpdate.phone_ru = contactEntity.phone_ru;

                contactTheUpdate.fax_tr = contactEntity.fax_tr;
                contactTheUpdate.fax_en = contactEntity.fax_en;
                contactTheUpdate.fax_ru = contactEntity.fax_ru;

                contactTheUpdate.mail_tr = contactEntity.mail_tr;
                contactTheUpdate.mail_en = contactEntity.mail_en;
                contactTheUpdate.mail_ru = contactEntity.mail_ru;

                contactTheUpdate.city_tr = contactEntity.city_tr;
                contactTheUpdate.city_en = contactEntity.city_en;
                contactTheUpdate.city_ru = contactEntity.city_ru;

                contactTheUpdate.country_tr = contactEntity.country_tr;
                contactTheUpdate.country_en = contactEntity.country_en;
                contactTheUpdate.country_ru = contactEntity.country_ru;

                contactTheUpdate.adress_tr = contactEntity.adress_tr;
                contactTheUpdate.adress_en = contactEntity.adress_en;
                contactTheUpdate.adress_ru = contactEntity.adress_ru;

                entities.SaveChanges();
                Session["status"] = "success";
            }
            else
            {
                Session["status"] = "itemNotFound";
            }

            return RedirectToAction("Index");
        }
    }
}