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
    public class MasterPackagesController : Controller
    {
       MedhurryContext db = new MedhurryContext();

        // GET: MasterPackages
        public ActionResult Index()
        {
            return View(db.masterPackages.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPackage masterPackage = db.masterPackages.Find(id);
            if (masterPackage == null)
            {
                return HttpNotFound();
            }
            return View(masterPackage);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterPackage cvm)
        {
            MasterPackage cat = new MasterPackage();
            cat.Package = cvm.Package;
            cat.Price = cvm.Price;
            cat.Validity = cvm.Validity;
            cat.Description = cvm.Description;
            cat.isActive = false;
            if (cat.statusex == "yes")
            {
                cat.isActive = true;
            }

            db.masterPackages.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public JsonResult CheckUserName(string Package)
        {
            return Json(!db.masterPackages.Any(X => X.Package== Package), JsonRequestBehavior.AllowGet);
        }
        //PackageID,Package,Price,Validity,Description,isActive


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPackage masterPackage = db.masterPackages.Find(id);
            if (masterPackage == null)
            {
                return HttpNotFound();
            }
            return View(masterPackage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterPackage cvm)
        {
            var cat = db.masterPackages.Where(x => x.PackageID == cvm.PackageID).FirstOrDefault();
            cat.Package = cvm.Package;
            cat.Price = cvm.Price;
            cat.Validity = cvm.Validity;
            cat.Description = cvm.Description;
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPackage masterPackage = db.masterPackages.Find(id);
            if (masterPackage == null)
            {
                return HttpNotFound();
            }
            return View(masterPackage);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterPackage masterPackage = db.masterPackages.Find(id);
            db.masterPackages.Remove(masterPackage);
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
