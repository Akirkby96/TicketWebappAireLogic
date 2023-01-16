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
using TicketWebappAireLogic.Models;

namespace TicketWebappAireLogic.Controllers.API
{
    public class userTablesAPIController : ApiController
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: api/userTables
        public IQueryable<userTable> GetuserTables()
        {
            return db.userTables;
        }

        // GET: api/userTables/5
        [ResponseType(typeof(userTable))]
        public IHttpActionResult GetuserTable(int id)
        {
            userTable userTable = db.userTables.Find(id);
            if (userTable == null)
            {
                return NotFound();
            }

            return Ok(userTable);
        }

        // PUT: api/userTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutuserTable(int id, userTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTable.userID)
            {
                return BadRequest();
            }

            db.Entry(userTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userTableExists(id))
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

        // POST: api/userTables
        [ResponseType(typeof(userTable))]
        public IHttpActionResult PostuserTable(userTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userTables.Add(userTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userTable.userID }, userTable);
        }

        // DELETE: api/userTables/5
        [ResponseType(typeof(userTable))]
        public IHttpActionResult DeleteuserTable(int id)
        {
            userTable userTable = db.userTables.Find(id);
            if (userTable == null)
            {
                return NotFound();
            }

            db.userTables.Remove(userTable);
            db.SaveChanges();

            return Ok(userTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userTableExists(int id)
        {
            return db.userTables.Count(e => e.userID == id) > 0;
        }
    }
}