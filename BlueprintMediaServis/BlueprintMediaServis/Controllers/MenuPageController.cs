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
    public class MenuPageController : Controller
    {
        public ActionResult Display(int id)
        {
            Session["id"] = id;
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/mobileapp/MenuPage";

            return Redirect(strTarget);
        }
        // GET: MenuPage
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            var query = BMSentity.Menu.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();
            ViewBag.Status = Session["status"];
            Session["status"] = null;

            return View(Tuple.Create<Menu, IEnumerable<Menu>>(new Menu(), result));
            
        }       

        [HttpPost]
        public ActionResult Update(Menu menuEntity,HttpPostedFileBase image_tr6, HttpPostedFileBase image_en6, HttpPostedFileBase image_ru6)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();           

            var menuTheUpdate = entities.Menu.Where(w => w.id == menuEntity.id).FirstOrDefault();

            if (image_tr6 != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr6.FileName));
                menuTheUpdate.image_tr = ConvertByte(imagePath, image_tr6);
            }
            if (image_en6 != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en6.FileName));
                menuTheUpdate.image_en = ConvertByte(imagePath, image_en6);
            }
            if (image_ru6 != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru6.FileName));
                menuTheUpdate.image_ru = ConvertByte(imagePath, image_ru6);
            }

            if (menuTheUpdate != null)
            {
                menuTheUpdate.imageName_tr = menuEntity.imageName_tr;
                menuTheUpdate.imageName_en = menuEntity.imageName_en;
                menuTheUpdate.imageName_ru = menuEntity.imageName_ru;
                menuTheUpdate.slug = menuEntity.slug;
                menuTheUpdate.name_tr = menuEntity.name_tr;
                menuTheUpdate.content_tr = menuEntity.content_tr;
                menuTheUpdate.name_en = menuEntity.name_en;
                menuTheUpdate.content_en = menuEntity.content_en;
                menuTheUpdate.name_ru = menuEntity.name_ru;
                menuTheUpdate.content_ru = menuEntity.content_ru;

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