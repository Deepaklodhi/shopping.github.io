using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medhurry.Models;

namespace Medhurry.Controllers
{
    public class MasterBlogsController : Controller
    {
    MedhurryContext db = new MedhurryContext();

   
        public ActionResult Index()
        {
            return View(db.masterBlogs.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBlogs masterBlogs = db.masterBlogs.Find(id);
            if (masterBlogs == null)
            {
                return HttpNotFound();
            }
            return View(masterBlogs);
        }

       
        public ActionResult Create()
        {

            ViewBag.MasterCategory = new SelectList(db.masterBlogCategories, "CategoryID", "Category");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterBlogs cvm, HttpPostedFileBase imgfile)
        {

            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterBlogs cat = new MasterBlogs();
               
                cat.Photo = path;
                cat.cat_Status = 1;
                cat.Header = cvm.Header;
                cat.Summary = cvm.Summary;
                cat.WrittenBy = cvm.WrittenBy;
                cat.Descriptions = cvm.Descriptions;
                cat.meta = cvm.meta;
                cat.keyword = cvm.keyword;
                cat.slug = cvm.slug;
                cat.CreateDate = cvm.CreateDate;
                cat.CategoryID = cvm.CategoryID;
               
                cat.isActive = false;
                if (cvm.Statusex == "Yes")
                {
                    cat.isActive = true;
                }

                db.masterBlogs.Add(cat);
                db.SaveChanges();
                ViewBag.MasterCategory = new SelectList(db.masterBlogCategories, "CategoryID", "Category");
                return RedirectToAction("Index");

            }
            return View();
        }
        //"ID,CategoryId,Header,Summary,WrittenBy,Descriptions,meta,keyword,slug,Photo,isActive,CreateDate"
        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBlogs masterBlogs = db.masterBlogs.Find(id);
            Session["FileName"] = masterBlogs.Photo;
            if (masterBlogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCategory = new SelectList(db.masterBlogCategories, "CategoryID", "Category");
            return View(masterBlogs);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterBlogs cvm, HttpPostedFileBase imgfile)
        {

            string path = "";
            var cat = db.masterBlogs.Where(x => x.ID == cvm.ID).FirstOrDefault();
            if (cat.Photo == Session["FileName"].ToString() && imgfile == null)
            {
                path = cat.Photo;
            }
            else
            {
                path = uploadimgfile1(imgfile);

            }
            if (path.Equals("-1") && cat.Photo != Session["FileName"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            { 
                cat.Photo = path;
                cat.cat_Status = 1;
                cat.Header = cvm.Header;
                cat.Summary = cvm.Summary;
                cat.WrittenBy = cvm.WrittenBy;
                cat.Descriptions = cvm.Descriptions;
                cat.meta = cvm.meta;
                cat.keyword = cvm.keyword;
                cat.slug = cvm.slug;
                cat.CreateDate = cvm.CreateDate;
                cat.CategoryID = cvm.CategoryID;

                cat.isActive = false;
                if (cvm.Statusex == "Yes")
                {
                    cat.isActive = true;
                }

                db.masterBlogs.Add(cat);
                db.SaveChanges();
                ViewBag.MasterCategory = new SelectList(db.masterBlogCategories, "CategoryID", "Category");
                return RedirectToAction("Index");
            }
                return View();
        }

        public string uploadimgfile1(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBlogs masterBlogs = db.masterBlogs.Find(id);
            if (masterBlogs == null)
            {
                return HttpNotFound();
            }
            return View(masterBlogs);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterBlogs masterBlogs = db.masterBlogs.Find(id);
            db.masterBlogs.Remove(masterBlogs);
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
