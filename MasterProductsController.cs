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
    public class MasterProductsController : Controller
    {
        MedhurryContext db = new MedhurryContext();

        // GET: MasterProducts
        public ActionResult Index()
        {
            var masterProducts = db.masterProducts.Include(m => m.MasterBrand).Include(m => m.MasterCategory);
            return View(masterProducts.ToList());
        }

        // GET: MasterProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProducts masterProduct = db.masterProducts.Find(id);
            if (masterProduct == null)
            {
                return HttpNotFound();
            }
            return View(masterProduct);
        }

        // GET: MasterProducts/Create
        public ActionResult Create()
        {
            ViewBag.SportType3 = new SelectList(db.masterBrands, "BrandID", "Brand");
            ViewBag.SportType1 = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
            //ViewBag.SportType2 = new SelectList(db.masterSubCategories, "SubCategoryID", "SubCatName");

         

            var getprodcut = db.masterProducts.FirstOrDefault();
            var mspro = new MasterProducts();
            if (getprodcut != null)
            {
                var maxcode = db.masterProducts.OrderByDescending(x => x.ProductID).Select(x => x.Code).FirstOrDefault();
                if (maxcode != null)
                {
                    mspro.Code = maxcode.ToString();
                }
            }
            return View(mspro);
        }

      
            public JsonResult GetCategory(int CategoryID)
        {
            List<MasterSubCategory> masterSubCategories = db.masterSubCategories.Where(X=>X.CategoryID ==CategoryID).ToList();
            return Json(masterSubCategories,JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckUserName(string ProductName)
        {
            return Json(!db.masterProducts.Any(X => X.ProductName == ProductName), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterProducts cvm, HttpPostedFileBase imgfile)


        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterProducts cat = new MasterProducts();
                cat.ProductName = cvm.ProductName;
                cat.Photo = path;
                cat.cat_status = 1;
                cat.GenericName = cvm.GenericName;
                if (cvm.Code == null)
                {
                    cat.Code = "0001";//cvm.Code;
                }
                else
                {
                    int maxcode = Convert.ToInt32(cvm.Code);
                    maxcode = maxcode + 1;
                    //int getfinalcode = maxcode + maxcode;
                    cat.Code = "000"+ maxcode.ToString();
                }
                //cat.isActive = cvm.isActive;
                //cat.CategoryID = cvm.CategoryID;
                cat.BatchNumber = cvm.BatchNumber;
                cat.HSN = cvm.HSN;
                cat.CategoryID = cvm.CategoryID;
                cat.SubCategoryID = cvm.SubCategoryID;
                cat.Composition = cvm.Composition;
                //*************************************************************
                //string[] comparr = cvm.Composition.Split(',');
                //foreach (string author in authorsList)
                    //*****************************************************************
                    cat.BrandID = cvm.BrandID;
                //ViewBag.SportType1 = new SelectList(db.MasterCategories, "CategoryID", "CatName");


                ViewBag.SportType3 = new SelectList(db.masterBrands, "BrandID", "Brand");
                //ViewBag.SportType1 = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
                //ViewBag.SportType2 = new SelectList(db.masterSubCategories, "SubCategoryID", "SubCatName");

                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }


                db.masterProducts.Add(cat);
                db.SaveChanges();

               
                return RedirectToAction("Index");




            }



            return View();
        }


        //public JsonResult GetStateList(int CategoryID)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<MasterSubCategory> subCategorieslist = db.masterSubCategories.Where(x => x.CategoryID == CategoryID).ToList();
        //    return Json(subCategorieslist, JsonRequestBehavior.AllowGet);
        //}
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


        // GET: MasterProducts/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var getprodcut = db.masterProducts.FirstOrDefault();
            //var mspro = new MasterProduct();
            //if (getprodcut != null)
            //{
            //    var maxcode = db.masterProducts.OrderByDescending(x => x.ProductID).Select(x => x.Code).FirstOrDefault();
            //    if (maxcode != null)
            //    {
            //        mspro.Code = maxcode.ToString();
            //    }
            //}
            MasterProducts masterProduct = db.masterProducts.Find(id);
            Session["FileName"] = masterProduct.Photo;
            if (masterProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportType3 = new SelectList(db.masterBrands, "BrandID", "Brand");
            ViewBag.SportType1 = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
            //ViewBag.SportType3 = new SelectList(db.masterBrands, "BrandID", "Brand");
            //ViewBag.SportType1 = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
            //ViewBag.SportType2 = new SelectList(db.masterSubCategories, "SubCategoryID", "SubCatName");
            return View(masterProduct);
        }
        public JsonResult GetCategory1(int CategoryID)
        {
            List<MasterSubCategory> masterSubCategories = db.masterSubCategories.Where(X => X.CategoryID == CategoryID).ToList();
            return Json(masterSubCategories, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterProducts cvm, HttpPostedFileBase imgfile)
        {
            string path = "";
            var cat = db.masterProducts.Where(x => x.ProductID == cvm.ProductID).FirstOrDefault();

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
               
                cat.ProductName = cvm.ProductName;
                cat.Photo = path;
                cat.cat_status = 1;
                cat.GenericName = cvm.GenericName;
                //if (cvm.Code == null)
                //{
                //    cat.Code = "0001";//cvm.Code;
                //}
                //else
                //{
                //    int maxcode = Convert.ToInt32(cvm.Code);
                //    maxcode = maxcode + 1;
                //    //int getfinalcode = maxcode + maxcode;
                //    cat.Code = "000" + maxcode.ToString();
                //}
                //cat.isActive = cvm.isActive;
                //cat.CategoryID = cvm.CategoryID;
                cat.BatchNumber = cvm.BatchNumber;
                cat.Code = cvm.Code;
                cat.HSN = cvm.HSN;
                cat.CategoryID = cvm.CategoryID;
                cat.SubCategoryID = cvm.SubCategoryID;
                cat.BrandID = cvm.BrandID;
                //ViewBag.SportType1 = new SelectList(db.MasterCategorys, "CategoryID", "CatName");
                //ViewBag.SportType2 = new SelectList(db.masterSubCategories, "SubCategoryID", "SubCatName");
                ViewBag.SportType3 = new SelectList(db.masterBrands, "BrandID", "Brand");
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
        // GET: MasterProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProducts masterProduct = db.masterProducts.Find(id);
            if (masterProduct == null)
            {
                return HttpNotFound();
            }
            return View(masterProduct);
        }

        // POST: MasterProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterProducts masterProduct = db.masterProducts.Find(id);
            db.masterProducts.Remove(masterProduct);
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
