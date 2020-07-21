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
    public class MasterRolesController : Controller
    {
       MedhurryContext db = new MedhurryContext();

        // GET: MasterRoles
        public ActionResult Index()
        {
            return View(db.masterRoles.ToList());
        }

        // GET: MasterRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRoles masterRoles = db.masterRoles.Find(id);
            if (masterRoles == null)
            {
                return HttpNotFound();
            }
            return View(masterRoles);
        }

        // GET: MasterRoles/Create
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MasterRoles masterRoles)
        {
            if (ModelState.IsValid)
            {
                db.masterRoles.Add(masterRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterRoles);
        }

        // GET: MasterRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRoles masterRoles = db.masterRoles.Find(id);
            if (masterRoles == null)
            {
                return HttpNotFound();
            }
            return View(masterRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterRoles masterRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterRoles);
        }

        // GET: MasterRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRoles masterRoles = db.masterRoles.Find(id);
            if (masterRoles == null)
            {
                return HttpNotFound();
            }
            return View(masterRoles);
        }

        // POST: MasterRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterRoles masterRoles = db.masterRoles.Find(id);
            db.masterRoles.Remove(masterRoles);
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
