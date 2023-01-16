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

namespace TicketWebappAireLogic.Controllers
{
    public class ticketTablesAPIController : ApiController
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: api/ticketTablesAPI
        public IQueryable<ticketTable> GetticketTables()
        {
            return db.ticketTables;
        }

        // GET: api/ticketTablesAPI/5
        [ResponseType(typeof(ticketTable))]
        public async Task<IHttpActionResult> GetticketTable(int id)
        {
            ticketTable ticketTable = await db.ticketTables.FindAsync(id);
            if (ticketTable == null)
            {
                return NotFound();
            }

            return Ok(ticketTable);
        }

        // PUT: api/ticketTablesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutticketTable(int id, ticketTable ticketTable)
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
                await db.SaveChangesAsync();
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

        // POST: api/ticketTablesAPI
        [ResponseType(typeof(ticketTable))]
        public async Task<IHttpActionResult> PostticketTable(ticketTable ticketTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ticketTable.ticketCreationTime = DateTime.Now.ToString();
            db.ticketTables.Add(ticketTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ticketTable.ticketID }, ticketTable);
        }

        // DELETE: api/ticketTablesAPI/5
        [ResponseType(typeof(ticketTable))]
        public async Task<IHttpActionResult> DeleteticketTable(int id)
        {
            ticketTable ticketTable = await db.ticketTables.FindAsync(id);
            if (ticketTable == null)
            {
                return NotFound();
            }

            db.ticketTables.Remove(ticketTable);
            await db.SaveChangesAsync();

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