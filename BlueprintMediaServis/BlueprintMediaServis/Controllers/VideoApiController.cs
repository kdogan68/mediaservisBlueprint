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

        // PUT: api/VideoApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVideo(int id, Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != video.id)
            {
                return BadRequest();
            }

            db.Entry(video).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VideoApi
        [ResponseType(typeof(Video))]
        public IHttpActionResult PostVideo(Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Video.Add(video);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = video.id }, video);
        }

        // DELETE: api/VideoApi/5
        [ResponseType(typeof(Video))]
        public IHttpActionResult DeleteVideo(int id)
        {
            Video video = db.Video.Find(id);
            if (video == null)
            {
                return NotFound();
            }

            db.Video.Remove(video);
            db.SaveChanges();

            return Ok(video);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoExists(int id)
        {
            return db.Video.Count(e => e.id == id) > 0;
        }
    }
}