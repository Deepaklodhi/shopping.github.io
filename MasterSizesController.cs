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
    public class MasterSizesController : Controller
    {
     MedhurryContext db = new MedhurryContext();

       
        public ActionResult Index()
        {
            return View(db.masterSizes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSize masterSize = db.masterSizes.Find(id);
            if (masterSize == null)
            {
                return HttpNotFound();
            }
            return View(masterSize);
        }

   
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSize cvm)
        {

            MasterSize cat = new MasterSize();
            cat.Size = cvm.Size;
            cat.isActive = false;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.masterSizes.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public JsonResult CheckUserName(string Size)
        {
            return Json(!db.masterSizes.Any(X => X.Size == Size), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSize masterSize = db.masterSizes.Find(id);
            if (masterSize == null)
            {
                return HttpNotFound();
            }
            return View(masterSize);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterSize cvm)
        {
            var cat = db.masterSizes.Where(x => x.SizeID == cvm.SizeID).FirstOrDefault();
            cat.Size = cvm.Size;
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
            MasterSize masterSize = db.masterSizes.Find(id);
            if (masterSize == null)
            {
                return HttpNotFound();
            }
            return View(masterSize);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterSize masterSize = db.masterSizes.Find(id);
            db.masterSizes.Remove(masterSize);
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
