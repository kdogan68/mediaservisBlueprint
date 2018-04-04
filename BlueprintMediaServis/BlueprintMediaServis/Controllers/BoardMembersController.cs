using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class BoardMembersController : Controller
    {        
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();

            ViewBag.Entity = Session["entity"];
            ViewBag.Status = Session["status"];
            Session["entity"] = null;
            Session["status"] = null;

            BoardMembers boardMembers = new BoardMembers();
            List<BoardMembers> boardMembersList = new List<BoardMembers>();

            if (ViewBag.Entity != null)
            {
                boardMembers.content_tr = ViewBag.Entity.content_tr;
                boardMembers.content_en = ViewBag.Entity.content_en;
                boardMembers.content_ru = ViewBag.Entity.content_ru;
                boardMembersList.Add(boardMembers);

                return View(Tuple.Create<BoardMembers, IEnumerable<BoardMembers>>(new BoardMembers(), boardMembersList));
            }
            else
            {
                boardMembers.content_tr = "";
                boardMembers.content_en = "";
                boardMembers.content_ru = "";
                boardMembersList.Add(boardMembers);
                return View(Tuple.Create<BoardMembers, IEnumerable<BoardMembers>>(new BoardMembers(), boardMembersList));
            }            
        }

        [HttpPost]
        public ActionResult Insert(BoardMembers boardMembersEntity,HttpPostedFileBase image_tr7, HttpPostedFileBase image_en7, HttpPostedFileBase image_ru7)
        {

            if (image_tr7 != null)
            {
                string imagePath_tr = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr7.FileName));
                boardMembersEntity.image_tr = ConvertByte(imagePath_tr, image_tr7);
            }

            if (image_en7 != null)
            {
                string imagePath_en = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en7.FileName));
                boardMembersEntity.image_en = ConvertByte(imagePath_en, image_en7);
            }

            if (image_ru7 != null)
            {
                string imagePath_ru = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru7.FileName));
                boardMembersEntity.image_ru = ConvertByte(imagePath_ru, image_ru7);
            }
            
            if (boardMembersEntity.name_tr == null || boardMembersEntity.name_en == null || boardMembersEntity.name_ru == null)
            {
                Session["entity"] = boardMembersEntity;
                Session["status"] = "missingname";
            }
            else if (boardMembersEntity.position_tr == null || boardMembersEntity.position_en == null || boardMembersEntity.position_ru == null)
            {
                Session["entity"] = boardMembersEntity;
                Session["status"] = "missingPosition";
            }
            else if (boardMembersEntity.content_tr == null || boardMembersEntity.content_en == null || boardMembersEntity.content_ru == null)
            {
                Session["entity"] = boardMembersEntity;
                Session["status"] = "missingContent";
            }
            else
            {
                BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();
                boardMembersEntity.createTime = DateTime.Now;
                entities.BoardMembers.Add(boardMembersEntity);
                entities.SaveChanges();
                Session["status"] = "success";
            }


            return RedirectToAction("Index");
            
        }

        private Byte[] ConvertByte(string filePath, HttpPostedFileBase f)
        {
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension

            switch (ext)
            {
                case ".jpg":
                    contenttype = "image/jpg";
                    break;

                case ".png":
                    contenttype = "image/png";
                    break;

                case ".pdf":
                    contenttype = "application/pdf";
                    break;
            }

            if (contenttype != String.Empty)
            {
                Stream fs = f.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                return bytes;
            }

            return null;
        }


    }
}
