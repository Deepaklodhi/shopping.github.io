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
    public class POrdersController : Controller
    {
       MedhurryContext db = new MedhurryContext();

     
        public ActionResult Index()
        {
            var pOrders = db.pOrders.Include(p => p.MasterSupplier).Include(p => p.MasterVendor);
            return View(pOrders.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POrders pOrders = db.pOrders.Find(id);
            if (pOrders == null)
            {
                return HttpNotFound();
            }
            return View(pOrders);
        }

      
        public ActionResult Create()
        {
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            var getprodcut = db.pOrders.FirstOrDefault();
            var mspro = new POrders();
            if (getprodcut != null)
            {
                var maxcode = db.pOrders.OrderByDescending(x => x.POrderID).Select(x => x.InvoiceNo).FirstOrDefault();
                if (maxcode != null)
                {
                    mspro.InvoiceNo = maxcode.ToString();
                }
            }
            return View(mspro);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(POrders cvm)
        {
            POrders cat = new POrders();
            cat.SupplierID = cvm.SupplierID;
            cat.OrderDate = cvm.OrderDate;
            //cat.InvoiceNo = cvm.InvoiceNo;
            if (cvm.InvoiceNo == null)
            {
                cat.InvoiceNo = "00001";//cvm.Code;
            }
            else
            {
                int maxcode = Convert.ToInt32(cvm.InvoiceNo);
                maxcode = maxcode + 1;
                //int getfinalcode = maxcode + maxcode;
                cat.InvoiceNo = "0000" + maxcode.ToString();
            }
            cat.TotalNet = cvm.TotalNet;
            cat.VATTotal = cvm.VATTotal;
            cat.TotalDue = cvm.TotalDue;
            cat.DiscountTotal = cvm.DiscountTotal;
            cat.PPDiscountTotal = cvm.PPDiscountTotal;
            cat.DiscountPercent = cvm.DiscountPercent;
            cat.LaborCost = cvm.LaborCost;
            cat.DeliveryDate = cvm.DeliveryDate;
            cat.AdjustmentAmount = cvm.AdjustmentAmount;
            cat.PaidAmount = cvm.PaidAmount;
            cat.GrandPriceTotal = cvm.GrandPriceTotal;
            cat.CreateDate = cvm.CreateDate;
            cat.CreatedBy = cvm.CreatedBy;
            cat.ModifiedDate = cvm.ModifiedDate;
            cat.ModifiedDateBy = cvm.ModifiedDateBy;
            cat.VendorID = cvm.VendorID;
            cat.Status = false;
            if(cvm.Statusex =="Yes")
            {
                cat.Status = true;
            }
            db.pOrders.Add(cat);
            db.SaveChanges();




            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return RedirectToAction("Index");
        }
//"POrderID,SupplierID,OrderDate,InvoiceNo,TotalNet,VATTotal,TotalDue,Status,PPDiscountTotal,DiscountTotal,
//DiscountPercent,LaborCost,DeliveryDate,AdjustmentAmount,
//PaidAmount,GrandPriceTotal,CreateDate,CreatedBy,ModifiedDate,ModifiedDateBy,VendorID"
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POrders pOrders = db.pOrders.Find(id);
            if (pOrders == null)
            {
                return HttpNotFound();
            }
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(pOrders);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(POrders cvm)
        {
            POrders cat = new POrders();
            cat.SupplierID = cvm.SupplierID;
            cat.OrderDate = cvm.OrderDate;
            cat.InvoiceNo = cvm.InvoiceNo;
            cat.TotalNet = cvm.TotalNet;
            cat.VATTotal = cvm.VATTotal;
            cat.TotalDue = cvm.TotalDue;
            cat.DiscountTotal = cvm.DiscountTotal;
            cat.PPDiscountTotal = cvm.PPDiscountTotal;
            cat.DiscountPercent = cvm.DiscountPercent;
            cat.LaborCost = cvm.LaborCost;
            cat.DeliveryDate = cvm.DeliveryDate;
            cat.AdjustmentAmount = cvm.AdjustmentAmount;
            cat.PaidAmount = cvm.PaidAmount;
            cat.GrandPriceTotal = cvm.GrandPriceTotal;
            cat.CreateDate = cvm.CreateDate;
            cat.CreatedBy = cvm.CreatedBy;
            cat.ModifiedDate = cvm.ModifiedDate;
            cat.ModifiedDateBy = cvm.ModifiedDateBy;
            cat.VendorID = cvm.VendorID;
            ViewBag.Supplier1 = new SelectList(db.masterSuppliers, "SupplierID", "Name");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            cat.Status = false;
            if (cvm.Statusex == "Yes")
            {
                cat.Status = true;
            }
         
            db.SaveChanges();
           
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POrders pOrders = db.pOrders.Find(id);
            if (pOrders == null)
            {
                return HttpNotFound();
            }
            return View(pOrders);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POrders pOrders = db.pOrders.Find(id);
            db.pOrders.Remove(pOrders);
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
