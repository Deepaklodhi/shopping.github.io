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
    public class BanksController : Controller
    {
      MedhurryContext db = new MedhurryContext();

        // GET: Banks
        public ActionResult Index()
        {
            var banks = db.banks.Include(b => b.MasterVendor);
            return View(banks.ToList());
        }

  
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banks banks = db.banks.Find(id);
            if (banks == null)
            {
                return HttpNotFound();
            }
            return View(banks);
        }

   
        public ActionResult Create()
        {
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banks banks)
        {
            if (ModelState.IsValid)
            {
                db.banks.Add(banks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", banks.VendorID);
            return View(banks);
        }

        // GET: Banks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banks banks = db.banks.Find(id);
            if (banks == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", banks.VendorID);
            return View(banks);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Banks banks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", banks.VendorID);
            return View(banks);
        }

  
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banks banks = db.banks.Find(id);
            if (banks == null)
            {
                return HttpNotFound();
            }
            return View(banks);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banks banks = db.banks.Find(id);
            db.banks.Remove(banks);
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
