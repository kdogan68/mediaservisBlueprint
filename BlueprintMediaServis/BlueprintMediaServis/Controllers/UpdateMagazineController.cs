using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    public class UpdateMagazineController : Controller
    {
        
        // GET: UpdateMagazine

         public ActionResult Display(int id)
         {

            Session["id"] = id;

            
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget ="http://" + strAuth + "/UpdateMagazine";
            return Redirect(strTarget);
        }

        public ActionResult Index()
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            var query = entity.MagazinesContent.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();

            ViewData["ttle_tr"] = result[0].title_tr;
            ViewData["imageName_tr"] = result[0].imageName_tr;
            ViewData["pdfName_tr"] = result[0].pdfName_tr;
            ViewData["ttle_en"] = result[0].title_en;
            ViewData["imageName_en"] = result[0].imageName_en;
            ViewData["pdfName_en"] = result[0].pdfName_en;
            ViewData["ttle_ru"] = result[0].title_ru;
            ViewData["imageName_ru"] = result[0].imageName_ru;
            ViewData["pdfName_ru"] = result[0].pdfName_ru;



            return View(result[0]);
            

        
        }


        [HttpPost]
        public ActionResult Update(Magazines magazineEntity,MagazinesContent magazineContentEntity, HttpPostedFileBase image, HttpPostedFileBase pdf)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();

            var magazineContentTheUpdate = entities.MagazinesContent.Where(w => w.id == magazineContentEntity.id).FirstOrDefault();

            if (image != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image.FileName));
                magazineContentTheUpdate.imageFile_tr = ConvertByte(imagePath, image);
            }

            if (pdf != null)
            {
                string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf.FileName));
                magazineContentTheUpdate.pdfFile = ConvertByte(pdfPath, pdf);
            }


            magazineContentTheUpdate.imageName = magazineContentEntity.imageName;
            magazineContentTheUpdate.pdfName = magazineContentEntity.pdfName;
            magazineContentTheUpdate.title = magazineContentEntity.title;
            magazineContentTheUpdate.language1 = magazineContentEntity.language1;
            magazineContentTheUpdate.updateTime = DateTime.Now;

            entities.SaveChanges();


            return Redirect(Request.UrlReferrer.ToString());
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