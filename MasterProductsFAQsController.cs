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
    public class MasterProductsFAQsController : Controller
    {
         MedhurryContext db = new MedhurryContext();

        // GET: MasterProductsFAQs
        public ActionResult Index()
        {
            var masterProductsFAQs = db.masterProductsFAQs.Include(m => m.MasterProduct);
            return View(masterProductsFAQs.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductsFAQ masterProductsFAQ = db.masterProductsFAQs.Find(id);
            if (masterProductsFAQ == null)
            {
                return HttpNotFound();
            }
            return View(masterProductsFAQ);
        }


        public ActionResult Create()
        {
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterProductsFAQ cvm)
        {
            MasterProductsFAQ cat = new MasterProductsFAQ();

            cat.Answer = cvm.Answer;
            cat.Question = cvm.Question;
            cat.ExpertName = cvm.ExpertName;
            cat.ProductID = cvm.ProductID;
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            db.masterProductsFAQs.Add(cat);
            db.SaveChanges();
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            return RedirectToAction("Index");

        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MasterProductsFAQ masterProductsFAQ = db.masterProductsFAQs.Find(id);
            if (masterProductsFAQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            return View(masterProductsFAQ);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterProductsFAQ cvm)
        {
            var cat = db.masterProductsFAQs.Where(x => x.PFaqID == cvm.PFaqID).FirstOrDefault();
            cat.Answer = cvm.Answer;
            cat.Question = cvm.Question;
            cat.ExpertName = cvm.ExpertName;
            cat.ProductID = cvm.ProductID;
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");

            db.SaveChanges();
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            return RedirectToAction("Index");

        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductsFAQ masterProductsFAQ = db.masterProductsFAQs.Find(id);
            if (masterProductsFAQ == null)
            {
                return HttpNotFound();
            }
            return View(masterProductsFAQ);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterProductsFAQ masterProductsFAQ = db.masterProductsFAQs.Find(id);
            db.masterProductsFAQs.Remove(masterProductsFAQ);
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
