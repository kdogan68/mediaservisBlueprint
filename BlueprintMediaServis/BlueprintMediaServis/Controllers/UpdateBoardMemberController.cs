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
    public class UpdateBoardMemberController : Controller
    {
        public ActionResult Display(int id)
        {
            Session["id"] = id;
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/mobileapp/UpdateBoardMember";

            return Redirect(strTarget);
        }
        // GET: MenuPage
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var query = BMSentity.BoardMembers.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();
            ViewBag.Status = Session["status"];
            Session["status"] = null;

            return View(Tuple.Create<BoardMembers, IEnumerable<BoardMembers>>(new BoardMembers(), result));

        }

        [HttpPost]
        public ActionResult Update(BoardMembers valuesEntity, HttpPostedFileBase image_tr6, HttpPostedFileBase image_en6, HttpPostedFileBase image_ru6)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();

            var boardMemberTheUpdate = entities.BoardMembers.Where(w => w.id == valuesEntity.id).FirstOrDefault();

            if (image_tr6 != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr6.FileName));
                boardMemberTheUpdate.image_tr = ConvertByte(imagePath, image_tr6);
            }
            if (image_en6 != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en6.FileName));
                boardMemberTheUpdate.image_en = ConvertByte(imagePath, image_en6);
            }
            if (image_ru6 != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru6.FileName));
                boardMemberTheUpdate.image_ru = ConvertByte(imagePath, image_ru6);
            }
            if (boardMemberTheUpdate != null)
            {
                boardMemberTheUpdate.imageName_tr = valuesEntity.imageName_tr;
                boardMemberTheUpdate.imageName_en = valuesEntity.imageName_en;
                boardMemberTheUpdate.imageName_ru = valuesEntity.imageName_ru;

                boardMemberTheUpdate.name_tr = valuesEntity.name_tr;
                boardMemberTheUpdate.content_tr = valuesEntity.content_tr;
                boardMemberTheUpdate.position_tr = valuesEntity.position_tr;

                boardMemberTheUpdate.name_en = valuesEntity.name_en;                
                boardMemberTheUpdate.content_en = valuesEntity.content_en;
                boardMemberTheUpdate.position_en = valuesEntity.position_en;

                boardMemberTheUpdate.name_ru = valuesEntity.name_ru;
                boardMemberTheUpdate.content_ru = valuesEntity.content_ru;
                boardMemberTheUpdate.position_ru = valuesEntity.position_ru;

                entities.SaveChanges();
                Session["status"] = "success";
            }
            else
            {
                Session["status"] = "itemNotFound";
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