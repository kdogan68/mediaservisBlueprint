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
    public class ContactApiController : ApiController
    {
        private BlueprintMediaServisEntity db = new BlueprintMediaServisEntity();

        // GET: api/ContactApi
        public IQueryable<Contact> GetContact()
        {
            return db.Contact;
        }

        // GET: api/ContactApi/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        
    }
}