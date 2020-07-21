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
    public class MasterBrandsController : Controller
    {
        MedhurryContext db = new MedhurryContext();

        

        public ActionResult Index()
        {
            return View(db.masterBrands.ToList());
        }

        // GET: MasterBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBrand masterBrand = db.masterBrands.Find(id);
            if (masterBrand == null)
            {
                return HttpNotFound();
            }
            return View(masterBrand);
        }

        // GET: MasterBrands/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterBrand cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterBrand cat = new MasterBrand();
                cat.Brand = cvm.Brand;
                cat.BrandPhoto = path;
                cat.cat_status = 1;
                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }
                cat.BrandMobile = cvm.BrandMobile;
                cat.BrandEmail = cvm.BrandEmail;
                cat.BrandAddress = cvm.BrandAddress;
                cat.BrandDetails = cvm.BrandDetails;

                ViewBag.SportType = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
                //cat.CategoryID = Convert.ToInt32(Session["FK_CategoryID"].ToString());

                db.masterBrands.Add(cat);
                db.SaveChanges();
               
                return RedirectToAction("Index");

                //BrandID,Brand,BrandPhoto,isActive,BrandMobile,BrandEmail,BrandAddress,BrandDetails
            }
            return View();
        }
        public JsonResult CheckUserName(string Brand)
        {
            return Json(!db.masterBrands.Any(X => X.Brand == Brand), JsonRequestBehavior.AllowGet);
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


        // GET: MasterBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBrand masterBrand = db.masterBrands.Find(id);
            Session["FileName"] = masterBrand.BrandPhoto;
            if (masterBrand == null)
            {
                return HttpNotFound();
            }
            return View(masterBrand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterBrand cvm, HttpPostedFileBase imgfile)
        {
            var cat = db.masterBrands.Where(x => x.BrandID == cvm.BrandID).FirstOrDefault();
            string path = "";
            if (cat.BrandPhoto == Session["FileName"].ToString() && imgfile == null)
            {
                path = cat.BrandPhoto;
            }
            else
            {
                path = uploadimgfile1(imgfile);

            }
            if (path.Equals("-1") && cat.BrandPhoto != Session["FileName"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
               
                cat.Brand = cvm.Brand;
                cat.BrandPhoto = path;
                cat.cat_status = 1;
                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }
                cat.BrandMobile = cvm.BrandMobile;
                cat.BrandEmail = cvm.BrandEmail;
                cat.BrandAddress = cvm.BrandAddress;
                cat.BrandDetails = cvm.BrandDetails;

                //ViewBag.SportType = new SelectList(db.MasterCategories, "CategoryID", "CatName");
                //cat.CategoryID = Convert.ToInt32(Session["FK_CategoryID"].ToString());


                db.SaveChanges();
                ViewBag.MasterCategory = new SelectList(db.MasterCategorys, "IisActive");
                return RedirectToAction("Index");

                //BrandID,Brand,BrandPhoto,isActive,BrandMobile,BrandEmail,BrandAddress,BrandDetails
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

        // GET: MasterBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterBrand masterBrand = db.masterBrands.Find(id);
            if (masterBrand == null)
            {
                return HttpNotFound();
            }
            return View(masterBrand);
        }

        // POST: MasterBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterBrand masterBrand = db.masterBrands.Find(id);
            db.masterBrands.Remove(masterBrand);
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