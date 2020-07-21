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
    public class MasterCouponsController : Controller
    {
        MedhurryContext db = new MedhurryContext();
        
        public ActionResult Index()
        {
            return View(db.masterCoupons.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCoupons masterCoupons = db.masterCoupons.Find(id);
            if (masterCoupons == null)
            {
                return HttpNotFound();
            }
            return View(masterCoupons);
        }

      
        public ActionResult Create()
        {
            var getprodcut = db.masterCoupons.FirstOrDefault();
            var mspro = new MasterCoupons();
            if (getprodcut != null)
            {
                var maxcode = db.masterCoupons.OrderByDescending(x => x.ID).Select(x => x.Code).FirstOrDefault();
                if (maxcode != null)
                {
                    mspro.Code = maxcode.ToString();
                }
            }
            return View(mspro);
        
    }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterCoupons cvm)
        {
            MasterCoupons cat = new MasterCoupons();
            cat.Title = cvm.Title;
            cat.DiscountType = cvm.DiscountType;
            cat.DiscountValue = cvm.DiscountValue;
            cat.TillDate = cvm.TillDate;
           
            if (cvm.Code == null)
            {
                cat.Code = "0001";//cvm.Code;
            }
            else
            {
                int maxcode = Convert.ToInt32(cvm.Code);
                maxcode = maxcode + 1;
                //int getfinalcode = maxcode + maxcode;
                cat.Code = "000" + maxcode.ToString();
            }

            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }

            db.masterCoupons.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       //Title,Code,DiscountType,DiscountValue,TillDate,isActive

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCoupons masterCoupons = db.masterCoupons.Find(id);
            if (masterCoupons == null)
            {
                return HttpNotFound();
            }
            return View(masterCoupons);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterCoupons cvm)
        {
            var cat = db.masterCoupons.Where(x => x.ID == cvm.ID).FirstOrDefault();
            cat.Title = cvm.Title;
            cat.DiscountType = cvm.DiscountType;
            cat.DiscountValue = cvm.DiscountValue;
            cat.TillDate = cvm.TillDate;
            cat.isActive = false;
            if (cat.statusex == "Yes")
            {
                cat.isActive = true;
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
            MasterCoupons masterCoupons = db.masterCoupons.Find(id);
            if (masterCoupons == null)
            {
                return HttpNotFound();
            }
            return View(masterCoupons);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterCoupons masterCoupons = db.masterCoupons.Find(id);
            db.masterCoupons.Remove(masterCoupons);
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
