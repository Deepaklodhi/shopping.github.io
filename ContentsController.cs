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
    public class ContentsController : Controller
    {
MedhurryContext db = new MedhurryContext();

     
        public ActionResult Index()
        {
            return View(db.contents.ToList());
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

 
        public ActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content cvm)
        {
            Content cat = new Content();
            cat.Header = cvm.Header;
            cat.Descriptions = cvm.Descriptions;
            cat.Slug = cvm.Slug;
            cat.Meta = cvm.Meta;
            cat.Keyword = cvm.Keyword;
            cat.CreateDate = cvm.CreateDate;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.contents.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     
   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Content cvm)
        {
            var cat = db.contents.Where(x => x.ID == cvm.ID).FirstOrDefault();
            cat.Header = cvm.Header;
            cat.Descriptions = cvm.Descriptions;
            cat.Slug = cvm.Slug;
            cat.Meta = cvm.Meta;
            cat.Keyword = cvm.Keyword;
            cat.CreateDate = cvm.CreateDate;
            if (cvm.statusex == "Yes")
            {
                cat.isActive = true;
            }
            db.contents.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");

           
        }

   
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Content content = db.contents.Find(id);
            db.contents.Remove(content);
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
