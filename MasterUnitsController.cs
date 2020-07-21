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
    public class MasterUnitsController : Controller
    {
    MedhurryContext db = new MedhurryContext();

        // GET: MasterUnits
        public ActionResult Index()
        {
            return View(db.masterUnits.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterUnit masterUnit = db.masterUnits.Find(id);
            if (masterUnit == null)
            {
                return HttpNotFound();
            }
            return View(masterUnit);
        }

        // GET: MasterUnits/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterUnit cvm)
        {
            MasterUnit cat = new MasterUnit();
            cat.Unit = cvm.Unit;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.masterUnits.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult CheckUserName(string Unit)
        {
            return Json(!db.masterUnits.Any(X => X.Unit == Unit), JsonRequestBehavior.AllowGet);
        }

        //cat.isActive = false;
        //        if (cvm.statusex == "Yes")
        //        {
        //            cat.isActive = true;
        //        }
        // GET: MasterUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterUnit masterUnit = db.masterUnits.Find(id);
            if (masterUnit == null)
            {
                return HttpNotFound();
            }
            return View(masterUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterUnit cvm)
        {
            var cat = db.masterUnits.Where(x => x.UnitID == cvm.UnitID).FirstOrDefault();
            cat.Unit = cvm.Unit;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }



            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: MasterUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterUnit masterUnit = db.masterUnits.Find(id);
            if (masterUnit == null)
            {
                return HttpNotFound();
            }
            return View(masterUnit);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterUnit masterUnit = db.masterUnits.Find(id);
            db.masterUnits.Remove(masterUnit);
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
