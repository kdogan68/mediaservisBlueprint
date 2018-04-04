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
    public class UpdateMagazineController : Controller
    {
        
        // GET: UpdateMagazine

         public ActionResult Display(int id)
         {

            Session["id"] = id;

            
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget ="http://" + strAuth + "/mobileapp/UpdateMagazine";
            return Redirect(strTarget);
        }

        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            MagazineCategory magazineCategoryEntity = new MagazineCategory();

            IEnumerable<SelectListItem> enumarableMagazineCategoryList = magazineCategoryEntity.RetrieveMagazineCategory();

            var query = BMSentity.MagazinesContent.ToList();
            var result = query.Where(m => m.id == (int)Session["id"]).ToList();

           
            ViewData["category"] = result[0].categoryId;


            return View(Tuple.Create<MagazinesContent, IEnumerable<SelectListItem>, IEnumerable<MagazinesContent>>(new MagazinesContent(), enumarableMagazineCategoryList, result));
            
        }


        [HttpPost]
        public ActionResult Update(Magazines magazineEntity, MagazinesContent magazineContentEntity, HttpPostedFileBase image_tr, HttpPostedFileBase image_en, HttpPostedFileBase image_ru, HttpPostedFileBase pdf_tr, HttpPostedFileBase pdf_en, HttpPostedFileBase pdf_ru)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();

            var magazineContentTheUpdate = entities.MagazinesContent.Where(w => w.id == magazineContentEntity.id).FirstOrDefault();

            if (image_tr != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_tr.FileName));
                magazineContentTheUpdate.imageFile_tr = ConvertByte(imagePath, image_tr);
            }
            if (image_en != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_en.FileName));
                magazineContentTheUpdate.imageFile_en = ConvertByte(imagePath, image_en);
            }
            if (image_ru != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image_ru.FileName));
                magazineContentTheUpdate.imageFile_ru = ConvertByte(imagePath, image_ru);
            }


            if (pdf_tr != null)
            {
                string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_tr.FileName));
                magazineContentTheUpdate.pdfFile_tr = ConvertByte(pdfPath, pdf_tr);
            }
            if (pdf_en != null)
            {
                string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_en.FileName));
                magazineContentTheUpdate.pdfFile_en = ConvertByte(pdfPath, pdf_en);
            }
            if (pdf_ru != null)
            {
                string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf_ru.FileName));
                magazineContentTheUpdate.pdfFile_ru = ConvertByte(pdfPath, pdf_ru);
            }


            magazineContentTheUpdate.imageName_tr = magazineContentEntity.imageName_tr;
            magazineContentTheUpdate.imageName_en = magazineContentEntity.imageName_en;
            magazineContentTheUpdate.imageName_ru = magazineContentEntity.imageName_ru;
            magazineContentTheUpdate.pdfName_tr = magazineContentEntity.pdfName_tr;
            magazineContentTheUpdate.pdfName_en = magazineContentEntity.pdfName_en;
            magazineContentTheUpdate.pdfName_ru = magazineContentEntity.pdfName_ru;
            magazineContentTheUpdate.title_tr = magazineContentEntity.title_tr;
            magazineContentTheUpdate.title_en = magazineContentEntity.title_en;
            magazineContentTheUpdate.title_ru = magazineContentEntity.title_ru;
            magazineContentTheUpdate.categoryId = magazineContentEntity.categoryId;

            magazineContentTheUpdate.updateTime = DateTime.Now;
                        
            entities.SaveChanges();

            Session["status"] = "success";


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