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
    public class MenuApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/MenuApi
        public IQueryable<Menu> GetMenu()
        {
            return db.Menu;
        }

        // GET: api/Menu/5
        [ResponseType(typeof(Menu))]
        public IHttpActionResult GetMagazineCategory(int id)
        {
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }


    }
}