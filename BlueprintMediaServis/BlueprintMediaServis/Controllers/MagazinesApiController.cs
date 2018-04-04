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

        
    }
}