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
    public class BankTransactionsController : Controller
    {
       MedhurryContext db = new MedhurryContext();

   
        public ActionResult Index()
        {
            var bankTransactions = db.bankTransactions.Include(b => b.Banks);
            return View(bankTransactions.ToList());
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransaction bankTransaction = db.bankTransactions.Find(id);
            if (bankTransaction == null)
            {
                return HttpNotFound();
            }
            return View(bankTransaction);
        }

        // GET: BankTransactions/Create
        public ActionResult Create()
        {
            ViewBag.Bank1 = new SelectList(db.banks, "BankID", "BankName");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankTransaction bankTransaction)
        {
            if (ModelState.IsValid)
            {
                db.bankTransactions.Add(bankTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bank1 = new SelectList(db.banks, "BankID", "BankName", bankTransaction.BankID);
            //ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", bankTransaction.VendorID);
            return View(bankTransaction);
        }

   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransaction bankTransaction = db.bankTransactions.Find(id);
            if (bankTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bank1= new SelectList(db.banks, "BankID", "BankName", bankTransaction.BankID);
            //ViewBag.Vendor1= new SelectList(db.masterVendors, "VendorID", "Name", bankTransaction.VendorID);
            return View(bankTransaction);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankTransaction bankTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bank1 = new SelectList(db.banks, "BankID", "BankName", bankTransaction.BankID);
            //ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", bankTransaction.VendorID);
            return View(bankTransaction);
        }

        // GET: BankTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransaction bankTransaction = db.bankTransactions.Find(id);
            if (bankTransaction == null)
            {
                return HttpNotFound();
            }
            return View(bankTransaction);
        }

        // POST: BankTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankTransaction bankTransaction = db.bankTransactions.Find(id);
            db.bankTransactions.Remove(bankTransaction);
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
