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
    public class MasterBlogCategoriesController : Controller
    {
      MedhurryContext db = new MedhurryContext();

   
        public ActionResult Index()
        {
            return View(db.masterBlogCategories.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBlogCategory masterBlogCategory = db.masterBlogCategories.Find(id);
            if (masterBlogCategory == null)
            {
                return HttpNotFound();
            }
            return View(masterBlogCategory);
        }

        // GET: MasterBlogCategories/Create
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterBlogCategory masterBlogCategory)
        {
            if (ModelState.IsValid)
            {
                db.masterBlogCategories.Add(masterBlogCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterBlogCategory);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBlogCategory masterBlogCategory = db.masterBlogCategories.Find(id);
            if (masterBlogCategory == null)
            {
                return HttpNotFound();
            }
            return View(masterBlogCategory);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterBlogCategory masterBlogCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterBlogCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterBlogCategory);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBlogCategory masterBlogCategory = db.masterBlogCategories.Find(id);
            if (masterBlogCategory == null)
            {
                return HttpNotFound();
            }
            return View(masterBlogCategory);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterBlogCategory masterBlogCategory = db.masterBlogCategories.Find(id);
            db.masterBlogCategories.Remove(masterBlogCategory);
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
