using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();

            ViewBag.Entity = Session["entity"];
            ViewBag.Status = Session["status"];
            Session["entity"] = null;
            Session["status"] = null;

            Contact contact = new Contact();
            List<Contact> contactList = new List<Contact>();

            if (ViewBag.Entity != null)
            {
                contact.adress_tr = ViewBag.Entity.adress_tr;
                contact.adress_en = ViewBag.Entity.adress_en;
                contact.adress_ru = ViewBag.Entity.adress_ru;
                contactList.Add(contact);

                return View(Tuple.Create<Contact, IEnumerable<Contact>>(new Contact(), contactList));
            }
            else
            {
                contact.adress_tr = "";
                contact.adress_en = "";
                contact.adress_ru = "";
                contactList.Add(contact);
                return View(Tuple.Create<Contact, IEnumerable<Contact>>(new Contact(), contactList));
            }
        }

        [HttpPost]
        public ActionResult Insert (Contact contactEntity)
        {
            if (contactEntity.slug == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingSlug";
            }
            else if (contactEntity.name_tr == null || contactEntity.name_en == null || contactEntity.name_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingName";
            }
            else if (contactEntity.statu_tr == null || contactEntity.statu_en == null || contactEntity.statu_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingStatu";
            }
            else if (contactEntity.phone_tr == null || contactEntity.phone_en == null || contactEntity.phone_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingPhone";
            }
            else if (contactEntity.fax_tr == null || contactEntity.fax_en == null || contactEntity.fax_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingFax";
            }
            else if (contactEntity.mail_tr == null || contactEntity.mail_en == null || contactEntity.mail_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingMail";
            }
            else if (contactEntity.city_tr == null || contactEntity.city_en == null || contactEntity.city_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingCity";
            }
            else if (contactEntity.country_tr == null || contactEntity.country_en == null || contactEntity.country_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingCountry";
            }
            else if (contactEntity.adress_tr == null || contactEntity.adress_en == null || contactEntity.adress_ru == null)
            {
                Session["entity"] = contactEntity;
                Session["status"] = "missingAdress";
            }
            else
            {
                BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();                
                entities.Contact.Add(contactEntity);
                entities.SaveChanges();
                Session["status"] = "success";
            }
            return RedirectToAction("Index");
        }
    }
    
}