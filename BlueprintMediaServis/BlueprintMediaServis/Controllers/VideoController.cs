using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class VideoController : Controller
    {
        public ActionResult Index2()
        {
            string strAuth = Request.UrlReferrer.Authority.ToString();
            string strTarget = "http://" + strAuth + "/Video";
            return Redirect(strTarget);
        }
        public ActionResult Index()
        {
            if (RetrieveSingle() != null)
            {
                ViewData["embedcode_tr"] = RetrieveSingle().embedcode_tr;
                ViewData["embedcode_en"] = RetrieveSingle().embedcode_en;
                ViewData["embedcode_ru"] = RetrieveSingle().embedcode_ru;
            }
            ViewBag.Status = Session["status"];
            Session["status"] = null;


            return View();
        }


        [HttpPost]
        public ActionResult Insert(Video videoEntity)
        {
            if (videoEntity.url_tr != null && videoEntity.url_en != null && videoEntity.url_ru != null)
            {
                
                string embedYoutubeUrlTr = YoutubeURLConverter(videoEntity.url_tr);
                string embedYoutubeUrlEn = YoutubeURLConverter(videoEntity.url_en);
                string embedYoutubeUrlRu = YoutubeURLConverter(videoEntity.url_ru);

                if (embedYoutubeUrlTr != "" && embedYoutubeUrlEn != "" && embedYoutubeUrlRu != "")
                {
                    BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();
                    videoEntity.embedcode_tr = embedYoutubeUrlTr;
                    videoEntity.embedcode_en = embedYoutubeUrlEn;
                    videoEntity.embedcode_ru = embedYoutubeUrlRu;
                    videoEntity.createTime = DateTime.Now;

                    entities.Video.Add(videoEntity);
                    entities.SaveChanges();
                    Session["status"] = "success";
                }
                else
                {
                    Session["status"] = "error";
                }
                
            }
            else
            {
                Session["status"] = "noChange";
            }

            return RedirectToAction("Index");
        }

        public Video RetrieveSingle()
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            var query = entity.Video.ToList();
            var query2 = query.OrderByDescending(v => v.id);
            var result = query2.FirstOrDefault();

            if(result != null)
            {
                return result;
            }

            return null;
        }

        public string YoutubeURLConverter(string url)
        {
            var YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            return youtubeMatch.Success ? "http://www.youtube.com/embed/" + youtubeMatch.Groups[1].Value : string.Empty;
        }


    }
}