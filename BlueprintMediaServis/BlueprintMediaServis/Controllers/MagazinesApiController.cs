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
    public class MagazinesApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/MagazinesApi
        public IQueryable<Magazines> GetMagazines()
        {
            return db.Magazines;
        }

        // GET: api/MagazinesApi/5
        [ResponseType(typeof(Magazines))]
        public IHttpActionResult GetMagazines(int id)
        {
            Magazines magazines = db.Magazines.Find(id);
            if (magazines == null)
            {
                return NotFound();
            }

            return Ok(magazines);
        }

        // PUT: api/MagazinesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMagazines(int id, Magazines magazines)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != magazines.id)
            {
                return BadRequest();
            }

            db.Entry(magazines).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagazinesExists(id))
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

        // POST: api/MagazinesApi
        [ResponseType(typeof(Magazines))]
        public IHttpActionResult PostMagazines(Magazines magazines)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Magazines.Add(magazines);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = magazines.id }, magazines);
        }

        // DELETE: api/MagazinesApi/5
        [ResponseType(typeof(Magazines))]
        public IHttpActionResult DeleteMagazines(int id)
        {
            Magazines magazines = db.Magazines.Find(id);
            if (magazines == null)
            {
                return NotFound();
            }

            db.Magazines.Remove(magazines);
            db.SaveChanges();

            return Ok(magazines);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MagazinesExists(int id)
        {
            return db.Magazines.Count(e => e.id == id) > 0;
        }
    }
}