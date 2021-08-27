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
using Molotok34.Models;

namespace Molotok34.Controllers
{
    public class AdminsController : ApiController
    {
        private db_a790a0_gseuphoriaEntities db = new db_a790a0_gseuphoriaEntities();

        // GET: api/Admins
        public IQueryable<Admins> GetAdmins()
        {
            return db.Admins;
        }

        // GET: api/Admins/5
        [ResponseType(typeof(Admins))]
        public IHttpActionResult GetAdmins(int id)
        {
            Admins admins = db.Admins.Find(id);
            if (admins == null)
            {
                return NotFound();
            }

            return Ok(admins);
        }

        // PUT: api/Admins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdmins(int id, Admins admins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admins.Id)
            {
                return BadRequest();
            }

            db.Entry(admins).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminsExists(id))
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

        // POST: api/Admins
        [ResponseType(typeof(Admins))]
        public IHttpActionResult PostAdmins(Admins admins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admins.Add(admins);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = admins.Id }, admins);
        }

        // DELETE: api/Admins/5
        [ResponseType(typeof(Admins))]
        public IHttpActionResult DeleteAdmins(int id)
        {
            Admins admins = db.Admins.Find(id);
            if (admins == null)
            {
                return NotFound();
            }

            db.Admins.Remove(admins);
            db.SaveChanges();

            return Ok(admins);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminsExists(int id)
        {
            return db.Admins.Count(e => e.Id == id) > 0;
        }
    }
}