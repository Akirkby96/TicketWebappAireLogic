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
    public class commentTablesAPIController : ApiController
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: api/commentTables
        public IQueryable<commentTable> GetcommentTables()
        {
            return db.commentTables;
        }

        // GET: api/commentTables/5
        [ResponseType(typeof(commentTable))]
        public IHttpActionResult GetcommentTable(int id)
        {
            commentTable commentTable = db.commentTables.Find(id);
            if (commentTable == null)
            {
                return NotFound();
            }

            return Ok(commentTable);
        }

        // PUT: api/commentTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutcommentTable(int id, commentTable commentTable)
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
                db.SaveChanges();
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

        // POST: api/commentTables
        [ResponseType(typeof(commentTable))]
        public IHttpActionResult PostcommentTable(commentTable commentTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.commentTables.Add(commentTable);

            try
            {
                commentTable.commentTime = DateTime.Now.ToString();
                db.SaveChanges();
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

        // DELETE: api/commentTables/5
        [ResponseType(typeof(commentTable))]
        public IHttpActionResult DeletecommentTable(int id)
        {
            commentTable commentTable = db.commentTables.Find(id);
            if (commentTable == null)
            {
                return NotFound();
            }

            db.commentTables.Remove(commentTable);
            db.SaveChanges();

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