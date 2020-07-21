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
    public class MasterTaxesController : Controller
    {
    MedhurryContext db = new MedhurryContext();

      
        public ActionResult Index()
        {
            return View(db.masterTaxes.ToList());
        }

     
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterTax masterTax = db.masterTaxes.Find(id);
            if (masterTax == null)
            {
                return HttpNotFound();
            }
            return View(masterTax);
        }

      
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterTax cvm)
        {


            MasterTax cat = new MasterTax();
            cat.Tax = cvm.Tax;
            cat.TaxValue = cvm.TaxValue;
            cat.isActive = false;
            if (cvm.statusez == "Yes")
            {
                cat.isActive = true;
            }
            db.masterTaxes.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterTax masterTax = db.masterTaxes.Find(id);
            if (masterTax == null)
            {
                return HttpNotFound();
            }
            return View(masterTax);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MasterTax cvm)
        {
            var cat = db.masterTaxes.Where(x => x.TaxID == cvm.TaxID).FirstOrDefault();
            cat.Tax = cvm.Tax;
            cat.TaxValue = cvm.TaxValue;
            cat.isActive = false;
            if (cvm.statusez == "Yes")
            {
                cat.isActive = true;
            }
            db.masterTaxes.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterTax masterTax = db.masterTaxes.Find(id);
            if (masterTax == null)
            {
                return HttpNotFound();
            }
            return View(masterTax);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterTax masterTax = db.masterTaxes.Find(id);
            db.masterTaxes.Remove(masterTax);
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
