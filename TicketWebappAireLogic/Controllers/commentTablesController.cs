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
    public class commentTablesController : Controller
    {
        private ticketDBEntities2 db = new ticketDBEntities2();

        // GET: commentTables
        public ActionResult Index()
        {
            var commentTables = db.commentTables.Include(c => c.ticketTable).Include(c => c.userTable);
            return View(commentTables.ToList());
        }
        public ActionResult SortByTicket(string id)
        {
            List<commentTable> commentTables = db.commentTables.Include(c => c.ticketTable).Include(c => c.userTable).ToList();
            var sortedComments = commentTables.Where(comment => comment.ticketTable.ticketTitle == id);
            return View(sortedComments.ToList());
        }
        // GET: commentTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            commentTable commentTable = db.commentTables.Find(id);
            if (commentTable == null)
            {
                return HttpNotFound();
            }
            return View(commentTable);
        }

        // GET: commentTables/Create
        public ActionResult Create()
        {
            ViewBag.ticketID = new SelectList(db.ticketTables, "ticketID", "ticketTitle");
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName");
            return View();
        }

        // POST: commentTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentID,commentContents,commentTime,ticketID,userID")] commentTable commentTable)
        {
            if (ModelState.IsValid)
            {
                commentTable.commentTime = DateTime.Now.ToString();
                db.commentTables.Add(commentTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ticketID = new SelectList(db.ticketTables, "ticketID", "ticketTitle", commentTable.ticketID);
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName", commentTable.userID);
            return View(commentTable);
        }

        // GET: commentTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            commentTable commentTable = db.commentTables.Find(id);
            if (commentTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ticketID = new SelectList(db.ticketTables, "ticketID", "ticketTitle", commentTable.ticketID);
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName", commentTable.userID);
            return View(commentTable);
        }

        // POST: commentTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentID,commentContents,commentTime,ticketID,userID")] commentTable commentTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ticketID = new SelectList(db.ticketTables, "ticketID", "ticketTitle", commentTable.ticketID);
            ViewBag.userID = new SelectList(db.userTables, "userID", "userName", commentTable.userID);
            return View(commentTable);
        }

        // GET: commentTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            commentTable commentTable = db.commentTables.Find(id);
            if (commentTable == null)
            {
                return HttpNotFound();
            }
            return View(commentTable);
        }

        // POST: commentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            commentTable commentTable = db.commentTables.Find(id);
            db.commentTables.Remove(commentTable);
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
