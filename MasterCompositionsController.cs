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
    public class MasterCompositionsController : Controller
    {
         MedhurryContext db = new MedhurryContext();

        // GET: MasterCompositions
        public ActionResult Index()
        {
            return View(db.masterCompositions.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterComposition masterComposition = db.masterCompositions.Find(id);
            if (masterComposition == null)
            {
                return HttpNotFound();
            }
            return View(masterComposition);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterComposition cvm)
        {
            MasterComposition cat = new MasterComposition();
            cat.Composition = cvm.Composition;


            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.masterCompositions.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult CheckUserName(string Composition)
        {
            return Json(!db.masterCompositions.Any(X => X.Composition == Composition), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {

            MasterComposition masterComposition = db.masterCompositions.Find(id);

            return View(masterComposition);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterComposition cvm)
        {


            var cat = db.masterCompositions.Where(x => x.CompositionID == cvm.CompositionID).FirstOrDefault();

            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }

            db.SaveChanges();
            return RedirectToAction("Index");

        }


        public ActionResult Delete(int? id)
        {

            MasterComposition masterComposition = db.masterCompositions.Find(id);

            return View(masterComposition);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterComposition masterComposition = db.masterCompositions.Find(id);
            db.masterCompositions.Remove(masterComposition);
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
