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
    public class QuestionTypesController : Controller
    {
        private CodeliteEntities1 db = new CodeliteEntities1();

        // GET: QuestionTypes
        public ActionResult Index()
        {
            return View(db.QuestionTypes.ToList());
        }

        // GET: QuestionTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionType questionType = db.QuestionTypes.Find(id);
            if (questionType == null)
            {
                return HttpNotFound();
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: QuestionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Remarks")] QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTypes.Add(questionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionType);
        }

        // GET: QuestionTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionType questionType = db.QuestionTypes.Find(id);
            if (questionType == null)
            {
                return HttpNotFound();
            }
            return View(questionType);
        }

        // POST: QuestionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Remarks")] QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionType questionType = db.QuestionTypes.Find(id);
            if (questionType == null)
            {
                return HttpNotFound();
            }
            return View(questionType);
        }

        // POST: QuestionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            QuestionType questionType = db.QuestionTypes.Find(id);
            db.QuestionTypes.Remove(questionType);
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
