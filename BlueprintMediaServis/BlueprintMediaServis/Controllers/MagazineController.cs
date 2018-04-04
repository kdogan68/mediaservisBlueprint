using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueprintMediaServis.Models;
using System.IO;
using System.Web.UI.WebControls;
using System.Reflection;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class MagazineController : Controller
    {
        public ActionResult Index2()
        {
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/Magazine";
            return Redirect(strTarget);
        }

        public ActionResult Index()        {
            
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            MagazineCategory magazineCategoryEntity = new MagazineCategory();
            IEnumerable<SelectListItem> enumarableMagazineCategoryList = magazineCategoryEntity.RetrieveMagazineCategory();

            ViewBag.Status = Session["status"];
            Session["status"] = null;            

            return View(Tuple.Create<Magazines, IEnumerable<Magazines>, MagazinesContent,IEnumerable<SelectListItem>>(new Magazines(), BMSentity.Magazines.ToList(), new MagazinesContent(), enumarableMagazineCategoryList));
        }

        [HttpPost]
        public ActionResult Insert(Magazines magazineEntity, MagazinesContent magazinesContentEntity, HttpPostedFileBase image_tr, HttpPostedFileBase image_en, HttpPostedFileBase image_ru, HttpPostedFileBase pdf_tr, HttpPostedFileBase pdf_en, HttpPostedFileBase pdf_ru)
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            int count = 0;

            if(image_tr != null)
            {
                string imagePath_tr = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr.FileName));
                magazinesContentEntity.imageFile_tr = ConvertByte(imagePath_tr, image_tr);
                count++;
            }

            if(image_en != null)
            {
                string imagePath_en = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en.FileName));
                magazinesContentEntity.imageFile_en = ConvertByte(imagePath_en, image_en);
                count++;
            }

            if (image_ru != null)
            {
                string imagePath_ru = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru.FileName));
                magazinesContentEntity.imageFile_ru = ConvertByte(imagePath_ru, image_ru);
                count++;
            }

            if(pdf_tr != null)
            {
                string pdfPath_tr = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_tr.FileName));
                magazinesContentEntity.pdfFile_tr = ConvertByte(pdfPath_tr, pdf_tr);
                count++;
            }

            if (pdf_en != null)
            {
                string pdfPath_en = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_en.FileName));
                magazinesContentEntity.pdfFile_en = ConvertByte(pdfPath_en, pdf_en);
                count++;
            }

            if (pdf_ru != null)
            {
                string pdfPath_ru = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_ru.FileName));
                magazinesContentEntity.pdfFile_ru = ConvertByte(pdfPath_ru, pdf_ru);
                count++;
            }

            if(magazinesContentEntity.title_tr != null) { count++; }
            if (magazinesContentEntity.title_en != null) { count++; }
            if (magazinesContentEntity.title_ru != null) { count++; }

            if(count == 9)
            {
                magazineEntity = new Magazines();

                magazinesContentEntity.updateTime = DateTime.Now;
                magazinesContentEntity.createTime = DateTime.Now;
                magazinesContentEntity.Magazines = magazineEntity;

                BMSentity.MagazinesContent.Add(magazinesContentEntity);
                BMSentity.Magazines.Add(magazineEntity);
                BMSentity.SaveChanges();
                Session["status"] = "success";
            }
            else if (count == 8)
            {
                magazineEntity = new Magazines();

                magazinesContentEntity.updateTime = DateTime.Now;
                magazinesContentEntity.createTime = DateTime.Now;
                magazinesContentEntity.Magazines = magazineEntity;

                BMSentity.MagazinesContent.Add(magazinesContentEntity);
                BMSentity.Magazines.Add(magazineEntity);
                BMSentity.SaveChanges();
                Session["status"] = "missing";
            }
            else
            {
                Session["status"] = "failed";
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            MagazinesContent magazineContent = entity.MagazinesContent.Find(id);
            

            if (magazineContent != null)
            {
                Magazines magazine = entity.Magazines.Find(magazineContent.magazineId);
                entity.Magazines.Remove(magazine);
                entity.MagazinesContent.Remove(magazineContent);
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

        public ActionResult DisplayPDF(int id)
        {
            var entity = new BlueprintMediaServisEntity();
            var query = entity.MagazinesContent.ToList();
            var result = query.Where(m => m.id == id).ToList();
            byte[] byteArray = result.Select(m => m.pdfFile_tr).First();
            MemoryStream pdfStream = new MemoryStream();

            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;

            return new FileStreamResult(pdfStream, "application/pdf");
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