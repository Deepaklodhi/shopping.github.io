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
    public class PReturnsController : Controller
    {
 MedhurryContext db = new MedhurryContext();

      
        public ActionResult Index()
        {
            var pReturns = db.pReturns.Include(p => p.MasterSupplier).Include(p => p.MasterVendor);
            return View(pReturns.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PReturns pReturns = db.pReturns.Find(id);
            if (pReturns == null)
            {
                return HttpNotFound();
            }
            return View(pReturns);
        }

    
        public ActionResult Create()
        {
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            var getprodcut = db.pReturns.FirstOrDefault();
            var mspro = new PReturns();
            if (getprodcut != null)
            {
                var maxcode = db.pReturns.OrderByDescending(x => x.PReturnID).Select(x => x.InvoiceNo).FirstOrDefault();
                if (maxcode != null)
                {
                    mspro.InvoiceNo = maxcode.ToString();
                }
            }
            return View(mspro);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PReturns cvm)
        {
            PReturns cat = new PReturns();
            //cat.InvoiceNo = cvm.InvoiceNo;
            if (cvm.InvoiceNo == null)
            {
                cat.InvoiceNo = "00002";//cvm.Code;
            }
            else
            {
                int maxcode = Convert.ToInt32(cvm.InvoiceNo);
                maxcode = maxcode + 1;
                //int getfinalcode = maxcode + maxcode;
                cat.InvoiceNo = "0000" + maxcode.ToString();
            }
            cat.InvoiceDate = cvm.InvoiceDate;
        
            cat.VendorID = cvm.VendorID;
            cat.PPDiscountTotal = cvm.PPDiscountTotal;
            cat.DiscountPercent = cvm.DiscountPercent;
            cat.DiscountTotal = cvm.DiscountTotal;
            cat.TotalNet = cvm.TotalNet;
            cat.BackAmount = cvm.BackAmount;
            cat.Remarks = cvm.Remarks;
            cat.TotalDue = cvm.TotalDue;
            cat.VATTotal = cvm.VATTotal;
            cat.AdjustmentAmount = cvm.AdjustmentAmount;
            cat.PaidAmount = cvm.PaidAmount;
            cat.GrandPriceTotal = cvm.GrandPriceTotal;
            cat.CreateDate = cvm.CreateDate;
            cat.CreatedBy = cvm.CreatedBy;
            cat.ModifiedDate = cvm.ModifiedDate;
            cat.ModifiedDateBy = cvm.ModifiedDateBy;
            cat.VendorID = cvm.VendorID;
            cat.SupplierID = cvm.SupplierID;
            cat.Status = false;
            if(cvm.Statusex =="Yes")
            {
                cat.Status = true;
            }
            db.pReturns.Add(cat);
            db.SaveChanges();
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return RedirectToAction("Index");

          
         
        }
        //"PReturnID,InvoiceNo,InvoiceDate,SupplierID,PPDiscountTotal,DiscountPercent,DiscountTotal,TotalNet
        //,BackAmount,Remarks,TotalDue,Status,VATTotal,AdjustmentAmount,PaidAmount,GrandPriceTotal,
        //    CreateDate,CreatedBy,ModifiedDate,ModifiedDateBy,VendorID"
        // GET: PReturns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PReturns pReturns = db.pReturns.Find(id);
            if (pReturns == null)
            {
                return HttpNotFound();
            }
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(pReturns);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PReturns cvm)
        {
            var cat = db.pReturns.Where(x => x.PReturnID == cvm.PReturnID).FirstOrDefault();
            cat.InvoiceNo = cvm.InvoiceNo;
            cat.InvoiceDate = cvm.InvoiceDate;

            cat.VendorID = cvm.VendorID;
            cat.PPDiscountTotal = cvm.PPDiscountTotal;
            cat.DiscountPercent = cvm.DiscountPercent;
            cat.DiscountTotal = cvm.DiscountTotal;
            cat.TotalNet = cvm.TotalNet;
            cat.BackAmount = cvm.BackAmount;
            cat.Remarks = cvm.Remarks;
            cat.TotalDue = cvm.TotalDue;
            cat.VATTotal = cvm.VATTotal;
            cat.AdjustmentAmount = cvm.AdjustmentAmount;
            cat.PaidAmount = cvm.PaidAmount;
            cat.GrandPriceTotal = cvm.GrandPriceTotal;
            cat.CreateDate = cvm.CreateDate;
            cat.CreatedBy = cvm.CreatedBy;
            cat.ModifiedDate = cvm.ModifiedDate;
            cat.ModifiedDateBy = cvm.ModifiedDateBy;
            cat.VendorID = cvm.VendorID;
            cat.SupplierID = cvm.SupplierID;
            cat.Status = false;
            if (cvm.Statusex == "Yes")
            {
                cat.Status = true;
            }
            db.pReturns.Add(cat);
            db.SaveChanges();
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return RedirectToAction("Index");


        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PReturns pReturns = db.pReturns.Find(id);
            if (pReturns == null)
            {
                return HttpNotFound();
            }
            return View(pReturns);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PReturns pReturns = db.pReturns.Find(id);
            db.pReturns.Remove(pReturns);
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
