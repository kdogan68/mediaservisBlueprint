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
        public IQueryable GetMagazinesContent(int id)
        {
          // List<MagazinesContent> magazinesContent = db.MagazinesContent.Where(M => magazinesContent.categoryId == id).ToList();           

            return db.MagazinesContent.Where(M => M.categoryId == id); ;
        }

      
    }
}