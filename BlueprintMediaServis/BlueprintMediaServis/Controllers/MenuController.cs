using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueprintMediaServis.Models;
using System.IO;

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
                   
            ViewBag.Entity = Session["entity"];
            ViewBag.Status = Session["status"];
            Session["entity"] = null;
            Session["status"] = null;

            Menu menu = new Menu();
            List<Menu> menuList = new List<Menu>(); 

            if(ViewBag.Entity != null)
            {
                menu.content_tr = ViewBag.Entity.content_tr;
                menu.content_en = ViewBag.Entity.content_en;
                menu.content_ru = ViewBag.Entity.content_ru;
                menuList.Add(menu);

                return View(Tuple.Create<Menu, IEnumerable<Menu>>(new Menu(), menuList));
            }
            else
            {
                menu.content_tr = "";
                menu.content_en = "";
                menu.content_ru = "";
                menuList.Add(menu);
                return View(Tuple.Create<Menu, IEnumerable<Menu>>(new Menu(), menuList));
            }
            
        }

        [HttpPost]
        public ActionResult Insert(Menu menuEntity, HttpPostedFileBase image_tr5, HttpPostedFileBase image_en5, HttpPostedFileBase image_ru5)
        {
            if (image_tr5 != null)
            {
                string imagePath_tr = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr5.FileName));
                menuEntity.image_tr = ConvertByte(imagePath_tr, image_tr5);                
            }

            if (image_en5 != null)
            {
                string imagePath_en = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en5.FileName));
                menuEntity.image_en = ConvertByte(imagePath_en, image_en5);               
            }

            if (image_ru5 != null)
            {
                string imagePath_ru = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru5.FileName));
                menuEntity.image_ru = ConvertByte(imagePath_ru, image_ru5);                
            }

            if (menuEntity.slug == null)
            {
                Session["entity"] = menuEntity;
                Session["status"] = "missingSlug";
            }
            else if (menuEntity.name_tr == null || menuEntity.name_en == null || menuEntity.name_ru == null)
            {
                Session["entity"] = menuEntity;
                Session["status"] = "missingTitle";
            }
            else if (menuEntity.content_tr == null || menuEntity.content_en == null || menuEntity.content_ru == null)
            {
                Session["entity"] = menuEntity;
                Session["status"] = "missingContent";
            }
            else
            {
                BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();
                menuEntity.createTime = DateTime.Now;
                entities.Menu.Add(menuEntity);
                entities.SaveChanges();
                Session["status"] = "success";
            }


            return RedirectToAction("Index");
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
       
            ViewData["name_tr"] = result[0].name_tr;
            ViewData["content_tr"] = result[0].content_tr;
            ViewData["name_en"] = result[0].name_en;
            ViewData["content_en"] = result[0].content_en;
            ViewData["name_ru"] = result[0].name_ru;
            ViewData["content_ru"] = result[0].content_ru;

            return View();
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
