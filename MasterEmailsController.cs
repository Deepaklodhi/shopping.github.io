using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medhurry.Models;

namespace Medhurry.Controllers
{
    public class MasterEmailsController : Controller
    {
     MedhurryContext db = new MedhurryContext();

       
        public ActionResult Index()
        {
            return View(db.masterEmails.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmail masterEmail = db.masterEmails.Find(id);
            if (masterEmail == null)
            {
                return HttpNotFound();
            }
            return View(masterEmail);
        }

       
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MasterEmail masterEmail)
        {
            if (ModelState.IsValid)
            {
                db.masterEmails.Add(masterEmail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterEmail);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmail masterEmail = db.masterEmails.Find(id);
            if (masterEmail == null)
            {
                return HttpNotFound();
            }
            return View(masterEmail);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterEmail masterEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterEmail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterEmail);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmail masterEmail = db.masterEmails.Find(id);
            if (masterEmail == null)
            {
                return HttpNotFound();
            }
            return View(masterEmail);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterEmail masterEmail = db.masterEmails.Find(id);
            db.masterEmails.Remove(masterEmail);
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
