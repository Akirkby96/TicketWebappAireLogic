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

namespace TicketWebappAireLogic.Controllers
{
    public class ticketTablesAPIController : ApiController
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: api/ticketTables1
        public IQueryable<ticketTable> GetticketTables()
        {
            return db.ticketTables;
        }

        // GET: api/ticketTables1/5
        [ResponseType(typeof(ticketTable))]
        public IHttpActionResult GetticketTable(int id)
        {
            ticketTable ticketTable = db.ticketTables.Find(id);
            if (ticketTable == null)
            {
                return NotFound();
            }

            return Ok(ticketTable);
        }

        // PUT: api/ticketTables1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutticketTable(int id, ticketTable ticketTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketTable.ticketID)
            {
                return BadRequest();
            }

            db.Entry(ticketTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ticketTableExists(id))
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

        // POST: api/ticketTables1
        [ResponseType(typeof(ticketTable))]
        public IHttpActionResult PostticketTable(ticketTable ticketTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ticketTable.ticketCreationTime = DateTime.Now.ToString();
            db.ticketTables.Add(ticketTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ticketTable.ticketID }, ticketTable);
        }

        // DELETE: api/ticketTables1/5
        [ResponseType(typeof(ticketTable))]
        public IHttpActionResult DeleteticketTable(int id)
        {
            ticketTable ticketTable = db.ticketTables.Find(id);
            if (ticketTable == null)
            {
                return NotFound();
            }

            db.ticketTables.Remove(ticketTable);
            db.SaveChanges();

            return Ok(ticketTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ticketTableExists(int id)
        {
            return db.ticketTables.Count(e => e.ticketID == id) > 0;
        }
    }
}