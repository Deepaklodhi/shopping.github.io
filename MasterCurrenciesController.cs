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
    public class MasterCurrenciesController : Controller
    {
        private MedhurryContext db = new MedhurryContext();

  
        public ActionResult Index()
        {
            return View(db.masterCurrencies.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCurrency masterCurrency = db.masterCurrencies.Find(id);
            if (masterCurrency == null)
            {
                return HttpNotFound();
            }
            return View(masterCurrency);
        }

        // GET: MasterCurrencies/Create
        public ActionResult Create()
        {
            var getprodcut = db.masterCurrencies.FirstOrDefault();
            var mspro = new MasterCurrency();
            if (getprodcut != null)
            {
                var maxcode = db.masterCurrencies.OrderByDescending(x => x.ID).Select(x => x.Code).FirstOrDefault();
                if (maxcode != null)
                {
                    mspro.Code = maxcode.ToString();
                }
            }
            return View(mspro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MasterCurrency cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                MasterCurrency cat = new MasterCurrency();
                cat.CName = cvm.CName;
                cat.Symbol = path;
                cat.cat_status= 1;
                if (cvm.Code == null)
                {
                    cat.Code = "00001";//cvm.Code;
                }
                else
                {
                    int maxcode = Convert.ToInt32(cvm.Code);
                    maxcode = maxcode + 1;
                    //int getfinalcode = maxcode + maxcode;
                    cat.Code = "0000" + maxcode.ToString();
                }
                cat.ExchangeRate = cvm.ExchangeRate;
                cat.EmailAddress = cvm.EmailAddress;
                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }

                db.masterCurrencies.Add(cat);
                db.SaveChanges();
              
                return RedirectToAction("Index");

            }

            return View();
        }

        public JsonResult CheckUserName(string CName, string Symbol)
        {
            return Json(!db.masterCurrencies.Any(X => X.CName == CName & X.Symbol == Symbol), JsonRequestBehavior.AllowGet);
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
        //[Bind(Include = "ID,CName,Symbol,ExchangeRate,Code,isActive")]
        // GET: MasterCurrencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCurrency masterCurrency = db.masterCurrencies.Find(id);
            Session["FileName"] = masterCurrency.Symbol;
            if (masterCurrency == null)
            {
                return HttpNotFound();
            }
            return View(masterCurrency);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterCurrency cvm, HttpPostedFileBase imgfile)
        {
            string path = "";
            var cat = db.masterCurrencies.Where(x => x.ID == cvm.ID).FirstOrDefault();
            if (cat.Symbol == Session["FileName"].ToString() && imgfile == null)
            {
                path = cat.Symbol;
            }
            else
            {
                path = uploadimgfile1(imgfile);

            }
            if (path.Equals("-1") && cat.Symbol != Session["FileName"].ToString())
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
               
                cat.CName = cvm.CName;
                cat.Symbol = path;
                cat.cat_status = 1;
                //if (cvm.Code == null)
                //{
                //    cat.Code = "00001";//cvm.Code;
                //}
                //else
                //{
                //    int maxcode = Convert.ToInt32(cvm.Code);
                //    maxcode = maxcode + 1;
                //    //int getfinalcode = maxcode + maxcode;
                //    cat.Code = "0000" + maxcode.ToString();
                //}
                cat.Code = cvm.Code;
                cat.Symbol = path;
                cat.cat_status = 1;
                cat.ExchangeRate = cvm.ExchangeRate;
                cat.EmailAddress = cvm.EmailAddress;
                cat.isActive = false;
                if (cvm.statusex == "Yes")
                {
                    cat.isActive = true;
                }

                db.masterCurrencies.Add(cat);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCurrency masterCurrency = db.masterCurrencies.Find(id);
            if (masterCurrency == null)
            {
                return HttpNotFound();
            }
            return View(masterCurrency);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterCurrency masterCurrency = db.masterCurrencies.Find(id);
            db.masterCurrencies.Remove(masterCurrency);
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
