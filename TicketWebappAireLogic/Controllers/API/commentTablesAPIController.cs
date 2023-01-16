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
    public class commentTablesAPIController : ApiController
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: api/commentTablesAPI
        public IQueryable<commentTable> GetcommentTables()
        {
            return db.commentTables;
        }

        // GET: api/commentTablesAPI/5
        [ResponseType(typeof(commentTable))]
        public async Task<IHttpActionResult> GetcommentTable(int id)
        {
            commentTable commentTable = await db.commentTables.FindAsync(id);
            if (commentTable == null)
            {
                return NotFound();
            }

            return Ok(commentTable);
        }

        // PUT: api/commentTablesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutcommentTable(int id, commentTable commentTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commentTable.commentID)
            {
                return BadRequest();
            }

            db.Entry(commentTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!commentTableExists(id))
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

        // POST: api/commentTablesAPI
        [ResponseType(typeof(commentTable))]
        public async Task<IHttpActionResult> PostcommentTable(commentTable commentTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.commentTables.Add(commentTable);

            try
            {
                commentTable.commentTime = DateTime.Now.ToString();
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (commentTableExists(commentTable.commentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = commentTable.commentID }, commentTable);
        }

        // DELETE: api/commentTablesAPI/5
        [ResponseType(typeof(commentTable))]
        public async Task<IHttpActionResult> DeletecommentTable(int id)
        {
            commentTable commentTable = await db.commentTables.FindAsync(id);
            if (commentTable == null)
            {
                return NotFound();
            }

            db.commentTables.Remove(commentTable);
            await db.SaveChangesAsync();

            return Ok(commentTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool commentTableExists(int id)
        {
            return db.commentTables.Count(e => e.commentID == id) > 0;
        }
    }
}