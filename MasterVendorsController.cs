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
    public class MasterVendorsController : Controller
    {
    MedhurryContext db = new MedhurryContext();

        // GET: MasterVendors
        public ActionResult Index()
        {
            return View(db.masterVendors.ToList());
        }

        // GET: MasterVendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterVendor masterVendor = db.masterVendors.Find(id);
            if (masterVendor == null)
            {
                return HttpNotFound();
            }
            return View(masterVendor);
        }

        // GET: MasterVendors/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = ")] MasterVendor masterVendor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MasterVendors.Add(masterVendor);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(masterVendor);
        //}



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SportType = new SelectList(db.masterPackages, "PackageID", "Package");


             var getprodcut = db.masterVendors.FirstOrDefault();
            var mspro = new MasterVendor();
            if (getprodcut != null)
            {
                var maxcode = db.masterVendors.OrderByDescending(x => x.VendorID).Select(x => x.code).FirstOrDefault();
                if (maxcode != null)
                {
                    mspro.code = maxcode.ToString();
                }
            }
            return View(mspro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterVendor cvm)
        {



           
            string path1 = uploadimgfile(cvm.customerImage1);
            string path2 = uploadimgfile(cvm.customerImage2);
            string path3 = uploadimgfile(cvm.customerImage3);
            string path4 = uploadimgfile(cvm.customerImage4);
            string path5 = uploadimgfile(cvm.customerImage5);
            string path6 = uploadimgfile(cvm.customerImage6);
            if (path1.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path1.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path2.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path3.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path4.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path5.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
          
            if (path6.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterVendor cat = new MasterVendor();
                cat.Name = cvm.Name;
                cat.Document1 = path1;
                cat.Document2 = path2;
                cat.Document3 = path3;
                cat.cat_status = 1;
                cat.Document4 = path4;
                cat.Document5 = path5;
                cat.Photo = path6;
                cat.Address = cvm.Address;
                cat.TelephoneNo = cvm.TelephoneNo;
                cat.EmailAddress = cvm.EmailAddress;
                cat.WebAddress = cvm.WebAddress;
                cat.SystemStartDate = cvm.SystemStartDate;
                cat.SystemExpiryDate = cvm.SystemExpiryDate;
                cat.PackageID = cvm.PackageID;
                cat.DisplayName = cvm.DisplayName;

                cat.Descriptions = cvm.Descriptions;
                cat.Facebook = cvm.Facebook;
                cat.WebAddress = cvm.WebAddress;
                cat.Skype = cvm.Skype;
                cat.GooglePlus = cvm.GooglePlus;
                cat.Twitter = cvm.Twitter;
                cat.Youtube = cvm.Youtube;
                cat.Linkedin = cvm.Linkedin;
                cat.Address1 = cvm.Address1;
                cat.Address2 = cvm.Address2;
                cat.City = cvm.City;
                cat.AState = cvm.AState;
                cat.PIN = cvm.PIN;
                cat.Country = cvm.Country;
                cat.Meta = cvm.Meta;
                cat.Keywords = cvm.Keywords;
                //db.masterVendors.Add(cat);
                //db.SaveChanges();




                cat.lat = cvm.lat;
                cat.Long = cvm.Long;
                if (cvm.code == null)
                {
                    cat.code = "00001";//cvm.Code;
                }
                else
                {
                    int maxcode = Convert.ToInt32(cvm.code);
                    maxcode = maxcode + 1;
                    //int getfinalcode = maxcode + maxcode;
                    cat.code = "0000" + maxcode.ToString();
                }
                cat.CreateDate = cvm.CreateDate;
                cat.isFranchise = cvm.isFranchise;
                cat.VendorType = cvm.VendorType;
               
             

                ViewBag.SportType = new SelectList(db.masterPackages, "PackageID", "Package");
                //cat.CategoryID = Convert.ToInt32(Session["FK_CategoryID"].ToString());

                db.masterVendors.Add(cat);
                db.SaveChanges();

                return RedirectToAction("Index");
                //VendorID,Name,Address,TelephoneNo,EmailAddress,WebAddress,SystemStartDate,SystemExpiryDate,PackageID,ProductPhotoPath,SupplierPhotoPath,CustomerPhotoPath,CustomerNIDPatht,SupplierDocPath,EmployeePhotoPath"

            }

            return View();
        } //end,,,,,,,,,,,,,,,,,,,
        public JsonResult CheckUserName(string EmailAddress, string TelephoneNo)
        {
            return Json(!db.masterVendors.Any(X => X.EmailAddress == EmailAddress & X.TelephoneNo == TelephoneNo), JsonRequestBehavior.AllowGet);
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






        // GET: MasterVendors/Edit/5
        public ActionResult Edit(int? id)
        {
          
            MasterVendor masterVendor = db.masterVendors.Find(id);
            ViewBag.SportType = new SelectList(db.masterPackages, "PackageID", "Package");
            Session["customerImage1"] = masterVendor.Document1;
            Session["customerImage2"] = masterVendor.Document2;
            Session["customerImage3"] = masterVendor.Document3;
            Session["customerImage4"] = masterVendor.Document4;
            Session["customerImage5"] = masterVendor.Document5;
            Session["customerImage6"] = masterVendor.Photo;
            return View(masterVendor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterVendor cvm)
        {
            var cat = db.masterVendors.Where(x => x.VendorID == cvm.VendorID).FirstOrDefault();
            string path1 = "customerImage1";
            string path2 = "customerImage2";
            string path3 = "customerImage3";
            string path4 = "customerImage4";
            string path5 = "customerImage5";
            string path6 = "customerImage6";
            if (cat.Document1 == Session["customerImage1"].ToString() && cvm.Document1 == null)
            {
                path1 = cat.Document1;
            }
            else
            {
                path1 = uploadimgfile(cvm.customerImage1);
            }
            if (cat.Document2 == Session["customerImage2"].ToString() && cvm.Document2 == null)
            {
                path2 = cat.Document2;
            }
            else
            {
                path2 = uploadimgfile(cvm.customerImage2);
            }
            if (cat.Document3 == Session["customerImage3"].ToString() && cvm.Document3 == null)
            {
                path3 = cat.Document3;
            }
            else
            {
                path3 = uploadimgfile(cvm.customerImage3);
            }


            if (cat.Document4 == Session["customerImage4"].ToString() && cvm.Document4 == null)
            {
                path4 = cat.Document4;
            }
            else
            {
                path4 = uploadimgfile(cvm.customerImage4);
            }
            if (cat.Document5 == Session["customerImage5"].ToString() && cvm.Document5 == null)
            {
                path5 = cat.Document5;
            }
            else
            {
                path5 = uploadimgfile(cvm.customerImage5);
            }

            if (cat.Photo == Session["customerImage6"].ToString() && cvm.Photo == null)
            {
                path6 = cat.Photo;
            }
            else
            {
                path6 = uploadimgfile(cvm.customerImage6);
            }



         

            if (path1.Equals("-1") && cat.Document1 != Session["customerImage1"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }

            if (path2.Equals("-1") && cat.Document2 != Session["customerImage2"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path3.Equals("-1") && cat.Document3 != Session["customerImage3"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path4.Equals("-1") && cat.Document4 != Session["customerImage4"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            if (path5.Equals("-1") && cat.Document5 != Session["customerImage5"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }

            if (path6.Equals("-1") && cat.Photo != Session["customerImage6"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                cat.Name = cvm.Name;
                cat.Document1 = path1;
                cat.Document2 = path2;
                cat.Document3 = path3;
                cat.cat_status = 1;
                cat.Document4 = path4;
                cat.Document5 = path5;
                cat.Photo = path6;
                cat.Address = cvm.Address;
                cat.TelephoneNo = cvm.TelephoneNo;
                cat.EmailAddress = cvm.EmailAddress;
                cat.WebAddress = cvm.WebAddress;
                cat.SystemStartDate = cvm.SystemStartDate;
                cat.SystemExpiryDate = cvm.SystemExpiryDate;
                cat.PackageID = cvm.PackageID;
                cat.VendorType = cvm.VendorType;
                cat.lat = cvm.lat;
                cat.Long = cvm.Long;
                cat.code = cvm.code;
                cat.CreateDate = cvm.CreateDate;
                cat.isFranchise = cvm.isFranchise;
                cat.DisplayName = cvm.DisplayName;
                cat.Descriptions = cvm.Descriptions;
                cat.Facebook = cvm.Facebook;
                cat.WebAddress = cvm.WebAddress;
                cat.Skype = cvm.Skype;
                cat.GooglePlus = cvm.GooglePlus;
                cat.Twitter = cvm.Twitter;
                cat.Youtube = cvm.Youtube;
                cat.Linkedin = cvm.Linkedin;
                cat.Address1 = cvm.Address1;
                cat.Address2 = cvm.Address2;
                cat.City = cvm.City;
                cat.AState = cvm.AState;
                cat.PIN = cvm.PIN;
                cat.Country = cvm.Country;
                cat.Meta = cvm.Meta;
                cat.Keywords = cvm.Keywords;


                ViewBag.SportType = new SelectList(db.masterPackages, "PackageID", "Package");
               


                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
            }



        // GET: MasterVendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterVendor masterVendor = db.masterVendors.Find(id);
            if (masterVendor == null)
            {
                return HttpNotFound();
            }
            return View(masterVendor);
        }

        // POST: MasterVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterVendor masterVendor = db.masterVendors.Find(id);
            db.masterVendors.Remove(masterVendor);
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
