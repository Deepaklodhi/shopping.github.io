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
    public class UsersController : Controller
    {
    MedhurryContext db = new MedhurryContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.users.Include(u => u.MasterRole).Include(u => u.MasterVendor);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Role1 = new SelectList(db.masterRoles, "RoleID", "Roles");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users cvm)
        {
            Users cat = new Users();
            cat.UserName = cvm.UserName;
            cat.UserPassword = cvm.UserPassword;
            cat.ContactNo = cvm.ContactNo;
            cat.EmailAddress = cvm.EmailAddress;
            cat.UserType = cvm.UserType;
            cat.CreateDate = cvm.CreateDate;
            cat.CreatedBy = cvm.CreatedBy;
            cat.Status = false;
            if (cvm.statusex == "Yes")
            {
                cat.Status = true;
            }
            cat.VendorID = cvm.VendorID;
            cat.RoleId = cvm.RoleId;
            db.users.Add(cat);
            db.SaveChanges();
            ViewBag.Role1 = new SelectList(db.masterRoles, "RoleID", "Roles");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return RedirectToAction("Index");

        
            
        }
        //UserID,UserName,UserPassword,ContactNo,EmailAddress,UserType,CreateDate,CreatedBy,Status,VendorID,RoleId
        public JsonResult CheckUserName(string UserName)
        {
            return Json(!db.users.Any(X => X.UserName == UserName), JsonRequestBehavior.AllowGet);
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role1 = new SelectList(db.masterRoles, "RoleID", "Roles");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(users);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users cvm)
        {
            var cat = db.users.Where(x => x.UserID == cvm.UserID).FirstOrDefault();
            cat.UserName = cvm.UserName;
            cat.UserPassword = cvm.UserPassword;
            cat.ContactNo = cvm.ContactNo;
            cat.EmailAddress = cvm.EmailAddress;
            cat.UserType = cvm.UserType;
            cat.CreateDate = cvm.CreateDate;
            cat.CreatedBy = cvm.CreatedBy;
            cat.Status = false;
            if (cvm.statusex == "Yes")
            {
                cat.Status = true;
            }
            cat.VendorID = cvm.VendorID;
            cat.RoleId = cvm.RoleId;
            db.users.Add(cat);
            db.SaveChanges();
            ViewBag.Role1 = new SelectList(db.masterRoles, "RoleID", "Roles");
            ViewBag.Vendor1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.users.Find(id);
            db.users.Remove(users);
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
