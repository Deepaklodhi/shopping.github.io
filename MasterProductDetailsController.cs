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
    public class MasterProductDetailsController : Controller
    {
         MedhurryContext db = new MedhurryContext();

        public ActionResult Index()
        {
            var masterProductDetails = db.masterProductDetails.Include(m => m.MasterProduct);
            return View(masterProductDetails.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductDetail masterProductDetail = db.masterProductDetails.Find(id);
            if (masterProductDetail == null)
            {
                return HttpNotFound();
            }
            return View(masterProductDetail);
        }


        public ActionResult Create()
        {
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterProductDetail cvm)
        {
            MasterProductDetail cat = new MasterProductDetail();
            cat.ProductID = cvm.ProductID;
            cat.Description = cvm.Description;
            cat.Use = cvm.Use;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            cat.SideEffect = cvm.SideEffect;
            cat.HowToUse = cvm.HowToUse;
            cat.HowItWorks = cvm.HowItWorks;
            cat.MoreInfo = cvm.MoreInfo;
        
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            db.masterProductDetails.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");

        }





        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductDetail masterProductDetail = db.masterProductDetails.Find(id);

            if (masterProductDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            return View(masterProductDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterProductDetail cvm)
        {
            var cat = db.masterProductDetails.Where(x => x.PDetailsID == cvm.PDetailsID).FirstOrDefault();
            cat.ProductID = cvm.ProductID;
            cat.Description = cvm.Description;
            cat.Use = cvm.Use;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            cat.SideEffect = cvm.SideEffect;
            cat.HowToUse = cvm.HowToUse;
            cat.HowItWorks = cvm.HowItWorks;
            cat.MoreInfo = cvm.MoreInfo;

            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            
        

           

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: MasterProductDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductDetail masterProductDetail = db.masterProductDetails.Find(id);
            if (masterProductDetail == null)
            {
                return HttpNotFound();
            }
            return View(masterProductDetail);
        }

        // POST: MasterProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterProductDetail masterProductDetail = db.masterProductDetails.Find(id);
            db.masterProductDetails.Remove(masterProductDetail);
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
