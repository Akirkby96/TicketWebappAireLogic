using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TicketWebappAireLogic.Models;

namespace TicketWebappAireLogic.Controllers.API
{
    public class userTablesAPIController : ApiController
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: api/userTablesAPI
        public IQueryable<userTable> GetuserTables()
        {
            return db.userTables;
        }

        // GET: api/userTablesAPI/5
        [ResponseType(typeof(userTable))]
        public async Task<IHttpActionResult> GetuserTable(int id)
        {
            userTable userTable = await db.userTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }

            return Ok(userTable);
        }

        // PUT: api/userTablesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutuserTable(int id, userTable userTable)
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
                await db.SaveChangesAsync();
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

        // POST: api/userTablesAPI
        [ResponseType(typeof(userTable))]
        public async Task<IHttpActionResult> PostuserTable(userTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userTables.Add(userTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userTable.userID }, userTable);
        }

        // DELETE: api/userTablesAPI/5
        [ResponseType(typeof(userTable))]
        public async Task<IHttpActionResult> DeleteuserTable(int id)
        {
            userTable userTable = await db.userTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }

            db.userTables.Remove(userTable);
            await db.SaveChangesAsync();

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