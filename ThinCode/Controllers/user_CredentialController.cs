using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThinCode.Models;
using ThinCode.Models.Tools;
using ThinCode.Models.Tools.SoftTools;

namespace ThinCode.Controllers
{
   [CheckAuthorization]
    public class user_CredentialController : Controller
    {
        private CodeliteEntities1 db = new CodeliteEntities1();

        // GET: user_Credential
        public ActionResult Index()
        {
            return View(db.user_Credential.ToList());
        }

        // GET: user_Credential/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_Credential user_Credential = db.user_Credential.Find(id);
            if (user_Credential == null)
            {
                return HttpNotFound();
            }
            return View(user_Credential);
        }

        // GET: user_Credential/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_Credential/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,user_Name,user_passWord,user_FirstName,User_LastName,user_email,user_registered_Date,Activity,display_name,User_Image,User_Address,User_Country,User_PostalCode,User_Abouts")] user_Credential user_Credential)
        {
            if (ModelState.IsValid)
            {
                //Generate Hash
                byte[] _solt = GenerateSaltKey.GenerateSALT(GenerateSaltKey._SaltLengthLimit);
                string _hashCode = GenerateHASHValue.GenerateHASH(user_Credential.user_Name, user_Credential.user_passWord, _solt);
              
                user_Credential.HASH= _hashCode;
                user_Credential.SALT = _solt;

                ///Fill
                db.user_Credential.Add(user_Credential);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Credential);
        }

        // GET: user_Credential/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_Credential user_Credential = db.user_Credential.Find(id);
            if (user_Credential == null)
            {
                return HttpNotFound();
            }
            return View(user_Credential);
        }

        // POST: user_Credential/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,user_Name,user_passWord,user_FirstName,User_LastName,user_email,user_registered_Date,Activity,display_name,User_Image,User_Address,User_Country,User_PostalCode,User_Abouts")] user_Credential user_Credential)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Credential).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Credential);
        }

        // GET: user_Credential/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_Credential user_Credential = db.user_Credential.Find(id);
            if (user_Credential == null)
            {
                return HttpNotFound();
            }
            return View(user_Credential);
        }

        // POST: user_Credential/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            user_Credential user_Credential = db.user_Credential.Find(id);
            db.user_Credential.Remove(user_Credential);
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
