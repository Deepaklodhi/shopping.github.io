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
    public class MasterCategoriesController : Controller
    {
        MedhurryContext db = new MedhurryContext();



        public ActionResult Index()
        {


            return View(db.MasterCategorys.ToList());

        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCategory masterCategory = db.MasterCategorys.Find(id);
            if (masterCategory == null)
            {
                return HttpNotFound();
            }
            return View(masterCategory);
        }

        









        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterCategory cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterCategory cat = new MasterCategory();
                cat.CatName = cvm.CatName;
                cat.CatPhoto = path;
                cat.cat_status = 1;
                cat.Description = cvm.Description;
                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }

                db.MasterCategorys.Add(cat);
                db.SaveChanges();
                ViewBag.MasterCategory = new SelectList(db.MasterCategorys, "IisActive");
                return RedirectToAction("Index");

            }

            return View();
        } //end,,,,,,,,,,,,,,,,,,,

         
        public JsonResult CheckUserName(string CatName)
        {
            return Json(!db.MasterCategorys.Any(X=>X.CatName==CatName), JsonRequestBehavior.AllowGet);
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

        public ActionResult Edit(int? id)
        {
            MasterCategory masterCategory = db.MasterCategorys.Find(id);
            Session["FileName"] = masterCategory.CatPhoto;
            return View(masterCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterCategory cvm, HttpPostedFileBase imgfile)
        {

            string path = "";
            var cat = db.MasterCategorys.Where(x => x.CategoryID == cvm.CategoryID).FirstOrDefault();
            if (cat.CatPhoto == Session["FileName"].ToString() && imgfile == null)
            {
                path = cat.CatPhoto;
            }
            else
            {
                path = uploadimgfile1(imgfile);

            }
            if (path.Equals("-1") && cat.CatPhoto != Session["FileName"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                cat.CatName = cvm.CatName;
                cat.CatPhoto = path;
                cat.cat_status = 1;
                cat.Description = cvm.Description;
                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }
                //db.MasterCategories.Add(cat);
                //  db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.MasterCategory = new SelectList(db.MasterCategorys, "IisActive");
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


        // GET: MasterCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCategory masterCategory = db.MasterCategorys.Find(id);
            if (masterCategory == null)
            {
                return HttpNotFound();
            }
            return View(masterCategory);
        }

        // POST: MasterCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterCategory masterCategory = db.MasterCategorys.Find(id);
            db.MasterCategorys.Remove(masterCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
