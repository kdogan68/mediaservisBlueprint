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

    public class MagazineController : Controller
    {        
        public ActionResult Index()        {
            
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
               
            return View(Tuple.Create<Magazines, IEnumerable<Magazines>, MagazinesContent>(new Magazines(), BMSentity.Magazines.ToList(), new MagazinesContent()));
        }

        [HttpPost]
        public ActionResult Insert(Magazines magazineEntity, MagazinesContent magazinesContentEntity, HttpPostedFileBase image_tr, HttpPostedFileBase image_en, HttpPostedFileBase image_ru, HttpPostedFileBase pdf_tr, HttpPostedFileBase pdf_en, HttpPostedFileBase pdf_ru)
        {
            string imagePath_tr = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr.FileName));
            string imagePath_en = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en.FileName));
            string imagePath_ru = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru.FileName));

            string pdfPath_tr = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_tr.FileName));
            string pdfPath_en = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_en.FileName));
            string pdfPath_ru = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_ru.FileName));

            magazineEntity = new Magazines();

            using (BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity())
            {

                magazinesContentEntity.imageFile_tr = ConvertByte(imagePath_tr, image_tr);
                magazinesContentEntity.imageFile_en = ConvertByte(imagePath_en, image_en);
                magazinesContentEntity.imageFile_ru = ConvertByte(imagePath_ru, image_ru);

                magazinesContentEntity.pdfFile_tr = ConvertByte(pdfPath_tr, pdf_tr);
                magazinesContentEntity.pdfFile_en = ConvertByte(pdfPath_en, pdf_en);
                magazinesContentEntity.pdfFile_ru = ConvertByte(pdfPath_ru, pdf_ru);
                magazinesContentEntity.updateTime = DateTime.Now;
                magazinesContentEntity.createTime = DateTime.Now;
                magazinesContentEntity.Magazines = magazineEntity;

                entities.MagazinesContent.Add(magazinesContentEntity);
                entities.Magazines.Add(magazineEntity);
                entities.SaveChanges();
            }

           return  Redirect(Request.UrlReferrer.ToString());
        }


        public ActionResult Delete(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            MagazinesContent magazineContent = entity.MagazinesContent.Find(id);
            Magazines magazine = entity.Magazines.Find(magazineContent.magazineId);
            
            entity.Magazines.Remove(magazine);
            entity.MagazinesContent.Remove(magazineContent);
            entity.SaveChanges();
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