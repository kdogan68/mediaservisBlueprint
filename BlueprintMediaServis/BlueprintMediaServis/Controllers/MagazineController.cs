using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueprintMediaServis.Models;
using System.IO;
using System.Web.UI.WebControls;

namespace BlueprintMediaServis.Controllers
{
    public class MagazineController : Controller
    {
        public ActionResult Index()
        {
            
            BlueprintMediaServisEntity BMSentity = new BlueprintMediaServisEntity();
           // BlueprintMediaServis.Models.Magazines      
            return View(Tuple.Create<Magazines, IEnumerable<Magazines>>(new Magazines(), BMSentity.Magazines.ToList()));
        }

        [HttpPost]
        public ActionResult Insert(Magazines magazineEntity, HttpPostedFileBase image, HttpPostedFileBase pdf)
        {
            string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image.FileName));
            string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf.FileName));

            using (BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity())
            {
                magazineEntity.imageFile = ConvertByte(imagePath, image);
                magazineEntity.pdfFile = ConvertByte(pdfPath, pdf);
                magazineEntity.updateTime = DateTime.Now;
                magazineEntity.createTime = DateTime.Now;
        
                entities.Magazines.Add(magazineEntity);
                entities.SaveChanges();
            }

           return  Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Update(Magazines magazineEntity, HttpPostedFileBase image, HttpPostedFileBase pdf)
        {
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();          
           
            var magazineTheUpdate = entities.Magazines.Where(w => w.id == magazineEntity.id).FirstOrDefault();

            if (image != null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image.FileName));
                magazineTheUpdate.imageFile = ConvertByte(imagePath, image);
            }

            if (pdf != null)
            {
                string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf.FileName));
                magazineTheUpdate.pdfFile = ConvertByte(pdfPath, pdf);
            }
                         
               
            magazineTheUpdate.imageName = magazineEntity.imageName;
            magazineTheUpdate.pdfName = magazineEntity.pdfName;
            magazineTheUpdate.title = magazineEntity.title;
            magazineTheUpdate.updateTime = DateTime.Now;             

            entities.SaveChanges();
            

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int id)
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            Magazines magazine = entity.Magazines.Find(id);
            entity.Magazines.Remove(magazine);
            entity.SaveChanges();
            return RedirectToAction("Index");            
        }
     
       


        public ActionResult DisplayPDF(int id)
        {
            var entity = new BlueprintMediaServisEntity();
            var query = entity.Magazines.ToList();
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