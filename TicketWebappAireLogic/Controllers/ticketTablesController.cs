using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketWebappAireLogic.Models;

namespace TicketWebappAireLogic.Controllers
{
    public class ticketTablesController : Controller
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: ticketTables
        public ActionResult Index()
        {
            var ticketTables = db.ticketTables.Include(t => t.userTable);
            return View(ticketTables.ToList());
        }

        // GET: ticketTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticketTable ticketTable = db.ticketTables.Find(id);
            if (ticketTable == null)
            {
                return HttpNotFound();
            }
            return View(ticketTable);
        }

        // GET: ticketTables/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName");
            return View();
        }

        // POST: ticketTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ticketID,ticketTitle,ticketDescription,ticketStatus,userID,ticketCreationTime")] ticketTable ticketTable)
        {
            if (ModelState.IsValid)
            {
                ticketTable.ticketCreationTime = DateTime.Now.ToString();
                db.ticketTables.Add(ticketTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.userTables, "userID", "userName", ticketTable.userID);
            return View(ticketTable);
        }
        //GET: ticketTables/Status/open
        public ActionResult Status(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ticketTable> ticketTables = db.ticketTables.Include(t => t.userTable).ToList();
            var status = ticketTables.Where(w => w.ticketStatus == id).ToList();

            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status.ToList());
        }

        // GET: ticketTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticketTable ticketTable = db.ticketTables.Find(id);
            if (ticketTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName", ticketTable.userID);
            return View(ticketTable);
        }

        // POST: ticketTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ticketID,ticketTitle,ticketDescription,ticketStatus,userID,ticketCreationTime")] ticketTable ticketTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName", ticketTable.userID);
            return View(ticketTable);
        }

        // GET: ticketTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticketTable ticketTable = db.ticketTables.Find(id);
            if (ticketTable == null)
            {
                return HttpNotFound();
            }
            return View(ticketTable);
        }

        // POST: ticketTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticketTable ticketTable = db.ticketTables.Find(id);
            db.ticketTables.Remove(ticketTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
