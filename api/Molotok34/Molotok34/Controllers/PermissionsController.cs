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
    public class PermissionsController : ApiController
    {
        private db_a790a0_gseuphoriaEntities db = new db_a790a0_gseuphoriaEntities();

        // GET: api/Permissions
        public IQueryable<Permissions> GetPermissions()
        {
            return db.Permissions;
        }

        // GET: api/Permissions/5
        [ResponseType(typeof(Permissions))]
        public IHttpActionResult GetPermissions(int id)
        {
            Permissions permissions = db.Permissions.Find(id);
            if (permissions == null)
            {
                return NotFound();
            }

            return Ok(permissions);
        }

        // PUT: api/Permissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermissions(int id, Permissions permissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permissions.Id)
            {
                return BadRequest();
            }

            db.Entry(permissions).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionsExists(id))
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

        // POST: api/Permissions
        [ResponseType(typeof(Permissions))]
        public IHttpActionResult PostPermissions(Permissions permissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permissions.Add(permissions);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = permissions.Id }, permissions);
        }

        // DELETE: api/Permissions/5
        [ResponseType(typeof(Permissions))]
        public IHttpActionResult DeletePermissions(int id)
        {
            Permissions permissions = db.Permissions.Find(id);
            if (permissions == null)
            {
                return NotFound();
            }

            db.Permissions.Remove(permissions);
            db.SaveChanges();

            return Ok(permissions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionsExists(int id)
        {
            return db.Permissions.Count(e => e.Id == id) > 0;
        }
    }
}