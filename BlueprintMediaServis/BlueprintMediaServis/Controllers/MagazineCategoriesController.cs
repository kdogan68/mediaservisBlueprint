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
    public class MagazineCategoriesController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/MagazineCategories
        public List<MagazineCategory> GetMagazineCategory()
        {
            MagazineCategory magazineCategory = new MagazineCategory();
            return magazineCategory.RetriveMagazineCategory4API();
        }

        // GET: api/MagazineCategories/5
        [ResponseType(typeof(MagazineCategory))]
        public IHttpActionResult GetMagazineCategory(int id)
        {
            MagazineCategory magazineCategory = db.MagazineCategory.Find(id);
            if (magazineCategory == null)
            {
                return NotFound();
            }

            return Ok(magazineCategory);
        }

       

        
        

        

       
    }
}