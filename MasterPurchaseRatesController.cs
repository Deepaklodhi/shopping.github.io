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
    public class MasterPurchaseRatesController : Controller
    {
      MedhurryContext db = new MedhurryContext();

        // GET: MasterPurchaseRates
        public ActionResult Index()
        {
            var masterPurchaseRates = db.masterPurchaseRates.Include(m => m.MasterProduct).Include(m => m.MasterSize).Include(m => m.MasterSupplier).Include(m => m.MasterUnit).Include(m => m.MasterVendor);
            return View(masterPurchaseRates.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPurchaseRates masterPurchaseRates = db.masterPurchaseRates.Find(id);
            if (masterPurchaseRates == null)
            {
                return HttpNotFound();
            }
            return View(masterPurchaseRates);
        }

        // GET: MasterPurchaseRates/Create
        public ActionResult Create()
        {
            ViewBag.Product1 = new SelectList(db.masterProducts, "ProductID", "ProductName");
            ViewBag.Size1 = new SelectList(db.masterSizes, "SizeID", "Size");
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Unit1 = new SelectList(db.masterUnits, "UnitID", "Unit");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MasterPurchaseRates masterPurchaseRates)
        {
            if (ModelState.IsValid)
            {
                db.masterPurchaseRates.Add(masterPurchaseRates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product1 = new SelectList(db.masterProducts, "ProductID", "ProductName");
            ViewBag.Size1 = new SelectList(db.masterSizes, "SizeID", "Size");
            ViewBag.Supplier1= new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Unit1 = new SelectList(db.masterUnits, "UnitID", "Unit");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(masterPurchaseRates);
        }

        // GET: MasterPurchaseRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPurchaseRates masterPurchaseRates = db.masterPurchaseRates.Find(id);
            if (masterPurchaseRates == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product1 = new SelectList(db.masterProducts, "ProductID", "ProductName");
            ViewBag.Size1 = new SelectList(db.masterSizes, "SizeID", "Size");
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Unit1 = new SelectList(db.masterUnits, "UnitID", "Unit");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(masterPurchaseRates);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MasterPurchaseRates masterPurchaseRates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterPurchaseRates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product1 = new SelectList(db.masterProducts, "ProductID", "ProductName");
            ViewBag.Size1 = new SelectList(db.masterSizes, "SizeID", "Size");
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Unit1 = new SelectList(db.masterUnits, "UnitID", "Unit");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(masterPurchaseRates);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPurchaseRates masterPurchaseRates = db.masterPurchaseRates.Find(id);
            if (masterPurchaseRates == null)
            {
                return HttpNotFound();
            }
            return View(masterPurchaseRates);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterPurchaseRates masterPurchaseRates = db.masterPurchaseRates.Find(id);
            db.masterPurchaseRates.Remove(masterPurchaseRates);
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
