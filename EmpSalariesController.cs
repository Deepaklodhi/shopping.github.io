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
    public class EmpSalariesController : Controller
    {
    MedhurryContext db = new MedhurryContext();

        // GET: EmpSalaries
        public ActionResult Index()
        {
            var empSalaries = db.empSalaries.Include(e => e.Employees).Include(e => e.MasterVendor);
            return View(empSalaries.ToList());
        }

        // GET: EmpSalaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpSalaries empSalaries = db.empSalaries.Find(id);
            if (empSalaries == null)
            {
                return HttpNotFound();
            }
            return View(empSalaries);
        }

        // GET: EmpSalaries/Create
        public ActionResult Create()
        {
            ViewBag.Employee1 = new SelectList(db.employees, "EmployeeID", "FirstName");
            ViewBag.Vendor1= new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpSalaries empSalaries)
        {
            if (ModelState.IsValid)
            {
                db.empSalaries.Add(empSalaries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee1 = new SelectList(db.employees, "EmployeeID", "FirstName", empSalaries.EmployeeID);
            ViewBag.Vendor1= new SelectList(db.masterVendors, "VendorID", "Name", empSalaries.VendorID);
            return View(empSalaries);
        }

        // GET: EmpSalaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpSalaries empSalaries = db.empSalaries.Find(id);
            if (empSalaries == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee1 = new SelectList(db.employees, "EmployeeID", "FirstName", empSalaries.EmployeeID);
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", empSalaries.VendorID);
            return View(empSalaries);
        }

   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpSalaries empSalaries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empSalaries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee1 = new SelectList(db.employees, "EmployeeID", "FirstName", empSalaries.EmployeeID);
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name", empSalaries.VendorID);
            return View(empSalaries);
        }

        // GET: EmpSalaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpSalaries empSalaries = db.empSalaries.Find(id);
            if (empSalaries == null)
            {
                return HttpNotFound();
            }
            return View(empSalaries);
        }

        // POST: EmpSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpSalaries empSalaries = db.empSalaries.Find(id);
            db.empSalaries.Remove(empSalaries);
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
