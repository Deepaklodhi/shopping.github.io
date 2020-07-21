using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medhurry.Models;

namespace Medhurry.Controllers
{
    public class SettingsController : Controller
    {
      MedhurryContext db = new MedhurryContext();

        // GET: Settings
        public ActionResult Index()
        {
            return View(db.settings.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Settings settings = db.settings.Find(id);
            if (settings == null)
            {
                return HttpNotFound();
            }
            return View(settings);
        }

      
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Settings cvm)
        {
            
                Settings cat = new Settings();
                cat.WebsiteName = cvm.WebsiteName;
                cat.Email = cvm.Email;
                cat.Phone = cvm.Phone;
                cat.CustomerCare = cvm.CustomerCare;
                cat.Sales = cvm.Sales;
                cat.TechnicalSupport = cvm.TechnicalSupport;
                cat.WAddress = cvm.WAddress;
                cat.TimeZone = cvm.TimeZone;
                cat.Facebook = cvm.Facebook;
                cat.Google = cvm.Google;
                cat.Twitter = cvm.Twitter;
                cat.Instagram = cvm.Instagram;
                cat.Linkedin = cvm.Linkedin;
                cat.Skype = cvm.Skype;
                cat.Logo = cvm.Logo;
                cat.Favicon = cvm.Favicon;
                cat.BaseURL = cvm.BaseURL;
                cat.AndroidAppURL = cvm.AndroidAppURL;
                cat.iOSAppURL = cvm.iOSAppURL;
                cat.DeliveryBoyAppURL = cvm.DeliveryBoyAppURL;
                cat.LogisticsAppURL = cvm.LogisticsAppURL;
                cat.CommissionPer = cvm.CommissionPer;
                cat.MinVendorPayOut = cvm.MinVendorPayOut;
            //cat.EnableVendor = cvm.EnableVendor;
            //cat.EnableMaintenance = cvm.EnableMaintenance;
            cat.EnableVendor = false;
            if (cvm.statusex == "Yes")
            {
                cat.EnableVendor = true;
            }
            //cat.EnableMaintenance = cvm.EnableMaintenance;
            cat.EnableMaintenance = false;
            if (cvm.statusex1 == "Yes")
            {
                cat.EnableMaintenance = true;
            }
            cat.DisplayRecordPerPage = cvm.DisplayRecordPerPage;
                cat.HeaderColor = cvm.HeaderColor;
                cat.FooterColor = cvm.FooterColor;
                cat.FooterText = cvm.FooterText;
                cat.MinOrderValue = cvm.MinOrderValue;
                cat.MaxOrderValue = cvm.MaxOrderValue;
                cat.Commission =cvm.Commission;
                cat.DeliveryCost = cvm.DeliveryCost;
                cat.DefaultCurrency = cvm.DefaultCurrency;
                db.settings.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            
           
            
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Settings settings = db.settings.Find(id);
            if (settings == null)
            {
                return HttpNotFound();
            }
            return View(settings);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Settings cvm)
        {
            var cat = db.settings.Where(x => x.ID == cvm.ID).FirstOrDefault();
            cat.WebsiteName = cvm.WebsiteName;
            cat.Email = cvm.Email;
            cat.Phone = cvm.Phone;
            cat.CustomerCare = cvm.CustomerCare;
            cat.Sales = cvm.Sales;
            cat.TechnicalSupport = cvm.TechnicalSupport;
            cat.WAddress = cvm.WAddress;
            cat.TimeZone = cvm.TimeZone;
            cat.Facebook = cvm.Facebook;
            cat.Google = cvm.Google;
            cat.Twitter = cvm.Twitter;
            cat.Instagram = cvm.Instagram;
            cat.Linkedin = cvm.Linkedin;
            cat.Skype = cvm.Skype;
            cat.Logo = cvm.Logo;
            cat.Favicon = cvm.Favicon;
            cat.BaseURL = cvm.BaseURL;
            cat.AndroidAppURL = cvm.AndroidAppURL;
            cat.iOSAppURL = cvm.iOSAppURL;
            cat.DeliveryBoyAppURL = cvm.DeliveryBoyAppURL;
            cat.LogisticsAppURL = cvm.LogisticsAppURL;
            cat.CommissionPer = cvm.CommissionPer;
            cat.MinVendorPayOut = cvm.MinVendorPayOut;
            //cat.EnableVendor = cvm.EnableVendor;
            cat.EnableVendor = false;
            if (cvm.statusex == "Yes")
            {
                cat.EnableVendor = true;
            }
            //cat.EnableMaintenance = cvm.EnableMaintenance;
            cat.EnableMaintenance = false;
            if (cvm.statusex == "Yes")
            {
                cat.EnableMaintenance = true;
            }
            cat.DisplayRecordPerPage = cvm.DisplayRecordPerPage;
            cat.HeaderColor = cvm.HeaderColor;
            cat.FooterColor = cvm.FooterColor;
            cat.FooterText = cvm.FooterText;
            cat.MinOrderValue = cvm.MinOrderValue;
            cat.MaxOrderValue = cvm.MaxOrderValue;
            cat.Commission = cvm.Commission;
            cat.DeliveryCost = cvm.DeliveryCost;
            cat.DefaultCurrency = cvm.DefaultCurrency;
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Settings settings = db.settings.Find(id);
            if (settings == null)
            {
                return HttpNotFound();
            }
            return View(settings);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Settings settings = db.settings.Find(id);
            db.settings.Remove(settings);
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
