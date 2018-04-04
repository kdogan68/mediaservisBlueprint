using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BlueprintMediaServis.Models;

namespace BlueprintMediaServis.Controllers
{
    public class VideoApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/VideoApi
        [ResponseType(typeof(Video))]
        public IHttpActionResult GetVideo()
        {
            Video video = db.Video.OrderByDescending(p => p.id).First();
           
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
           
        }

        // GET: api/VideoApi/5
        [ResponseType(typeof(Video))]
        public IHttpActionResult GetVideo(int id)
        {
            Video video = db.Video.Find(id);
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        
    }
}