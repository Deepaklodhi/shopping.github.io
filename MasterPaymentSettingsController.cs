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
    public class MasterPaymentSettingsController : Controller
    {
 MedhurryContext db = new MedhurryContext();

     
        public ActionResult Index()
        {
            return View(db.masterPaymentSettings.ToList());
        }

  
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPaymentSettings masterPaymentSettings = db.masterPaymentSettings.Find(id);
            if (masterPaymentSettings == null)
            {
                return HttpNotFound();
            }
            return View(masterPaymentSettings);
        }

  
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterPaymentSettings cvm)
        {
            MasterPaymentSettings cat = new MasterPaymentSettings();
            cat.COD = false;
            if (cvm.statusex == "Yes")
            {
                cat.COD = true;
            }
            cat.Stripe = false;
            if (cvm.statusez == "Yes")
            {
                cat.Stripe = true;
            }

            cat.Paypal = false;
            if (cvm.statusr == "Yes")
            {
                cat.Paypal = true;
            }

            cat.Razorpay = false;
            if (cvm.statusx == "Yes")
            {
                cat.Razorpay = true;
            }
            cat.RazorpayTestLive = false;
            if (cvm.statusy == "Yes")
            {
                cat.RazorpayTestLive = true;
            }
            cat.Paytm = false;
            if (cvm.statusp == "Yes")
            {
                cat.Paytm = true;
            }
            cat.StripeSecret = cvm.StripeSecret;
            cat.StripePublish = cvm.StripePublish;
            cat.PaypalSandboxLive = cvm.PaypalSandboxLive;
            cat.PaypalMerchantEmail = cvm.PaypalMerchantEmail;
            cat.RazorpayKey = cvm.RazorpayKey;
            cat.RazorpaySecret = cvm.RazorpaySecret;
            cat.PaytmTestLive = cvm.PaytmTestLive;
            cat.PaytmMID = cvm.PaytmMID;
            cat.PaytmKey = cvm.PaytmKey;
            cat.CreateDate = cvm.CreateDate;
            db.masterPaymentSettings.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");

       
        }
//StripeSecret,StripePublish,PaypalSandboxLive,PaypalMerchantEmail,RazorpayKey,RazorpaySecret,PaytmTestLive,PaytmMID,PaytmKey,CreateDate
      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPaymentSettings masterPaymentSettings = db.masterPaymentSettings.Find(id);
            if (masterPaymentSettings == null)
            {
                return HttpNotFound();
            }
            return View(masterPaymentSettings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterPaymentSettings cvm)
        {
            var cat = db.masterPaymentSettings.Where(x => x.ID == cvm.ID).FirstOrDefault();
            cat.COD = false;
            if (cvm.statusex == "Yes")
            {
                cat.COD = true;
            }
            cat.Stripe = false;
            if (cvm.statusez == "Yes")
            {
                cat.Stripe = true;
            }

            cat.Paypal = false;
            if (cvm.statusr == "Yes")
            {
                cat.Paypal = true;
            }

            cat.Razorpay = false;
            if (cvm.statusx == "Yes")
            {
                cat.Razorpay = true;
            }
            cat.RazorpayTestLive = false;
            if (cvm.statusy == "Yes")
            {
                cat.RazorpayTestLive = true;
            }
            cat.Paytm = false;
            if (cvm.statusp == "Yes")
            {
                cat.Paytm = true;
            }
            cat.StripeSecret = cvm.StripeSecret;
            cat.StripePublish = cvm.StripePublish;
            cat.PaypalSandboxLive = cvm.PaypalSandboxLive;
            cat.PaypalMerchantEmail = cvm.PaypalMerchantEmail;
            cat.RazorpayKey = cvm.RazorpayKey;
            cat.RazorpaySecret = cvm.RazorpaySecret;
            cat.PaytmTestLive = cvm.PaytmTestLive;
            cat.PaytmMID = cvm.PaytmMID;
            cat.PaytmKey = cvm.PaytmKey;
            cat.CreateDate = cvm.CreateDate;
          
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPaymentSettings masterPaymentSettings = db.masterPaymentSettings.Find(id);
            if (masterPaymentSettings == null)
            {
                return HttpNotFound();
            }
            return View(masterPaymentSettings);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterPaymentSettings masterPaymentSettings = db.masterPaymentSettings.Find(id);
            db.masterPaymentSettings.Remove(masterPaymentSettings);
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
