using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueprintMediaServis.Models;
using System.IO;
using System.Collections;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class CatalogController : Controller
    {
        public ActionResult Index2()
        {
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/Catalog";
            return Redirect(strTarget);
        }
        public ActionResult Index()
        {
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem() { Text = "Türkçe", Value = "1", Selected = false };
            SelectListItem item2 = new SelectListItem() { Text = "İngilizce", Value = "2", Selected = false };
            SelectListItem item3 = new SelectListItem() { Text = "Rusça", Value = "3", Selected = true };

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            ViewBag.LanguageItem = items;



            // BlueprintMediaServis.Models.Magazines      
            return View(Tuple.Create<Catalog, IEnumerable<Catalog>>(new Catalog(), BMSentity.Catalog.ToList()));
        }
    
        [HttpPost]
        public ActionResult Insert(Catalog catalogEntity, HttpPostedFileBase image, HttpPostedFileBase pdf)
        {
            string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image.FileName));
            string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf.FileName));

            using (BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity())
            {

                catalogEntity.imageFile = ConvertByte(imagePath, image);
                catalogEntity.pdfFile = ConvertByte(pdfPath, pdf);
               
                catalogEntity.createTime = DateTime.Now;

                entities.Catalog.Add(catalogEntity);
                entities.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Update(Catalog catalogEntity, HttpPostedFileBase image, HttpPostedFileBase pdf)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();

            var catalogTheUpdate = entities.Catalog.Where(w => w.language == catalogEntity.language.ToString()).FirstOrDefault();

            if (image != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image.FileName));
                catalogTheUpdate.imageFile = ConvertByte(imagePath, image);
            }

            if (pdf != null)
            {
                string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf.FileName));
                catalogTheUpdate.pdfFile = ConvertByte(pdfPath, pdf);
            }

            catalogTheUpdate.imageName = catalogEntity.imageName;
            catalogTheUpdate.pdfName = catalogEntity.pdfName;
            catalogTheUpdate.title = catalogEntity.title;
            catalogTheUpdate.createTime = DateTime.Now;

            entities.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult RetrieveSingle(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            var query = entity.Catalog.ToList();
            var result = query.Where(m => m.id == id).ToList();

            ViewData["ttle"] = result[0].title;
            ViewData["imageName"] = result[0].imageName;

            ViewData["pdfName"] = result[0].pdfName;
            ViewData["id"] = result[0].id;

            return PartialView("UpdateCatalog", result[0]);
        }
        
        public ActionResult DisplayPDF(int id)
        {
            var entity = new BlueprintMediaServisEntity();
            var query = entity.Catalog.ToList();
            var result = query.Where(m => m.id == id).ToList();
            byte[] byteArray = result.Select(m => m.pdfFile).First();
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