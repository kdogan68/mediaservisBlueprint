using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class ContactListController : Controller
    {
        // GET: ContactList
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var model = BMSentity.Contact;

            ViewBag.Status = Session["status"];
            Session["status"] = null;

            return View(model);
        }

        public ActionResult DeleteContact(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            Contact contactEntity = entity.Contact.Find(id);

            if (contactEntity != null)
            {
                Contact contact = entity.Contact.Find(contactEntity.id);
                entity.Contact.Remove(contact);
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