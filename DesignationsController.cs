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
    public class DesignationsController : Controller
    {
       MedhurryContext db = new MedhurryContext();

        
        public ActionResult Index()
        {
            return View(db.designations.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designations designations = db.designations.Find(id);
            if (designations == null)
            {
                return HttpNotFound();
            }
            return View(designations);
        }

 
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Designations designations)
        {
            if (ModelState.IsValid)
            {
                db.designations.Add(designations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designations designations = db.designations.Find(id);
            if (designations == null)
            {
                return HttpNotFound();
            }
            return View(designations);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Designations designations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(designations);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designations designations = db.designations.Find(id);
            if (designations == null)
            {
                return HttpNotFound();
            }
            return View(designations);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Designations designations = db.designations.Find(id);
            db.designations.Remove(designations);
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
