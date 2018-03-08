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
    public class MagazinesContentsApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/MagazinesContentsApi
        public IQueryable<MagazinesContent> GetMagazinesContent()
        {
            return db.MagazinesContent;
        }

        // GET: api/MagazinesContentsApi/5
        [ResponseType(typeof(MagazinesContent))]
        public IHttpActionResult GetMagazinesContent(int id)
        {
            MagazinesContent magazinesContent = db.MagazinesContent.Find(id);
            if (magazinesContent == null)
            {
                return NotFound();
            }

            return Ok(magazinesContent);
        }

        // PUT: api/MagazinesContentsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMagazinesContent(int id, MagazinesContent magazinesContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != magazinesContent.id)
            {
                return BadRequest();
            }

            db.Entry(magazinesContent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagazinesContentExists(id))
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

        // POST: api/MagazinesContentsApi
        [ResponseType(typeof(MagazinesContent))]
        public IHttpActionResult PostMagazinesContent(MagazinesContent magazinesContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MagazinesContent.Add(magazinesContent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = magazinesContent.id }, magazinesContent);
        }

        // DELETE: api/MagazinesContentsApi/5
        [ResponseType(typeof(MagazinesContent))]
        public IHttpActionResult DeleteMagazinesContent(int id)
        {
            MagazinesContent magazinesContent = db.MagazinesContent.Find(id);
            if (magazinesContent == null)
            {
                return NotFound();
            }

            db.MagazinesContent.Remove(magazinesContent);
            db.SaveChanges();

            return Ok(magazinesContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MagazinesContentExists(int id)
        {
            return db.MagazinesContent.Count(e => e.id == id) > 0;
        }
    }
}