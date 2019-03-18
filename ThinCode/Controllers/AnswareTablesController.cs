using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using ThinCode.Models;
using ThinCode.Models.Tools;
using ThinCode.Models.Tools.SoftTools;

namespace ThinCode.Controllers
{
    [CheckAuthorization]
    public class AnswareTablesController : Controller
    {
        private CodeliteEntities1 db = new CodeliteEntities1();
        
        // GET: AnswareTables
        // GET: AnswareTables
        public ActionResult Index()
        {
            var answareTables = db.AnswareTables.Include(a => a.QuestionTable);
            return View(answareTables.ToList());
        }

        // GET: AnswareTables/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswareTable answareTable = db.AnswareTables.Find(id);
            if (answareTable == null)
            {
                return HttpNotFound();
            }
            return View(answareTable);
        }

        [ChildActionOnly]//only internally use Directly link not accessable
        // GET: AnswareTables/Create
        public PartialViewResult Create()
        {
            //for defult date
            AnswareTable answareTable = new AnswareTable();
            answareTable.Date = DateTime.Now.Date;
            return PartialView("Create", answareTable);
        }

        // POST: AnswareTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,QusID,Answer,CorrectAns,Image,Remarks,ProjectFilePath,Date,FullName,Email,ContactNo")] AnswareTable answareTable,HttpPostedFileBase _answareimage)
        {
            //Dynamic Validation
            if (!answareTable.QusID.ISValidObject())
            {
                ModelState.AddModelError("QusID", "Select Valid Question");
            }
            if (ModelState.IsValid)
            {
                if (_answareimage.IsImage(1048576))
                {
                    answareTable.Image = new byte[_answareimage.ContentLength];
                    _answareimage.InputStream .Read(answareTable.Image,0,_answareimage.ContentLength);
                }
                db.AnswareTables.Add(answareTable);
              var i=  db.SaveChanges();
                ViewBag.result = WebMessageBox.Show("","",WebMessageBox._messegeType.info,WebMessageBox._alartTopIndex.Simple);
                return RedirectToAction("Index");
            }

            //ViewBag.QusID = new SelectList(db.QuestionTables, "QusID", "Question", answareTable.QusID);
            return View();
        }

        // GET: AnswareTables/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswareTable answareTable = db.AnswareTables.Find(id);
            if (answareTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.QusID = new SelectList(db.QuestionTables, "QusID", "Question", answareTable.QusID);
            return View(answareTable);
        }

        // POST: AnswareTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QusID,Answer,CorrectAns,Image,Remarks,ProjectFilePath,Date,FullName,Email,ContactNo")] AnswareTable answareTable)
        {
            if (ModelState.IsValid)
            {

                db.Entry(answareTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QusID = new SelectList(db.QuestionTables, "QusID", "Question", answareTable.QusID);
            return View(answareTable);
        }

        // GET: AnswareTables/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswareTable answareTable = db.AnswareTables.Find(id);
            if (answareTable == null)
            {
                return HttpNotFound();
            }
            return View(answareTable);
        }

        // POST: AnswareTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AnswareTable answareTable = db.AnswareTables.Find(id);
            db.AnswareTables.Remove(answareTable);
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
