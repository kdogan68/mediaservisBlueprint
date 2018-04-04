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

        
    }
}