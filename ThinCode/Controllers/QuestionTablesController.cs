using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThinCode.Models;
using ThinCode.Models.Tools.SoftTools;

namespace ThinCode.Controllers
{
    [CheckAuthorization]
    public class QuestionTablesController : Controller
    {
        private CodeliteEntities1 db = new CodeliteEntities1();

        // GET: QuestionTables
        public ActionResult Index()
        {
            return View(db.QuestionTables.ToList());
        }

        // GET: QuestionTables/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTable questionTable = db.QuestionTables.Find(id);
            if (questionTable == null)
            {
                return HttpNotFound();
            }
            return View(questionTable);
        }

        // GET: QuestionTables/Create
        public ActionResult Create()
        {
            //for defult date
            QuestionTable questionTable = new QuestionTable();
            questionTable.Date = DateTime.Now.Date;
            return View(questionTable);
        }

        // POST: QuestionTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QusID,Date,Question,Type,Subject,Image,Tag")] QuestionTable questionTable)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTables.Add(questionTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionTable);
        }

        // GET: QuestionTables/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTable questionTable = db.QuestionTables.Find(id);
            if (questionTable == null)
            {
                return HttpNotFound();
            }
            return View(questionTable);
        }

        // POST: QuestionTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QusID,Date,Question,Type,Subject,Image,Tag")] QuestionTable questionTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionTable);
        }

        // GET: QuestionTables/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTable questionTable = db.QuestionTables.Find(id);
            if (questionTable == null)
            {
                return HttpNotFound();
            }
            return View(questionTable);
        }

        // POST: QuestionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            QuestionTable questionTable = db.QuestionTables.Find(id);
            db.QuestionTables.Remove(questionTable);
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
