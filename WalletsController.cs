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
    public class WalletsController : Controller
    {
      MedhurryContext db = new MedhurryContext();

 
        public ActionResult Index()
        {
            var wallets = db.wallets.Include(w => w.MasterCustomers);
            return View(wallets.ToList());
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wallet wallet = db.wallets.Find(id);
            if (wallet == null)
            {
                return HttpNotFound();
            }
            return View(wallet);
        }

     
        public ActionResult Create()
        {
            ViewBag.Customer1 = new SelectList(db.masterCustomers, "CustomerID", "FirstName");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Wallet cvm)
        {
            Wallet cat = new Wallet();
            cat.Amount = cvm.Amount;
            cat.TransactionType = cvm.TransactionType;
            cat.Remarks = cvm.Remarks;
            cat.isActive = false;
                if (cvm.statusex =="Yes")
            {
                cat.isActive = true;
            }
            cat.CreateDate = cvm.CreateDate;
            ViewBag.Customer1 = new SelectList(db.masterCustomers, "CustomerID", "FirstName");
            db.wallets.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //WalletId,CustomerId,Amount,TransactionType,Remarks,isActive,CreateDate
     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wallet wallet = db.wallets.Find(id);
            if (wallet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer1 = new SelectList(db.masterCustomers, "CustomerID", "FirstName");
            return View(wallet);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Wallet cvm)
        {
            var cat = db.wallets.Where(x => x.WalletId == cvm.WalletId).FirstOrDefault();
            cat.Amount = cvm.Amount;
            cat.TransactionType = cvm.TransactionType;
            cat.Remarks = cvm.Remarks;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            cat.CreateDate = cvm.CreateDate;
            ViewBag.Customer1 = new SelectList(db.masterCustomers, "CustomerID", "FirstName");
         
            db.SaveChanges();
            return RedirectToAction("Index"); ;
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wallet wallet = db.wallets.Find(id);
            if (wallet == null)
            {
                return HttpNotFound();
            }
            return View(wallet);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wallet wallet = db.wallets.Find(id);
            db.wallets.Remove(wallet);
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
