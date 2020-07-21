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
    public class MasterFAQsController : Controller
    {
       MedhurryContext db = new MedhurryContext();

    
        public ActionResult Index()
        {
            return View(db.masterFAQs.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterFAQ masterFAQ = db.masterFAQs.Find(id);
            if (masterFAQ == null)
            {
                return HttpNotFound();
            }
            return View(masterFAQ);
        }

      
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterFAQ cvm)
        {
            MasterFAQ cat = new MasterFAQ();
            cat.Header = cvm.Header;
            cat.Descriptions = cvm.Descriptions;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.masterFAQs.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");

            
        }

   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterFAQ masterFAQ = db.masterFAQs.Find(id);
            if (masterFAQ == null)
            {
                return HttpNotFound();
            }
            return View(masterFAQ);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterFAQ cvm)
        {
            var cat = db.masterFAQs.Where(x => x.ID == cvm.ID).FirstOrDefault();
            cat.Header = cvm.Header;
            cat.Descriptions = cvm.Descriptions;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.masterFAQs.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterFAQ masterFAQ = db.masterFAQs.Find(id);
            if (masterFAQ == null)
            {
                return HttpNotFound();
            }
            return View(masterFAQ);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterFAQ masterFAQ = db.masterFAQs.Find(id);
            db.masterFAQs.Remove(masterFAQ);
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
