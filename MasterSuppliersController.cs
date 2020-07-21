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
    public class MasterSuppliersController : Controller
    {
    MedhurryContext db = new MedhurryContext();

       
        public ActionResult Index()
        {
            var masterSuppliers = db.masterSuppliers.Include(m => m.MasterVendor);
            return View(masterSuppliers.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSuppliers masterSuppliers = db.masterSuppliers.Find(id);
            if (masterSuppliers == null)
            {
                return HttpNotFound();
            }
            return View(masterSuppliers);
        }

     
        public ActionResult Create()
        {
           

            ViewBag.SportType1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSuppliers cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterSuppliers cat = new MasterSuppliers();
                cat.Name = cvm.Name;
                cat.OwnerName = cvm.OwnerName;
                cat.ContactNo = cvm.ContactNo;
                cat.EmailId = cvm.EmailId;
                cat.Address = cvm.Address;
                cat.PhotoPath = path;
                cat.cat_status = 1;
                cat.lat = cvm.lat;
                cat.Long = cvm.Long;
                cat.TotalDue = cvm.TotalDue;
                cat.OpeningDue = cvm.OpeningDue;
                cat.VendorID = cvm.VendorID;
                cat.CreateDate = cvm.CreateDate;

                ViewBag.SportType1 = new SelectList(db.masterVendors, "VendorID", "Name");
                db.masterSuppliers.Add(cat);
                db.SaveChanges();


                return RedirectToAction("Index");

            }

            return View();
        }
        //SupplierID,Name,OwnerName,ContactNo,EmailId,Address,PhotoPath,let,Long,TotalDue,OpeningDue,VendorID"
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
 

        // GET: MasterSuppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSuppliers masterSuppliers = db.masterSuppliers.Find(id);
            Session["FileName"] = masterSuppliers.PhotoPath;
            if (masterSuppliers == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportType1 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(masterSuppliers);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterSuppliers cvm, HttpPostedFileBase imgfile)
        {
            string path = "";
            var cat = db.masterSuppliers.Where(x => x.SupplierID == cvm.SupplierID).FirstOrDefault();
            if (cat.PhotoPath == Session["FileName"].ToString() && imgfile == null)
            {
                path = cat.PhotoPath;
            }
            else
            {
                path = uploadimgfile1(imgfile);

            }
            if (path.Equals("-1") && cat.PhotoPath != Session["FileName"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                cat.Name = cvm.Name;
                cat.OwnerName = cvm.OwnerName;
                cat.ContactNo = cvm.ContactNo;
                cat.EmailId = cvm.EmailId;
                cat.Address = cvm.Address;
                cat.PhotoPath = path;
                cat.cat_status = 1;
                cat.lat = cvm.lat;
                cat.Long = cvm.Long;
                cat.TotalDue = cvm.TotalDue;
                cat.OpeningDue = cvm.OpeningDue;
                cat.VendorID = cvm.VendorID;
                cat.CreateDate = cvm.CreateDate;

                ViewBag.SportType1 = new SelectList(db.masterVendors, "VendorID", "Name");
            
                db.SaveChanges();


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
                        path = "Content/upload/" + random + Path.GetFileName(file.FileName);

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
        // GET: MasterSuppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSuppliers masterSuppliers = db.masterSuppliers.Find(id);
            if (masterSuppliers == null)
            {
                return HttpNotFound();
            }
            return View(masterSuppliers);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterSuppliers masterSuppliers = db.masterSuppliers.Find(id);
            db.masterSuppliers.Remove(masterSuppliers);
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
