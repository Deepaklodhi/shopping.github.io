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
    public class MasterProductsImagesController : Controller
    {
        MedhurryContext db = new MedhurryContext();

        // GET: MasterProductsImages
        public ActionResult Index()
        {
            var masterProductsImages = db.masterProductsImages.Include(m => m.MasterProduct);
            return View(masterProductsImages.ToList());
        }

        // GET: MasterProductsImages/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductsImage masterProductsImage = db.masterProductsImages.Find(id);
            if (masterProductsImage == null)
            {
                return HttpNotFound();
            }
            return View(masterProductsImage);
        }

        // GET: MasterProductsImages/Create
        public ActionResult Create()
        {
            ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterProductsImage masterProductsImage)
        {
            try
            {


                //Ensure model state is valid 
                if (ModelState.IsValid)
                {
                    //iterating through multiple file collection   
                    //Checking file is available to save.  
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var cat = Request.Files[i];

                        if (cat != null && cat.ContentLength > 0)
                        {
                            // var fileName = Path.GetFileName(file.FileName);
                            string path1 = uploadimgfile(cat);
                            MasterProductsImage fileDetail = new MasterProductsImage()
                            {
                                ProductImage = path1,
                            };
                            fileDetail.ProductID = masterProductsImage.ProductID;
                            db.masterProductsImages.Add(fileDetail);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
          ViewBag.SportType = new SelectList(db.masterProducts, "ProductID", "ProductName");
            return RedirectToAction("Index");
        }

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

        // GET: MasterProductsImages/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductsImage masterProductsImage = db.masterProductsImages.Find(id);
            if (masterProductsImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.masterProducts, "ProductID", "Code", masterProductsImage.ProductID);
            return View(masterProductsImage);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductImageId,ProductID,ProductImage")] MasterProductsImage masterProductsImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterProductsImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.masterProducts, "ProductID", "Code", masterProductsImage.ProductID);
            return View(masterProductsImage);
        }

        // GET: MasterProductsImages/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProductsImage masterProductsImage = db.masterProductsImages.Find(id);
            if (masterProductsImage == null)
            {
                return HttpNotFound();
            }
            return View(masterProductsImage);
        }

        // POST: MasterProductsImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MasterProductsImage masterProductsImage = db.masterProductsImages.Find(id);
            db.masterProductsImages.Remove(masterProductsImage);
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
