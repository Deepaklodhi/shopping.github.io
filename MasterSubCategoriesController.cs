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
    public class MasterSubCategoriesController : Controller
    {
        MedhurryContext db = new MedhurryContext();

        public ActionResult Index()
        {
            var masterSubCategories = db.masterSubCategories.Include(m => m.MasterCategorys);
            return View(masterSubCategories.ToList());
        }


        public ActionResult Details(int? id)
        {

            MasterSubCategory masterSubCategory = db.masterSubCategories.Find(id);


            return View(masterSubCategory);
        }


      





        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SportType = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(MasterSubCategory cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterSubCategory cat = new MasterSubCategory();
                cat.SubCatName = cvm.SubCatName;
                cat.SubCatPhoto = path;
                cat.cat_status = 1;
                cat.Description = cvm.Description;

                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }

                cat.CategoryID = cvm.CategoryID;
                ViewBag.SportType = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
                //cat.CategoryID = Convert.ToInt32(Session["FK_CategoryID"].ToString());

                db.masterSubCategories.Add(cat);
                db.SaveChanges();

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

        // GET: MasterSubCategories/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            MasterSubCategory masterSubCategory = db.masterSubCategories.Find(id);
            Session["FileName"] = masterSubCategory.SubCatPhoto;
            ViewBag.SportType = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
            return View(masterSubCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterSubCategory cvm, HttpPostedFileBase imgfile)
        {
            string path = "";
          


                var cat = db.masterSubCategories.Where(x => x.SubCategoryID == cvm.SubCategoryID).FirstOrDefault();
                if (cat.SubCatPhoto== Session["FileName"].ToString() && imgfile == null)
                {
                    path = cat.SubCatPhoto;
                }
                else
                {
                    path = uploadimgfile1(imgfile);

                }
                if (path.Equals("-1") && cat.SubCatPhoto != Session["FileName"].ToString())
                {
                    ViewBag.error = "Image could not be uploaded....";
                }
                else
                {
                    cat.SubCatName = cvm.SubCatName;
                    cat.SubCatPhoto = path;
                    cat.cat_status = 1;
                    cat.Description = cvm.Description;

                    cat.CategoryID = cvm.CategoryID;
                    ViewBag.SportType = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
                    cat.isActive = false;
                    if (cvm.statusex == "Yes")
                    {
                        cat.isActive = true;
                    }

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

            MasterSubCategory masterSubCategory = db.masterSubCategories.Find(id);

            return View(masterSubCategory);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterSubCategory masterSubCategory = db.masterSubCategories.Find(id);
            db.masterSubCategories.Remove(masterSubCategory);
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
