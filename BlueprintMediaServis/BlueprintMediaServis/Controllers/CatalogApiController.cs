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
    public class CatalogApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/CatalogApi
        public IQueryable<Catalog> GetCatalog()
        {
            return db.Catalog;
        }

        // GET: api/CatalogApi/5
        [ResponseType(typeof(Catalog))]
        public IHttpActionResult GetCatalog(int id)
        {
            Catalog catalog = db.Catalog.Find(id);
            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
        }


        // PUT: api/CatalogApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCatalog(int id, Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catalog.id)
            {
                return BadRequest();
            }

            db.Entry(catalog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogExists(id))
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

        // POST: api/CatalogApi
        [ResponseType(typeof(Catalog))]
        public IHttpActionResult PostCatalog(Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Catalog.Add(catalog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = catalog.id }, catalog);
        }

        // DELETE: api/CatalogApi/5
        [ResponseType(typeof(Catalog))]
        public IHttpActionResult DeleteCatalog(int id)
        {
            Catalog catalog = db.Catalog.Find(id);
            if (catalog == null)
            {
                return NotFound();
            }

            db.Catalog.Remove(catalog);
            db.SaveChanges();

            return Ok(catalog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CatalogExists(int id)
        {
            return db.Catalog.Count(e => e.id == id) > 0;
        }
    }
}