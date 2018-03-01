using BlueprintMediaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    public class VideoController : Controller
    {
        public ActionResult Index()
        {
            if (RetrieveSingle() != null)
            {
                ViewData["embedcode"] = RetrieveSingle();
            }


            return View();
        }


        [HttpPost]
        public ActionResult Insert(Video videoEntity)
        {
            string embedYoutubeUrl = YoutubeURLConverter(videoEntity.url);
            BlueprintMediaServisEntity entities = new BlueprintMediaServisEntity();
            videoEntity.url = embedYoutubeUrl;
            videoEntity.createTime = DateTime.Now;

            entities.Video.Add(videoEntity);
            entities.SaveChanges();


            return Redirect(Request.UrlReferrer.ToString());
        }

        public string RetrieveSingle()
        {
            BlueprintMediaServisEntity entity = new BlueprintMediaServisEntity();
            var query = entity.Video.ToList();
            var query2 = query.OrderByDescending(v => v.id);
            var result = query2.FirstOrDefault();

            if(result != null)
            {
                return result.url;
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