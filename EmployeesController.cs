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
    public class EmployeesController : Controller
    {
      MedhurryContext db = new MedhurryContext();

     
        public ActionResult Index()
        {
            var employees = db.employees.Include(e => e.designations).Include(e => e.MasterVendor);
            return View(employees.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

       
        [HttpGet]
        public ActionResult Create()
        {


            ViewBag.SportType1 = new SelectList(db.designations, "DesignationID", "Description");
            ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                Employees cat = new Employees();
                cat.FirstName = cvm.FirstName;
                cat.PhotoPath = path;
                cat.cat_status = 1;
                cat.MiddleName = cvm.MiddleName;
                cat.LastName = cvm.LastName;
                cat.ContactNo = cvm.ContactNo;
              
                cat.BloodGroup = cvm.BloodGroup;
                cat.JoiningDate = cvm.JoiningDate;
                cat.PresentAdd = cvm.PresentAdd;
                cat.PermanentAdd = cvm.PermanentAdd;
                cat.DesignationID = cvm.DesignationID;
                cat.GrossSalary = cvm.GrossSalary;
                cat.VendorID = cvm.VendorID;
                cat.EmailID = cvm.EmailID;
                db.employees.Add(cat);
                db.SaveChanges();
                ViewBag.SportType1 = new SelectList(db.designations, "DesignationID", "Description");
                ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
                return RedirectToAction("Index");
            }

            return View();
        } //end,,,,,,,,,,,,,,,,,,,



 


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

                        path = Path.Combine(Server.MapPath("/Content/upload"), random + Path.GetFileName(file.FileName));
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
            Employees employees = db.employees.Find(id);
            Session["FileName"] = employees.PhotoPath;
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportType1 = new SelectList(db.designations, "DesignationID", "Description");
            ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(employees);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employees cvm, HttpPostedFileBase imgfile)
        {
            string path = "";
            var cat = db.employees.Where(x => x.EmployeeID == cvm.EmployeeID).FirstOrDefault();
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
                cat.PhotoPath = path;
                cat.cat_status = 1;
                cat.MiddleName = cvm.MiddleName;
                cat.LastName = cvm.LastName;
                cat.ContactNo = cvm.ContactNo;
               
                cat.BloodGroup = cvm.BloodGroup;
                cat.JoiningDate = cvm.JoiningDate;
                cat.PresentAdd = cvm.PresentAdd;
                cat.PermanentAdd = cvm.PermanentAdd;
                cat.DesignationID = cvm.DesignationID;
                cat.GrossSalary = cvm.GrossSalary;
                cat.VendorID = cvm.VendorID;
                cat.EmailID = cvm.EmailID;

                db.SaveChanges();

                ViewBag.SportType1 = new SelectList(db.designations, "DesignationID", "Description");
                ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
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

                        path = Path.Combine(Server.MapPath("/Content/upload"), random + Path.GetFileName(file.FileName));
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
            Employees employees = db.employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.employees.Find(id);
            db.employees.Remove(employees);
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
