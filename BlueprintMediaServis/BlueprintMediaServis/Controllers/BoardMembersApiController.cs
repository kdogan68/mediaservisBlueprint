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
    public class BoardMembersApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/BoardMembersApi
        public IQueryable<BoardMembers> GetBoardMembers()
        {
            return db.BoardMembers;
        }

        // GET: api/BoardMembersApi/5
        [ResponseType(typeof(BoardMembers))]
        public IHttpActionResult GetBoardMembers(int id)
        {
            BoardMembers boardMembers = db.BoardMembers.Find(id);
            if (boardMembers == null)
            {
                return NotFound();
            }

            return Ok(boardMembers);
        }
       
    }
}