using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medhurry.Models;

namespace Medhurry.Controllers
{
    public class MasterCustomersController : Controller
    {
   MedhurryContext db = new MedhurryContext();

      
        public ActionResult Index()
        {
            return View(db.masterCustomers.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCustomers masterCustomers = db.masterCustomers.Find(id);
            if (masterCustomers == null)
            {
                return HttpNotFound();
            }
            return View(masterCustomers);
        }

       

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SportType = new SelectList(db.masterVendors, "VendorID", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterCustomers cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                try
                {
                    MasterCustomers cat = new MasterCustomers();

                    cat.FirstName = cvm.FirstName;
                    cat.LastName = cvm.LastName;
                    cat.CompanyName = cvm.CompanyName;
                    cat.ContactNo = cvm.ContactNo;
                    cat.EmailID = cvm.EmailID;
                    //cat.NID = cvm.NID;
                    cat.Address = cvm.Address;
                    cat.cat_status = 1;
                    cat.PhotoPath = path;
                    cat.TotalDue = cvm.TotalDue;
                    //cat.RefName = cvm.RefName;
                    cat.RefContact = cat.RefContact;
                    cat.RefAddress = cvm.RefAddress;
                    cat.CustomerType = cvm.CustomerType;
                    cat.VendorID = cvm.VendorID;
                    cat.lat = cvm.lat;
                    cat.Long = cvm.Long;
                    cat.IPAddress = cvm.IPAddress;
                    cat.CPassword = cvm.CPassword;
                    cat.CreateDate = cvm.CreateDate;

                    cat.isActive = false;
                    if (cvm.statusex == "Yes")
                    {
                        cat.isActive = true;
                    }
                    cat.OpeningDue = cvm.OpeningDue;
                    ViewBag.SportType = new SelectList(db.masterVendors, "VendorID", "Name");
                    db.masterCustomers.Add(cat);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }





            }

            return View();
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
            MasterCustomers masterCustomers = db.masterCustomers.Find(id);
            Session["FileName"] = masterCustomers.PhotoPath;
            ViewBag.SportType = new SelectList(db.masterVendors, "VendorID", "Name");
            if (masterCustomers == null)
            {
                return HttpNotFound();
            }
            return View(masterCustomers);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterCustomers cvm, HttpPostedFileBase imgfile)
        {
            string path = "";



            var cat = db.masterCustomers.Where(x => x.CustomerID == cvm.CustomerID).FirstOrDefault();
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
                

                cat.FirstName = cvm.FirstName;
                cat.LastName = cvm.LastName;
                cat.CompanyName = cvm.CompanyName;
                cat.ContactNo = cvm.ContactNo;
                cat.EmailID = cvm.EmailID;
                //cat.NID = cvm.NID;
                cat.Address = cvm.Address;
                cat.cat_status = 1;
                cat.PhotoPath = path;
                cat.TotalDue = cvm.TotalDue;
                //cat.RefName = cvm.RefName;
                cat.RefContact = cat.RefContact;
                cat.RefAddress = cvm.RefAddress;
                cat.CustomerType = cvm.CustomerType;

                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }

                cat.VendorID = cvm.VendorID;
                cat.lat = cvm.lat;
                cat.Long = cvm.Long;
                cat.IPAddress = cvm.IPAddress;
                cat.CPassword = cvm.CPassword;
                cat.CreateDate = cvm.CreateDate;
                cat.OpeningDue = cvm.OpeningDue;
                ViewBag.SportType = new SelectList(db.masterVendors, "VendorID", "Deepak");
              
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
                        path = "/Content/upload/" + random + Path.GetFileName(file.FileName);

                    
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
            MasterCustomers masterCustomers = db.masterCustomers.Find(id);
            if (masterCustomers == null)
            {
                return HttpNotFound();
            }
            return View(masterCustomers);
        }

        // POST: MasterCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterCustomers masterCustomers = db.masterCustomers.Find(id);
            db.masterCustomers.Remove(masterCustomers);
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
