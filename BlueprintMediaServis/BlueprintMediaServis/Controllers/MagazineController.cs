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
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Magazine magazineEntity, HttpPostedFileBase image, HttpPostedFileBase pdf)
        {
            string imagePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdf.FileName));
            string pdfPath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(image.FileName));


            using (BlueprintMediaServisEntities1 entities = new BlueprintMediaServisEntities1())

            {
                magazineEntity.imageFile = ConvertByte(imagePath, image);
                magazineEntity.pdfFile = ConvertByte(pdfPath, pdf);

                //   magazineEntity.pdfFile = new byte[i.ContentLength];

                entities.Magazines.Add(magazineEntity);

                entities.SaveChanges();
            }
           return  Redirect(Request.UrlReferrer.ToString());

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        protected Byte[] ConvertByte(string filePath, HttpPostedFileBase f)
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