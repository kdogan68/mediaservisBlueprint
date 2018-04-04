using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class BoardMembersListController : Controller
    {
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var model = BMSentity.BoardMembers;

            ViewBag.Status = Session["status"];
            Session["status"] = null;

            return View(model);
        }

        public ActionResult DeleteValue(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            BoardMembers boardMembersEntity = entity.BoardMembers.Find(id);

            if (boardMembersEntity != null)
            {
                BoardMembers boardMembers = entity.BoardMembers.Find(boardMembersEntity.id);
                entity.BoardMembers.Remove(boardMembers);
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